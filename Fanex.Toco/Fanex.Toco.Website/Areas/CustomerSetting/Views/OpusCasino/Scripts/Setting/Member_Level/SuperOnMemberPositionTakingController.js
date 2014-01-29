OpusModule.controller('SuperOnMemberPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;

    $scope.iS1 = $scope.positionTaking.iS1;
    $scope.positionTaking.iS3 = true;

    $scope.s1Range = $scope.uS1Range = $rootScope.getRangeValue($scope.positionTaking.s4, 0.01, 100);
    $scope.s1 = $scope.positionTaking.s1 = $scope.positionTaking.iS3 == true ? $scope.positionTaking.s1 : 0;
    $scope.uS1 = $scope.positionTaking.uS1 = 0;

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSavedPT = value;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.monitorSaveButton = function () {
        if ($scope.iS1 != $scope.positionTaking.iS1) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        else {
            if (($scope.positionTaking.iS1 && ($scope.uS1 != $scope.positionTaking.uS1)) || (!$scope.positionTaking.iS1 && ($scope.s1 != $scope.positionTaking.s1))) {
                $scope.broadcastOpusSetting(false);
                return;
            }
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.$watch('[positionTaking.iS1,positionTaking.s1]', function () {
        $scope.monitorSaveButton();
    }, true);
});