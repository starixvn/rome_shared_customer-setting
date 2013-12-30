OpusModule.controller('betSettingController', function ($scope, $rootScope) {
    $scope.betLimitSetting = model.betLimitSetting;
    $scope.orignalBetLimitSetting = angular.copy(model.betLimitSetting);

    $scope.isDisabledSaveBetSetting = true;

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSaveBetSetting = value;
        $scope.$emit('betSetting', $scope.isDisabledSaveBetSetting);
    };

    $scope.$watch('[betLimitSetting.maxTransfer, betLimitSetting.isDailyAutoResetLimit]', function () {
        if (($scope.betLimitSetting.maxTransfer != $scope.orignalBetLimitSetting.maxTransfer) || ($scope.betLimitSetting.isDailyAutoResetLimit != $scope.orignalBetLimitSetting.isDailyAutoResetLimit)) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        $scope.broadcastOpusSetting(true);
    }, true);

});

