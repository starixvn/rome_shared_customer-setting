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

    var customer;
    var context;
    var ptGrid = null;
    var bettingLimitGrid = null;

    $scope.existedUser = { userid: -1, username: '' };
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
                if (custid != "-1") {
                    $scope.existedUser.userid = custid;
                    $("#checkExisted").addClass('success');
                    $("#checkExisted").removeClass('not_success');
                }
                else {
                    $("#checkExisted").removeClass('success');
                    $("#checkExisted").addClass('not_success');
                }
            }
            , error: function (e) {
                message.showAlertMessage("errorMsg", "The super doesn’t exist!", 1);
            }
        });
    };

    /* start */
    $scope.init();

} ]);