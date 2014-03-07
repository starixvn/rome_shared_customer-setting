"use strict";

KenoModule.controller('betSettingController', function ($scope, kenoRepositories) {
    $scope.betSettingList = kenoRepositories.kenoSetting.betSettings;
    $scope.roleId = kenoRepositories.kenoSetting.customerInformation.roleId;

    $scope.betSettingCopyAll = kenoRepositories.kenoSetting.betSettingCopyAll;
    $scope.isBetSettingPartial = false;
    $scope.isCopyAll = false;

    console.log($scope.betSettingList);

    $scope.copyAll = function (isCopy, betTypeID) {
        $scope.isCopyAll = isCopy;
        $scope.betSettingCopyAll.bettypeId = isCopy == true ? betTypeID : -1;
        $scope.betSettingCopyAll.isCopiedAll = isCopy
    };

    $scope.$on('betSettingRow', function (event) {
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

    $scope.$on('changeSetting', function (event) {
        var a = 1;
    });

    $scope.$watch('isCopyAll', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            if ($scope.isCopyAll) {
                $scope.isBetSettingPartial = true;
            }
            else {
                $scope.$broadcast('betSettingRow');
            }
        }
    }, true);

    $scope.$watch('isBetSettingPartial', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            $scope.$emit('betSettingBlock', $scope.isBetSettingPartial);
        }
    }, true);

});
