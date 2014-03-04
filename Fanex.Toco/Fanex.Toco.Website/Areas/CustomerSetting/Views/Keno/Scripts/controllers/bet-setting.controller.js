"use strict";

KenoModule.controller('betSettingController', function ($scope, kenoRepositories) {
    $scope.betSettingList = kenoRepositories.kenoSetting.betSettings;
    $scope.isBetSettingPartial = false;
    $scope.isCopyAll = false;

    $scope.copyAll = function (isCopy) {
        $scope.isCopyAll = isCopy;
    };

    $scope.$on('betSetting', function (event) {
        var length = $scope.betSettingList.length;
        for (var i = 0; i < length; i++) {
            var betSettingTmp = $scope.betSettingList[i];
            if (betSettingTmp.isBetSettingUpdated || betSettingTmp.isStatusUpdated) {
                $scope.isBetSettingPartial = true;
                return true;
            }
        }

        $scope.isBetSettingPartial = false;
    });


    $scope.$watch('isCopyAll', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            if ($scope.isCopyAll) {
                $scope.isBetSettingPartial = true;
            }
            else {
                $scope.$broadcast('betSetting');
            }
        }
    }, true);
});
