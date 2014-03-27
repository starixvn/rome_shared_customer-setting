"use strict";

KenoModule.controller('mainController', function ($scope, $timeout, kenoRepositories, ngDialog, commonService) {
    $scope.kenoSetting = kenoRepositories.kenoSetting;
    console.log($scope.kenoSetting),
    $scope.isUpdatedKeno = false;
    $scope.isUpdatePT = false;
    $scope.isUpdateBetSetting = false;

    $scope.errors = {};

    $scope.$watch(function () {
        if ($scope.isUpdatePT && $scope.isUpdateBetSetting) {
            $scope.isUpdatedKeno = true;
            return;
        }

        $scope.isUpdatedKeno = false;
    });

    $scope.$on('betSettingPartial', function (event, data) {
        $scope.isUpdateBetSetting = data;
    });

    $scope.$on('positionTakingPartial', function (event, data) {
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

        $scope.$broadcast("updateAllMember");

        $scope.doUpdated();
    };

    $scope.doUpdated = function (isConfirmIsAuto, isUpdateAllMember) {

        console.log($scope.kenoSetting);

        commonService.blockUI();
        var url = domain + 'customersetting/keno/kenosetting/EditSuper';
        kenoRepositories.save(url, $scope.kenoSetting).then(
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
                            commonService.unblockUI();
                            location.reload();
                            parent.frames["centerFr"].location.reload();
                        }, 5000);
                    } else {
                        commonService.unblockUI();
                    }
                }
                else {
                    var html = '<div class="alerterror">Updated fail.</div>';
                    $("#divShowInfo").append(html);
                    commonService.unblockUI();
                }
            },
            function () {
                var html = '<div class="alerterror">Updated fail.</div>';
                $("#divShowInfo").append(html);
                commonService.unblockUI();
            });
    };
});
