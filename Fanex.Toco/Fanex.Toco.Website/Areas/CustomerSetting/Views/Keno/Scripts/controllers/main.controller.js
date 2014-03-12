"use strict";

KenoModule.controller('mainController', function ($scope, $timeout, kenoRepositories, ngDialog) {
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

        $scope.$broadcast("updateAllMember");

        $scope.doUpdated();

        //        if ($scope.confirmIsAutoArray.length > 0) {
        //            var length = $scope.kenoSetting.positionTakings.length;
        //            $("#dialogMemberPT").dialog({
        //                title: "Confirm Update Member's PT",
        //                autoOpen: false,
        //                height: 200,
        //                width: 400,
        //                modal: true,
        //                buttons: {
        //                    "YES": function () {
        //                        for (var i = 0; i < length; i++) {
        //                            $scope.kenoSetting.positionTakings[i].isUpdateAllMember = true;
        //                        }
        ////                        $scope.$apply($scope.doUpdated(true));
        //                        $scope.doUpdated();
        //                        $("#dialogMemberPT").dialog("close");
        //                        return true;
        //                    },
        //                    "NO": function () {
        //                        for (var i = 0; i < length; i++) {
        //                            $scope.kenoSetting.positionTakings[i].isUpdateAllMember = false;
        //                        }
        //                        $scope.doUpdated();
        //                        $("#dialogMemberPT").dialog("close");
        //                        return true;
        //                    }
        //                },
        //                close: function (event, ui) {
        //                }
        //            });
        //            $('a.ui-dialog-titlebar-close').show();
        //            $("#dialogMemberPT").dialog("open");

        //            $('.ui-button-text').each(function (i) {
        //                $(this).html($(this).parent().attr('text'))
        //            })
        //        }
        //        else {
        //        }
        //        $scope.doUpdated();
    };

    $scope.doUpdated = function (isConfirmIsAuto, isUpdateAllMember) {

        console.log($scope.kenoSetting);


        //        BlockUI();
        //        var url = domain + 'customersetting/opuscasino/OpusCasinoSuperSetting/UpdateSuper';
        //        opusRepositories.save(url, $scope.opusSetting).then(
        //            function (results) {
        //                $("#divShowInfo").empty();
        //                if (typeof results != 'undefined') {
        //                    var length = results.length;
        //                    var isError = false;
        //                    for (var i = 0; i < length; i++) {
        //                        var className = results[i].ErrorCode > 0 ? "alerterror" : "alertsuccess";
        //                        var html = '<div class="' + className + '">' + results[i].ErrorMessage + '</div>';
        //                        $("#divShowInfo").append(html);
        //                        if (results[i].ErrorCode > 0) {
        //                            isError = true;
        //                        }
        //                    }

        //                    if (!isError) {
        //                        $timeout(function () {
        //                            UnBlockUI();
        //                            location.reload();
        //                            parent.frames["centerFr"].location.reload();
        //                        }, 5000);
        //                    } else {
        //                        UnBlockUI();
        //                    }
        //                }
        //                else {
        //                    var html = '<div class="alerterror">Updated fail.</div>';
        //                    $("#divShowInfo").append(html);
        //                    UnBlockUI();
        //                }
        //            },
        //            function () {
        //                var html = '<div class="alerterror">Updated fail.</div>';
        //                $("#divShowInfo").append(html);
        //                UnBlockUI();
        //            });
    };
});
