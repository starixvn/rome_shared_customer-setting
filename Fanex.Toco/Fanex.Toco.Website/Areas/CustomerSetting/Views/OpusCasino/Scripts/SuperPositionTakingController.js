app.controller('superPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isUpdatedPT = true;

    $scope.maxPTRange = $rootScope.getRangeValue(1, 0.01, 100);
    $scope.positionTaking.l4 = $scope.maxPTRange[0];
    $scope.positionTaking.n4 = $scope.positionTaking.l4;

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.commMax, 0.0005, 10000);
    $scope.positionTaking.comm4 = $scope.commRange[0];
    $scope.betLimitSetting = model.betLimitSetting;

    $scope.$watch('positionTaking.l4', function () {
        if ($scope.positionTaking.l4 <= $scope.positionTaking.n4) {
            $scope.positionTaking.n4 = $scope.positionTaking.l4;
        }
        $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.l4, 0.01, 100);
        return;
    }, true);
});


