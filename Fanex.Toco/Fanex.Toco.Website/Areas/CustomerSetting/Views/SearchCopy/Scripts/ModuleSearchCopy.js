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

    $scope.existedUser = { username: '' };
    $scope.newUser = {};
    $scope.context = {};

    /* define functions*/
    $scope.init = function () {
        $scope.newUser = angular.copy(user);
        
        $("#step2").hide();
        $("#errorMsg").addClass("hidden");

        $scope.addEvents();
    };

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
    }

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
    }

    /* initialization */
    $scope.init();

} ]);