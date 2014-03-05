"use strict";

KenoModule.controller('mainController', function ($scope, $timeout, kenoRepositories) {
    $scope.kenoSetting = kenoRepositories.kenoSetting;

    $scope.isUpdatedKeno = false;
    $scope.isUpdatePT = false;
    $scope.isUpdateBetSetting = false;

    $scope.errors = {};

    $scope.$watch(function () {
        if ($scope.isUpdatePT || $scope.isUpdateBetSetting) {
            $scope.isUpdatedKeno = true;
            return;
        }

        $scope.isUpdatedKeno = false;
    });

    $scope.$on('betSettingBlock', function (event, data) {
        $scope.isUpdateBetSetting = data;
    });

    $scope.$on('positionTakingBlock', function (event, data) {
        $scope.isUpdatePT = data;
    });

    $scope.updated = function (isUpdatePartial, partialName) {
        if (isUpdatePartial) {
            switch (partialName) {
                case "PositionTalking":
                    $scope.kenoSetting.betSettings = null;
                    break;
                case "BetSetting":
                    $scope.kenoSetting.positionTakings = null;
                    break;
                default:
                    break;
            }
        }

        $scope.doUpdated();
    };

    $scope.doUpdated = function () {
        BlockUI();
        var url = domain + 'customersetting/opuscasino/OpusCasinoSuperSetting/UpdateSuper';
        opusRepositories.save(url, $scope.opusSetting).then(
            function (results) {
                $("#divShowInfo").empty();
                if (typeof results != 'undefined') {
                    var length = results.length;
                    var isError = false;
                    for (var i = 0; i < length; i++) {
                        var className = results[i].ErrorCode > 0 ? "alerterror" : "alertsuccess";
                        var html = '<div class="' + className + '">' + results[i].ErrorMessage + '</div>';
                        $("#divShowInfo").append(html);
                        if (results[i].ErrorCode > 0) {
                            isError = true;
                        }
                    }

                    if (!isError) {
                        $timeout(function () {
                            UnBlockUI();
                            location.reload();
                            parent.frames["centerFr"].location.reload();
                        }, 5000);
                    } else {
                        UnBlockUI();
                    }
                }
                else {
                    var html = '<div class="alerterror">Updated fail.</div>';
                    $("#divShowInfo").append(html);
                    UnBlockUI();
                }
            },
            function () {
                var html = '<div class="alerterror">Updated fail.</div>';
                $("#divShowInfo").append(html);
                UnBlockUI();
            });
    };
});


//if (fanex.customerSetting.keno) {
//    fanex.customerSetting.keno.controller('mainController', ['$scope', '$rootScope', '$http', kenoRepositories, function ($scope, $rootScope, $http, kenoRepositories) {
//        $scope.kenoSetting = kenoRepositories.kenoSetting;

//        $scope.updateCustomerSetting = function (isUpdatePartial, sectionName) {
//            var changedBetSettings = null;
//            var changedPositiongTakings = null;
//            var changedCommissionSettings = null;

//            if (isUpdatePartial) {
//                switch (sectionName) {
//                    case "BetSetting":
//                        var betSettingScope = angular.element('[ng-controller="fanex.customerSetting.betSettingController"]').scope();
//                        if (!betSettingScope.isValidData()) {
//                            return;
//                        }
//                        changedBetSettings = betSettingScope.getChangedData();
//                        break;
//                    case "PositionTaking":
//                        var positionTakingScope = angular.element('[ng-controller="fanex.customerSetting.positionTakingController"]').scope();
//                        if (!positionTakingScope.isValidPositionTakings()) {
//                            return;
//                        }
//                        changedPositiongTakings = positionTakingScope.getChangedPositionTakings();

//                        if (!positionTakingScope.isValidCommissionSettings()) {
//                            return;
//                        }
//                        changedCommissionSettings = positionTakingScope.getChangedCommissionSettings();
//                        break;
//                    default:
//                        break;
//                }
//            }
//            else {
//                var betSettingScope = angular.element('[ng-controller="fanex.customerSetting.betSettingController"]').scope();
//                if (!betSettingScope.isValidData()) {
//                    return;
//                }
//                changedBetSettings = betSettingScope.getChangedData();

//                var positionTakingScope = angular.element('[ng-controller="fanex.customerSetting.positionTakingController"]').scope();
//                if (!positionTakingScope.isValidPositionTakings()) {
//                    return;
//                }
//                changedPositiongTakings = positionTakingScope.getChangedPositionTakings();

//                if (!positionTakingScope.isValidCommissionSettings()) {
//                    return;
//                }
//                changedCommissionSettings = positionTakingScope.getChangedCommissionSettings();
//            }

//            if ((!isUpdatePartial && changedBetSettings.length == 0 && changedPositiongTakings.length == 0 && changedCommissionSettings.length == 0)
//                || (isUpdatePartial && sectionName == "BetSetting" && changedBetSettings.length == 0)
//                || (isUpdatePartial && sectionName == "PositionTaking" && changedPositiongTakings.length == 0 && changedCommissionSettings.length == 0)) {
//                alert("Data isn't changed.");
//                return;
//            }

//            $http({
//                method: 'POST',
//                url: domain + "customersetting/keno/kenosetting/UpdateCustomerSetting?custid=1",
//                headers: { 'Content-Type': 'application/json' },
//                data: {
//                    BetSettings: changedBetSettings,
//                    PositiongTakingSections: (changedPositiongTakings == null ?
//                    null : fanex.customerSetting.commonService.composeChangedPositiongTakingSections(changedPositiongTakings, changedCommissionSettings)),
//                    FormType: $scope.model.formType
//                }
//            }).
//            success(function (data, status, headers, config) {
//                if (data.ErrCode != 0) {
//                    alert(data.ErrMsg);
//                }
//            }).
//            error(function (data, status, headers, config) {
//                alert(data.ErrMsg);
//            });
//        };
//    } ]);
//}