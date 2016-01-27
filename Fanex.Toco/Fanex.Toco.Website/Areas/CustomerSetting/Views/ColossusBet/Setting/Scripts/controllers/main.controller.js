"use strict";
var ColossusBetModule = angular.module('colossusBetSettingModule', []);
ColossusBetModule.config(['$httpProvider', function ($httpProvider) {
    'use strict';

    var antiXSRFToken = document.getElementsByName("__RequestVerificationToken");
    if (antiXSRFToken.length > 0) {
        $httpProvider.defaults.headers.post.__RequestVerificationToken = antiXSRFToken[0].value
    }

} ]);

ColossusBetModule.controller('mainController', function ($scope, $timeout, colossusBetRepositories, commonService) {
    colossusBetRepositories.getData().then(
        function (colossusBetSetting) {
            $scope.colossusBetSetting = colossusBetSetting;

            $scope.customerInformationPartial = $scope.colossusBetSetting.CustomerInformationPartial;
            $scope.statusPartial = $scope.colossusBetSetting.StatusPartial;
            $scope.comissionPartial = $scope.colossusBetSetting.ComissionPartial;
            $scope.cashInSharePartial = $scope.colossusBetSetting.CashInSharePartial;

            $scope.isUpdatedAll = false;
            $scope.errors = {};

            $scope.multipleEdit = {
                isMultipleEdit: isMultipleEdit,
                custids: isMultipleEdit ? custids : [],
                usernameArray: usernames,
                usernames: isMultipleEdit ? usernames.join(", ") : [],
                updatedCount: 0
            };

            if ($scope.multipleEdit.isMultipleEdit) {
                $scope.statusPartial.IsStatusUpdated = true;
                $scope.comissionPartial.IsCommisionUpdated = true;
                $scope.cashInSharePartial.IsCashInShareUpdated = true;
                $scope.isUpdatedAll = true;
            }

            $scope.$watch('[statusPartial.IsStatusUpdated, comissionPartial.IsCommisionUpdated, cashInSharePartial.IsCashInShareUpdated]', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    $scope.isUpdatedAll = $scope.statusPartial.IsStatusUpdated || $scope.comissionPartial.IsCommisionUpdated || $scope.cashInSharePartial.IsCashInShareUpdated;
                }
            }, true);

            $scope.updated = function (isUpdatePartial, partialName) {
                if (isUpdatePartial) {
                    switch (partialName) {
                        case "StatusPartial":
                            $scope.colossusBetSetting.ComissionPartial.IsCommisionUpdated = false;
                            $scope.colossusBetSetting.CashInSharePartial.IsCashInShareUpdated = false;
                            break;
                        case "ComissionPartial":
                            $scope.colossusBetSetting.StatusPartial.IsStatusUpdated = false;
                            $scope.colossusBetSetting.CashInSharePartial.IsCashInShareUpdated = false;
                            break;
                        case "CashInSharePartial":
                            $scope.colossusBetSetting.ComissionPartial.IsCommisionUpdated = false;
                            $scope.colossusBetSetting.StatusPartial.IsStatusUpdated = false;
                            break;
                        default:
                            break;
                    }
                }


                $scope.doUpdated($("#UpdateSettingtUrl").val());

            };

            $scope.showMesseges = function (html, isSuccess) {
                if (isSuccess) {
                    $timeout(function () {
                        $("#divShowInfo").append(html);
                        commonService.unblockUI();
                    }, 4000);
                }
                else {
                    $("#divShowInfo").append(html);
                    commonService.unblockUI();
                }
            }

            $scope.doUpdated = function (url) {
                commonService.blockUI();

                colossusBetRepositories.save(url, $scope.colossusBetSetting).then(
                    function (results) {
                        $("#divShowInfo").empty().show();

                        var html = "";
                        if (typeof results != 'undefined') {
                            var length = results.length;
                            var isError = false;
                            for (var i = 0; i < length; i++) {
                                if (results[i].ErrorCode > 0) {
                                    isError = true;
                                    html += '<div class="alerterror">' + results[i].ErrorMessage + '</div>';
                                }
                            }

                            if (!isError) {
                                $scope.showMesseges('<div class="alertsuccess">' + resources.successful + '</div>', true);
                                $timeout(function () {
                                    location.reload();
                                    parent.frames["centerFr"].location.reload();
                                }, 5000);
                            } else {
                                $scope.showMesseges(html, false);
                            }
                        } else {
                            $scope.showMesseges('<div class="alerterror">' + resources.failed + '</div>', false);
                        }
                    },
                    function () {
                        $scope.showMesseges('<div class="alerterror">' + resources.failed + '</div>', false);
                    });
            };
        }, function (reason) { }, function (update) { });
});