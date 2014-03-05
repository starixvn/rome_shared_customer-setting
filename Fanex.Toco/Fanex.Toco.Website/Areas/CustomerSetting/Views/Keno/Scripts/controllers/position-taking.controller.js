"use strict";

KenoModule.controller('positionTakingController', function ($scope, kenoRepositories) {
    $scope.positionTakingList = kenoRepositories.kenoSetting.superPositionTakings;
    $scope.isPositionTakingPartial = false;
    $scope.isCopyAll = false;

    $scope.copyAll = function (isCopy) {
        $scope.isCopyAll = isCopy;
    };

    $scope.$on('positionTaking', function (event) {
        var length = $scope.positionTakingList.length;
        for (var i = 0; i < length; i++) {
            var positionTakingTmp = $scope.positionTakingList[i];
            if (positionTakingTmp.isUpdatePT) {
                $scope.isPositionTakingPartial = true;
                return true;
            }
        }

        $scope.isPositionTakingPartial = false;
    });


    $scope.$watch('isCopyAll', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            if ($scope.isCopyAll) {
                $scope.isPositionTakingPartial = true;
            }
            else {
                $scope.$broadcast('positionTaking');
            }
        }
    }, true);
});
