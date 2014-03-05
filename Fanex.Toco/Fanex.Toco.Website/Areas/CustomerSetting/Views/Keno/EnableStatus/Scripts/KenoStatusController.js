angular.module('kenoStatusApp', []);

angular.element($('#ng-app')).ready(function () {
    $('#ng-app').show();
});

function KenoStatusController($scope) {
    $scope.model = model;

    $scope.sportTypes = $scope.model.settings.sportTypes;

    $scope.CheckStatus = function (preVal, scope) {
        if (preVal) {
            if (scope.item.enable) {
                scope.item.enableDownline = true;
                scope.item.disableDownline = true;
            }
            else {
                scope.item.enableDownline = false;
                scope.item.disableDownline = true;
            }
        }
        else {
            if (scope.item.enable) {
                scope.item.enableDownline = false;
                scope.item.disableDownline = false;
            }
            else {
                scope.item.enableDownline = false;
                scope.item.disableDownline = true;
            }
        }
    };

    $scope.Update = function () {
        //parent.Fanex.ExtAcc.BlockUI();

        $.ajax({
            dataType: 'json',
            type: 'post',
            url: resolveClientUrl('CustomerSetting/Keno/EnableStatus/Update'),
            data: {
                id: custId,
                uplineId: uplineId,
                txtCustomerObject: JSON.stringify(model),
                roleId: roleId,
                groupName: ''
            }
        })
        .success(function (data) {                        
            var errorCodes = data.errorCode.toString().indexOf('\u0007') != -1 ? data.errorCode.split('\u0007') : data.errorCode.toString();
            var success = true;

            $(errorCodes).each(function (i, error) {
                if (error != '' && parseInt(error) >= 0) {
                    success = false;
                }
            });

            if (data.errorCode == "0" || data.errorCode == "" || success) {
                $('#errMsg').attr('class', 'successMsg');
                $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                $('#errMsg').show();

                var timeout = setTimeout(function () {
                    clearTimeout(timeout);

                    document.location.reload(true);
                }, 3000);
            }
            else {
                $('#errMsg').attr('class', 'errMsg');
                $('#errMsg').html(data.errorMessage.formatErrorMessage());
                $('#errMsg').show();
            }

            //parent.Fanex.ExtAcc.UnBlockUI();
        }).error(function (data) {
            if (typeof console != 'undefined')
                console.log(data);            

            //parent.Fanex.ExtAcc.UnBlockUI();
        });
    };
};
/*
 test data
*/
var model = {
    "custId": 1176070,
    "userName": "SuperTest1",
    "password": "",
    "firstName": "Nexcel programmer",
    "lastName": "arisgame",
    "creator": 0,
    "phone": "0913660066aa",
    "mobilePhone": "123aaa",
    "fax": "sdaaa1",
    "credit": 0.0,
    "minCredit": 0.0,
    "maxCredit": 0.0,
    "memberMaxCredit": 0.0,
    "memberMinCredit": 0.0,
    "currencyId": 20,
    "isInternal": false,
    "sRecommend": 0,
    "mRecommend": 0,
    "aRecommend": 0,
    "disableAutoMasterPt": true,
    "settings": {
        "name": "ptGrid1",
        "sportTypes": [{
            "id": 151,
            "name": "Horse Racing",
            "key": "Horse Racing",
            "isHide": false,
            "collapsed": false,
            "betTypeGroups": [{
                "id": 41,
                "name": "UK/Irish Tote",
                "isHide": false,
                "allowCurrencies": "*",
                "bettingCommSetting": {
                    "minBet": 5.0000,
                    "maxBet": 1000.0000,
                    "maxPerMatch": 10001.0000,
                    "minBetMin": 10.0000,
                    "discount": 0.0090,
                    "discountMax": 0.01,
                    "disable": false,
                    "disableDownlines": true,
                    "disableUplines": false,
                    "isApplyPresetPt": false
                },
                "betTypes": [{
                    "id": 41,
                    "name": "Win",
                    "title": "Win",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5,
                        "iBC": 0.7000,
                        "iBCMax": 0.7
                    }
                }, {
                    "id": 42,
                    "name": "Place",
                    "title": "Place",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.4000,
                        "n4": 0.0000,
                        "l4": 0.5
                    }
                }]
            }, {
                "id": 31,
                "name": "SA Tote",
                "isHide": false,
                "allowCurrencies": "*",
                "bettingCommSetting": {
                    "minBet": 5.0000,
                    "maxBet": 1000.0000,
                    "maxPerMatch": 10001.0000,
                    "minBetMin": 10.0000,
                    "discount": 0.0090,
                    "discountMax": 0.01,
                    "disable": false,
                    "disableDownlines": true,
                    "disableUplines": false,
                    "isApplyPresetPt": false
                },
                "betTypes": [{
                    "id": 31,
                    "name": "Win",
                    "title": "Win",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5,
                        "iBC": 0.7000,
                        "iBCMax": 0.7
                    }
                }, {
                    "id": 32,
                    "name": "Place",
                    "title": "Place",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5
                    }
                }]
            }, {
                "id": 51,
                "name": "Aust Tote",
                "isHide": false,
                "allowCurrencies": "*",
                "bettingCommSetting": {
                    "minBet": 5.0000,
                    "maxBet": 1000.0000,
                    "maxPerMatch": 10001.0000,
                    "minBetMin": 10.0000,
                    "discount": 0.0090,
                    "discountMax": 0.01,
                    "disable": false,
                    "disableDownlines": true,
                    "disableUplines": false,
                    "isApplyPresetPt": false
                },
                "betTypes": [{
                    "id": 51,
                    "name": "Win",
                    "title": "Win",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5,
                        "iBC": 0.7000,
                        "iBCMax": 0.7
                    }
                }, {
                    "id": 52,
                    "name": "Place",
                    "title": "Place",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5
                    }
                }]
            }]
        }, {
            "id": 152,
            "name": "Greyhound",
            "key": "Greyhound",
            "isHide": false,
            "collapsed": true,
            "betTypeGroups": [{
                "id": 41,
                "name": "UK/Irish Tote",
                "isHide": false,
                "allowCurrencies": "*",
                "bettingCommSetting": {
                    "minBet": 5.0000,
                    "maxBet": 1000.0000,
                    "maxPerMatch": 10001.0000,
                    "minBetMin": 20.0000,
                    "discount": 0.0090,
                    "discountMax": 0.01,
                    "disable": false,
                    "disableDownlines": true,
                    "disableUplines": false,
                    "isApplyPresetPt": false
                },
                "betTypes": [{
                    "id": 41,
                    "name": "Win",
                    "title": "Win",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5,
                        "iBC": 0.7000,
                        "iBCMax": 0.7
                    }
                }, {
                    "id": 42,
                    "name": "Place",
                    "title": "Place",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5
                    }
                }]
            }, {
                "id": 51,
                "name": "Aust Tote",
                "isHide": false,
                "allowCurrencies": "*",
                "bettingCommSetting": {
                    "minBet": 5.0000,
                    "maxBet": 1000.0000,
                    "maxPerMatch": 10001.0000,
                    "minBetMin": 15.0000,
                    "discount": 0.0090,
                    "discountMax": 0.01,
                    "disable": false,
                    "disableDownlines": true,
                    "disableUplines": false,
                    "isApplyPresetPt": false
                },
                "betTypes": [{
                    "id": 51,
                    "name": "Win",
                    "title": "Win",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5,
                        "iBC": 0.7000,
                        "iBCMax": 0.7
                    }
                }, {
                    "id": 52,
                    "name": "Place",
                    "title": "Place",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.5000,
                        "n4": 0.0000,
                        "l4": 0.5
                    }
                }]
            }]
        }, {
            "id": 153,
            "name": "Harness",
            "key": "Harness",
            "isHide": false,
            "collapsed": true,
            "betTypeGroups": [{
                "id": 51,
                "name": "Aust Tote",
                "isHide": false,
                "allowCurrencies": "*",
                "bettingCommSetting": {
                    "minBet": 5.0000,
                    "maxBet": 1000.0000,
                    "maxPerMatch": 10001.0000,
                    "minBetMin": 10.0000,
                    "discount": 0.0090,
                    "discountMax": 0.01,
                    "disable": false,
                    "disableDownlines": true,
                    "disableUplines": false,
                    "isApplyPresetPt": false
                },
                "betTypes": [{
                    "id": 51,
                    "name": "Win",
                    "title": "Win",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.4000,
                        "n4": 0.0000,
                        "l4": 0.5,
                        "iBC": 0.7000,
                        "iBCMax": 0.7
                    }
                }, {
                    "id": 52,
                    "name": "Place",
                    "title": "Place",
                    "pt": {
                        "s4": 0.5000,
                        "m4": 0.5000,
                        "a4": 0.4000,
                        "n4": 0.0000,
                        "l4": 0.5
                    }
                }]
            }]
        }]
    }
};