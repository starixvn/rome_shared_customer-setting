app.controller('memberPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;

    $scope.a1Range = $rootScope.getRangeValue($scope.positionTaking.a2, 0.01, 100);
    $scope.a1 = $scope.positionTaking.a1 = $scope.a1Range[0];

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.comm2, 0.0005, 10000);
    $scope.comm1 = $scope.positionTaking.comm1 = $scope.commRange[0];

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSavedPT = value;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.monitorSaveButton = function () {
        if (($scope.a1 != $scope.positionTaking.a1)
            || ($scope.comm1 != $scope.positionTaking.comm1)) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.$watch('[positionTaking.a1,positionTaking.comm1]', function () {
        $scope.monitorSaveButton();
    }, true);
});