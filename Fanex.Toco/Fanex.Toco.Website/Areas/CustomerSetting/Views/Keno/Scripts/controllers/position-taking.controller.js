"use strict";

KenoModule.controller('positionTakingController', function ($scope, kenoRepositories) {
    $scope.positionTakingList = kenoRepositories.kenoSetting.positionTakings;

    $scope.positionTakingCopyAll = kenoRepositories.kenoSetting.betSettingCopyAll;
    $scope.isPositionTakingPartial = false;
    $scope.isCopyAll = false;

    $scope.copyAll = function (isCopy, betTypeID) {
        $scope.isCopyAll = isCopy;
        $scope.positionTakingCopyAll.bettypeId = isCopy == true ? betTypeID : -1; ;
        $scope.positionTakingCopyAll.isCopiedAll = isCopy
    };

    $scope.$on('positionTakingRow', function (event) {
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

    $scope.$on('confirmIsAuto', function (event, data) {
        var index = $scope.$parent.confirmIsAutoArray.indexOf(data.betTypeId);

        if (data.isAdd && index == -1) {
            $scope.$parent.confirmIsAutoArray.push(data.betTypeId);
        }

        if (!data.isAdd && index != -1) {
            $scope.$parent.confirmIsAutoArray.splice(index, 1);
        }

        return false;
    });

    $scope.$watch('isCopyAll', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            if ($scope.isCopyAll) {
                $scope.isPositionTakingPartial = true;
            }
            else {
                $scope.$broadcast('positionTakingRow');
            }
        }
    }, true);

    $scope.$watch('isPositionTakingPartial', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            $scope.$emit('positionTakingBlock', $scope.isPositionTakingPartial);
        }
    }, true);
});
