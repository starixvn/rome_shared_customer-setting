app.controller('masterOnMemberPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;

    $scope.iM1 = $scope.positionTaking.iM1;
    $scope.positionTaking.iM2 = true;

    $scope.m1Range = $scope.uM1Range = $rootScope.getRangeValue($scope.positionTaking.m4, 0.01, 100);
    $scope.m1 = $scope.positionTaking.m1 = $scope.positionTaking.iM3 == true ? $scope.positionTaking.m1 : 0;
    $scope.uM1 = $scope.positionTaking.uM1 = 0;

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSavedPT = value;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.monitorSaveButton = function () {
        if ($scope.iM1 != $scope.positionTaking.iM1) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        else {
            if (($scope.positionTaking.iM1 && ($scope.uM1 != $scope.positionTaking.uM1)) || (!$scope.positionTaking.iM1 && ($scope.m1 != $scope.positionTaking.m1))) {
                $scope.broadcastOpusSetting(false);
                return;
            }
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.$watch('[positionTaking.iM1,positionTaking.m1]', function () {
        $scope.monitorSaveButton();
    }, true);
});