app.controller('betSettingController', function ($scope, $rootScope) {
    $scope.betLimitSetting = model.betLimitSetting;
    $scope.orignalBetLimitSetting = angular.copy(model.betLimitSetting); 

    $scope.isDisabledSaveBetSetting = true;

    $scope.$watch(function () {
        if (($scope.betLimitSetting.maxTransfer != $scope.orignalBetLimitSetting.maxTransfer) || ($scope.betLimitSetting.isDailyAutoResetLimit != $scope.orignalBetLimitSetting.isDailyAutoResetLimit)) {
            $scope.isDisabledSaveBetSetting = false;
            return;
        }
        $scope.isDisabledSaveBetSetting = true;
    });

});

