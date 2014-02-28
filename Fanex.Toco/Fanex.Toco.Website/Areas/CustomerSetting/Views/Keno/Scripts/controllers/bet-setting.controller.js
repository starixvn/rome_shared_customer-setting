"use strict";

KenoModule.controller('betSettingController', function ($scope, kenoRepositories, betSettingService) {
    $scope.betSettingList = kenoRepositories.kenoSetting.betSettings;
    $scope.isUpdateBetSetting = false;
    $scope.init = function () {
        var length = $scope.betSettingList.length;

        for (var i = 0; i < length; i++) {
            betSettingService.setAction($scope.betSettingList[i]);
        }
    };

    $scope.init();

    $scope.getChangedData = function () {
        return betSettingService.getChangedData($scope.betSetting);
    };

    $scope.isValidData = function () {
        return betSettingService.isValidData($scope.betSetting);
    };
});
