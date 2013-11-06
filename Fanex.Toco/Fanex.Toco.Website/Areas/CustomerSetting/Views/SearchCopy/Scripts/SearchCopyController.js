var searchCopyController = angular.module('SearchCopy', ['SearchCopy.service', 'SearchCopy.directive', 'SearchCopy.filter']);

// A Controller for your app
searchCopyController.controller('SearchCopyController', ['$scope', 'message', function ($scope, message) {
    /* define variables*/
    var user = {
        userid: -1,
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

    var sites = [];
    var selectedSite = '';

    var customer;
    var context;
    var ptGrid = null;
    var bettingLimitGrid = null;

    $scope.existedUser = { userid: -1, username: '', selectedSite: {}, sites: [], selectedCurrency: { id: 0, name: ''} };
    $scope.newUser = {};
    $scope.context = {};

    /* initialization */
    $scope.init = function () {
        /* init GUI */
        $scope.newUser = angular.copy(user);
        $("#step2").hide();
        $("#errorMsg").addClass("hidden");
        $scope.addEvents();
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
            $scope.searchExistingUser(this.value);
            $("#checkExisted").show();

        });

        $("#step1Next").click(function () {
            $scope.step1Next();
        });

        $("#step2Back").click(function () {
            $scope.step2Back();
        });

        $("#step2Submit").click(function () {
            $scope.copyExistingUser();
        });
    };

    $scope.step1Next = function () {
        if ($scope.existedUser.username == undefined || $scope.existedUser.username == "") {
            message.showAlertMessage("errorMsg", "Please Enter User Name.", 1);
        }
        else {
            if ($scope.existedUser.userid != "-1") {
                $("#step1").hide();
                $("#step2").show();
                $(".settingInfo div").hide();
                $("#divGeneralInformation").show();

                $("#step2 .tab-active").attr("class", "tab-inactive");
                $("#tabGenealInfomation").attr("class", "tab-active");

                $("#panelStep1").attr("class", "step-inactive");
                $("#panelStep2").attr("class", "step-active");
            }
            else {
                message.showAlertMessage("errorMsg", "The super doesn’t exist!", 1);
            }
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
        $(settingInfoId + " div").show();

        $("#step2 .tab-active").attr("class", "tab-inactive");
        $(tabId).attr("class", "tab-active");
    };

    //Checking required fields: username, password, site and currency
    $scope.checkRequiredFields = function () {
        if ($scope.newUser.username == undefined || $scope.newUser.username == "") {
            message.showAlertMessage("errorMsg", "Please Enter User Name.", 1);
            return false;
        }
        else {
            $("#errorMsg").addClass("hidden");
            return true;
        }
    };

    $scope.searchExistingUser = function (username) {
        var param = {
            username: username
        };

        $.ajax({
            type: "POST",
            url: toUrl("Super/SearchExistingSuper"),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(param),
            success: function (custid) {
                $scope.existedUser.userid = custid;
                if (custid != "-1") {
                    $("#checkExisted").addClass('success');
                    $("#checkExisted").removeClass('not_success');
                    $scope.getSiteByUserId(custid);
                }
                else {
                    $("#checkExisted").removeClass('success');
                    $("#checkExisted").addClass('not_success');
                    message.showAlertMessage("errorMsg", "The super doesn’t exist!", 1);
                }
            }
            , error: function (e) {
                message.showAlertMessage("errorMsg", "Error for searching existing user!", 1);
            }
        });
    };

    $scope.getSiteByUserId = function (custid) {
        var param = {
            custid: custid
        };

        $.ajax({
            type: "POST",
            url: toUrl("Super/GetSiteByUserId"),
            contentType: "application/json; charset=utf-8",
            dataType: 'html',
            data: JSON.stringify(param),
            success: function (result) {
                selectedSite = result;
                $scope.getAllSites();
            }
            , error: function (e) {
                message.showAlertMessage("errorMsg", "Error for getting site by user id!", 1);
            }                      
        });
    };

    $scope.getAllSites = function () {
        $.ajax({
            type: "POST",
            url: toUrl("Super/GetSites"),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: null,
            success: function (result) {
                $scope.existedUser.sites = result;
                $scope.existedUser.selectedSite = $scope.existedUser.sites[2];

//                for (var i = 0; i < $scope.existedUser.sites.length; i++) {
//                    if ($scope.existedUser.sites[i].site == selectedSite) {
//                        $scope.existedUser.selectedSite = $scope.existedUser.sites[i];
//                        break;
//                    }
//                }

                $scope.$apply(); //important for angularjs
            }
            , error: function (e) {
                message.showAlertMessage("errorMsg", "Error for getting sites of existing user!", 1);
            }
        });
    };


    $scope.copyExistingUser = function () {
        var param = {
            username: $scope.newUser.username,
            Userpwd: $scope.newUser.password,
            FirstName: $scope.newUser.firstname,
            LastName: $scope.newUser.lastname,
            Phone: $scope.newUser.phone,
            MobilePhone: $scope.newUser.mobilephone,
            Site: $scope.existedUser.sites,
            Fax: $scope.newUser.fax,
            Credit: $scope.newUser.maxcredit,
            IsInternal: $scope.newUser.internaluser,
            ExistingCustId: $scope.existedUser.userid,
            Creator: 0 //todo
        };

        $.ajax({
            type: "POST",
            url: toUrl("Super/CopyExistingSuper"),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(param),
            success: function (result) {
            }
            , error: function (e) {
                message.showAlertMessage("errorMsg", "Error for copying existing user!", 1);
            }
        });
    };

    /* start */
    $scope.init();

} ]);