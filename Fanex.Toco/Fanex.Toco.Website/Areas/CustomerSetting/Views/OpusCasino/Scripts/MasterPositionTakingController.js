app.controller('masterPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;
    $scope.l4 = 0;
    $scope.n4 = 0;
    $scope.comm4 = 0;

    $scope.maxPTRange = $rootScope.getRangeValue(1, 0.01, 100);
    $scope.l4 = $scope.positionTaking.l4 = $scope.maxPTRange[0];
    $scope.positionTaking.n4 = 0;

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.commMax, 0.0005, 10000);
    $scope.comm4 = $scope.positionTaking.comm4 = $scope.commRange[0];

    $scope.monitorSaveButton = function () {
        if (($scope.l4 != $scope.positionTaking.l4)
            || ($scope.n4 != $scope.positionTaking.n4)
            || ($scope.comm4 != $scope.positionTaking.comm4)) {
            $scope.isDisabledSavedPT = false;
            $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
            return;
        }
        $scope.isDisabledSavedPT = true;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.$watch('positionTaking.l4', function () {
        if ($scope.positionTaking.l4 <= $scope.positionTaking.n4) {
            $scope.positionTaking.n4 = $scope.positionTaking.l4;
        }
        $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.l4, 0.01, 100);
        return;
    }, true);
});


