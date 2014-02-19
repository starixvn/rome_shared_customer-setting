OpusModule.controller('superPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;

    $scope.maxPTRange = $rootScope.getRangeValue($scope.positionTaking.s4Max, 0.01, 100);
    $scope.l4 = $scope.positionTaking.l4;
    $scope.n4 = $scope.positionTaking.n4;

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.comm4Max, 0.0005, 10000);
    $scope.comm4 = $scope.positionTaking.comm4;

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSavedPT = value;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.monitorSaveButton = function () {
        if (($scope.l4 != $scope.positionTaking.l4)
            || ($scope.n4 != $scope.positionTaking.n4)
            || ($scope.comm4 != $scope.positionTaking.comm4)) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.$watch('positionTaking.l4', function () {
        if ($scope.positionTaking.l4 <= $scope.positionTaking.n4) {
            $scope.positionTaking.n4 = $scope.positionTaking.l4;
        }
        $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.l4, 0.01, 100);
        $scope.monitorSaveButton();
    }, true);

    $scope.$watch('[positionTaking.n4,positionTaking.comm4]', function () {
        $scope.monitorSaveButton();
    }, true);
});


