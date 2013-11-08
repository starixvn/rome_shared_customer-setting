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
    $scope.existedUser = { userid: -1, username: '', selectedSite: { siteid: 0, site: '' }, sites: [], selectedCurrency: '' };
    $scope.newUser = {};

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
            if ($scope.validateData() == true) {
                $scope.copyExistingUser();
            }
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

                $("#spanstep1").removeClass("step1-active");
                $("#spanstep1").addClass("step1-inactive");

                $("#spanstep2").removeClass("step2-inactive");
                $("#spanstep2").addClass("step2-active");
            }
            else {
                message.showAlertMessage("errorMsg", "The super doesn’t exist!", 1);
            }
        }
    };

    $scope.step2Back = function () {
        $("#step2").hide();
        $("#step1").show();

        $("#spanstep1").removeClass("step1-inactive");
        $("#spanstep1").addClass("step1-active");

        $("#spanstep2").removeClass("step2-active");
        $("#spanstep2").addClass("step2-inactive");
    };

    $scope.showSetting = function (settingInfoId, tabId) {
        if (tabId != '#tabGenealInfomation') {
            var result = $scope.checkRequiredFields();
            if (result == false) {
                return;
            }
        }

        $(".settingInfo div").hide();
        //$(settingInfoId).show();
        //$(settingInfoId + " div").show();

        $("#step2 .tab-active").attr("class", "tab-inactive");
        $('#' + tabId).attr("class", "tab-active");

        var url = ""; //todo
        switch (tabId) {
            case 'tabSportsbook': url = "http://localhost:53906/Websites/EditSuper/EditSuperPage?custId=1373389&uname=testcs"; break;
            case 'tabSportsbookMinPT': url = "http://localhost:53906/Websites/EditSuperMinPt/EditSuperMinPtPage?&custId=1373389&uname=testcs"; break;
            case 'tabRacing': url = "http://localhost:53906/Websites/EditSuperPTRacing/Index?custid=1373389"; break;
            case 'tabRacingMinPT': url = "http://localhost:53906/Websites/customer/pt/racing/SuperMinUpdate.aspx?custId=1373389"; break;
            case 'tabCasino': url = "http://localhost:53906/Websites/Customer/PositionTakingCS.aspx?custId=1373389"; break;
            case 'tabVirtualSports': url = "http://localhost:53906/Websites/VirtualSportSuperPT/EditSuper?custId=1373389&uname=testcs&roleId=4"; break;
            case 'tabBingo': url = "http://localhost:53906/Websites/Customer/PT/Bingo/SuperUpdate.aspx?custId=1373389"; break;
            case 'tabLiveCasino': url = "http://localhost:53906/Websites/LiveCasino/PTSetting/EditSuper.aspx?custId=1373389&exchangeId=20&roleId=4"; break;
            case 'tabGenealInformation': $("#divGeneralInformation").show();
        }
        if (tabId != 'tabGenealInformation') {
            $('#frmSport').attr('src', url)
            $("#divSport").show();
            $("#divSport div").show();
        };
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
                    message.hideAlertMessage("errorMsg");
                    $("#checkExisted").addClass('success');
                    $("#checkExisted").removeClass('not_success');
                    $scope.getSiteAndCurrencyByUserId(custid);
                }
                else {
                    $("#checkExisted").removeClass('success');
                    $("#checkExisted").addClass('not_success');
                    message.showAlertMessage("errorMsg", "The super doesn’t exist!", 1);
                }
            }
            , error: function (xhr, ajaxOptions, thrownError) {
                message.showAlertMessage("errorMsg", "Error for searching existing user!", 1);
            }
        });
    };

    $scope.getSiteAndCurrencyByUserId = function (custid) {
        var param = {
            custid: custid
        };

        $.ajax({
            type: "POST",
            url: toUrl("Super/GetSiteAndCurrencyByUserId"),
            contentType: "application/json; charset=utf-8",
            dataType: 'html',
            data: JSON.stringify(param),
            success: function (result) {
                message.hideAlertMessage("errorMsg");
                var site_currency = result.split(',');
                $scope.existedUser.selectedSite.site = site_currency[0];
                $scope.existedUser.selectedCurrency = site_currency[1];
                $scope.getAllSites();
            }
                , error: function (xhr, ajaxOptions, thrownError) {
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
                message.hideAlertMessage("errorMsg");
                $scope.$apply(function () {
                    $scope.existedUser.sites = result;
                });

                $("select#listSite option").each(function () { this.selected = (this.text == $scope.existedUser.selectedSite.site); });
            }
            , error: function (xhr, ajaxOptions, thrownError) {
                message.showAlertMessage("errorMsg", "Error for getting sites of existing user!", 1);
            }
        });
    };

    $scope.validateData = function () {
        if ($scope.newUser.username == undefined || $scope.newUser.username == "") {
            message.showAlertMessage("errorMsg", "The username is required!", 1);
            return false;
        }
        if ($scope.newUser.password == undefined || $scope.newUser.password == "") {
            message.showAlertMessage("errorMsg", "The password is required!", 1);
            return false;
        }

        return true;
    };

    $scope.copyExistingUser = function () {
        var param = {
            username: $scope.newUser.username,
            Userpwd: $scope.newUser.password,
            FirstName: $scope.newUser.firstname,
            LastName: $scope.newUser.lastname,
            Phone: $scope.newUser.phone,
            MobilePhone: $scope.newUser.mobilephone,
            Site: $("#listSite option:selected").text(), //$scope.existedUser.selectedSite.site,
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
                var success = result != null && result.IsSuccessful == true;
                if (success) {
                    message.hideAlertMessage("errorMsg");
                } else {
                    if (result != null) {
                        message.showAlertMessage("errorMsg", result.Message, 1);
                    }
                }
            }
            , error: function (xhr, ajaxOptions, thrownError) {
                message.showAlertMessage("errorMsg", "Error for copying existing user!", 1);
            }
        });
    };

    /* start */
    $scope.init();

} ]);