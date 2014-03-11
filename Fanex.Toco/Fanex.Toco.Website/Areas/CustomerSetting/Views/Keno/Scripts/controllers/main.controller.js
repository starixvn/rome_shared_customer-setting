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
