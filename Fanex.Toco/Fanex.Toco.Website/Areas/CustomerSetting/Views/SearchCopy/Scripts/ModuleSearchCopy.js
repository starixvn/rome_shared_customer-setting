angular.module('SearchCopy.service', []);
angular.module('SearchCopy.directive', []);
angular.module('SearchCopy.filter', []);

var mySearchCopy = angular.module('SearchCopy', ['SearchCopy.service', 'SearchCopy.directive', 'SearchCopy.filter']);

// A Controller for your app
mySearchCopy.controller('SearchCopyController', ['$scope', function ($scope) {
    /* define variables*/
    var user = {
        username: '',
        password: '',
        firstname: '',
        lastname: '',
        phone: '',
        mobilephone: '',
        fax: '',
        maxcredit: '',
        currencylist: [
            { currency: '' }
        ],
        internaluser: false
    };

    var customer;
    var context;

    $scope.existedUser = { username: '' };
    $scope.newUser = {};
    $scope.context = {};

    /* initialization */
    $scope.init = function () {
        /* init GUI */
        $scope.newUser = angular.copy(user);
        $("#step2").hide();
        $("#errorMsg").addClass("hidden");
        $scope.addEvents();

        /* get data */
        $scope.initContext();
    };

    /* define functions*/
    $scope.addEvents = function () {
        $("#existedUserName").focus(function () {
            $("#errorMsg").addClass("hidden");
            $("#nameReqErr").hide();
            $("#superNotExistedErr").hide();
            $("#checkExisted").hide();
        });

        $("#existedUserName").focusout(function () {
            $scope.searchExistingUser();
            $("#checkExisted").show();

        });

        $("#step1Next").click(function () {
            $scope.step1Next();
        });

        $("#step2Back").click(function () {
            $scope.step2Back();
        });
    };

    $scope.searchExistingUser = function () {
        if ($scope.existedUser.username != undefined && $scope.existedUser.username != "") {//todo
            $("#checkExisted").addClass('success');
            $("#checkExisted").removeClass('not_success');
        }
        else {
            $("#checkExisted").removeClass('success');
            $("#checkExisted").addClass('not_success');
        }
    };

    $scope.searchExistingSupper = function () {
        var param = {
            uplineId: window.uplineId,
            targetCustId: window.custId,
            targetRoleId: window.targetRoleId,
            roleId: window.pageRole
        };

        $.ajax({
            type: "POST",
            url: parent.resolveClientUrl("CustomerSetting/Racebook/RacingSetting/GetSetting"),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(param),
            success: function (stringContext) {
                customerPTRacingSetting.context = JSON.parse(stringContext);
                customerPTRacingSetting.getCustomerInfo();
            }
            , error: function (e) {
                showAlertMessage("err", "Init Context Error", 1);
            }
        });
    };

    $scope.step1Next = function () {
        if ($scope.existedUser.username != undefined && $scope.existedUser.username != "") {//todo
            $("#step1").hide();
            $("#step2").show();
            $(".settingInfo div").hide();
            $("#divGeneralInforamtion").show();

            $("#step2 .tab-active").attr("class", "tab-inactive");
            $("#tabGenealInfomation").attr("class", "tab-active");

            $("#panelStep1").attr("class", "step-inactive");
            $("#panelStep2").attr("class", "step-active");
        }
        else {
            $("#errorMsg").removeClass("hidden");
            $("#superNotExistedErr").show();
        }
    };

    $scope.step2Back = function () {
        $("#step2").hide();
        $("#step1").show();

        $("#panelStep2").attr("class", "step-inactive");
        $("#panelStep1").attr("class", "step-active");
    };

    $scope.showSetting = function (settingInfoId, tabId) {
        if (tabId != '#tabGenealInfomation') {
            var result = $scope.checkRequiredFields();
            if (result == false) {
                return;
            }
        }

        $(".settingInfo div").hide();
        $(settingInfoId).show();

        $("#step2 .tab-active").attr("class", "tab-inactive");
        $(tabId).attr("class", "tab-active");
    };

    //Checking required fields: username, password, site and currency
    $scope.checkRequiredFields = function () {
        var result = true;

        if ($scope.newUser.username == undefined || $scope.newUser.username == "") {
            $("#errorMsg").removeClass("hidden");
            $("#nameReqErr").show();
            result = false;
        }
        else {
            $("#errorMsg").addClass("hidden");
        }

        return result;
    };

    //get context of form
    $scope.initContext = function () {
        $.ajax({
            type: "POST",
            url: "GetContextObject",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{"superId":' + JSON.stringify(superId) + '}',
            success: function (stringContext) {
                context = JSON.parse(stringContext);
                $scope.getCustomer();
            }
        , error: function (e) {
            showAlertMessage("err", "Init Context Error", 1);
        }
        });
    };

    //get default super value
    $scope.getCustomer = function () {
        $.ajax({
            type: "POST",
            url: "GetCustomerObject",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{"requestContext":' + JSON.stringify(context) + '}',
            success: function (stringCustomer) {
                customer = JSON.parse(stringCustomer);
                $scope.initPtGrid();
                //$scope.initForKnockout();
                $scope.initCommission();
                //set credit min
                $('#txtCreditMin').text(''.FormatNumber(customer.minCredit));
                $('#txtCredit').val(''.FormatNumber(customer.credit));
            }
        , error: function (e) {
            showAlertMessage("err", "Init Customer Error", 1);
        }
        });
    };

    //init PT Object Grid
    $scope.initPtGrid = function () {
        Fanex.PTEngine.CURRENT_CONTEXT = context;
        ptGrid = new Fanex.PTEngine.PTGrid(customer.ptSetting);
        var divPtGrid = document.getElementById('divPtGrid');
        ptGrid.Render(divPtGrid);

        bettingLimitGrid = new Fanex.PTEngine.BettingLimit(customer.bettingLimit);
        var bettingLimitContainer = document.getElementById('divBettingLimit');
        bettingLimitContainer.innerHTML = "";
        bettingLimitGrid.Render(bettingLimitContainer);
    };

    //render Commission combobox
    $scope.initCommission = function () {
        var commissionSetting = customer.commissionSetting;
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listGroupA'), 0, Math.max(commissionSetting.groupAMax, commissionSetting.groupA), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.groupA);
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listGroupB'), 0, Math.max(commissionSetting.groupBMax, commissionSetting.groupB), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.groupB);
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listGroupC'), 0, Math.max(commissionSetting.groupCMax, commissionSetting.groupC), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.groupC);
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listGroupD'), 0, Math.max(commissionSetting.groupDMax, commissionSetting.groupD), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.groupD);
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listOther'), 0, Math.max(commissionSetting.discountCSMax, commissionSetting.discountCS), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.discountCS);
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listGroup1x2'), 0, Math.max(commissionSetting.discount1x2Max, commissionSetting.discount1x2), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.discount1x2);
        Fanex.PTEngine.PT_SetSelect(document.getElementById('listGameComm'), 0, Math.max(commissionSetting.discountNumberMax, commissionSetting.discountNumber), Fanex.PTEngine.COMMISSION_STEP, commissionSetting.discountNumber);
    };

    /* start */
    $scope.init();

} ]);