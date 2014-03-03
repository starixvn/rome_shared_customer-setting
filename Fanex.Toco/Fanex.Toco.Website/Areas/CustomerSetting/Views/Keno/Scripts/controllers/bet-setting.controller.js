"use strict";

KenoModule.controller('betSettingController', function ($scope, kenoRepositories, betSettingService) {
    $scope.betSettingList = kenoRepositories.kenoSetting.betSettings;
    $scope.isBetSettingPartial = false;
    $scope.count = 0;

    $scope.$on('betSetting', function (event) {
        var length = $scope.betSettingList.length;
        for (var i = 0; i < length; i++) {
            var betSettingTmp = $scope.betSettingList[i];
            if (betSettingTmp.isBetSettingUpdated) {
                $scope.isBetSettingPartial = true;
                return true;
            }
        }

        $scope.isBetSettingPartial = false;
    });

});
