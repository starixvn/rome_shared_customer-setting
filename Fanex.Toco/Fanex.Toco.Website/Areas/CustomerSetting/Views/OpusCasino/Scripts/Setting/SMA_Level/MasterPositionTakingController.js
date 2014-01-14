OpusModule.controller('masterPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;

    $scope.iS3 = $scope.positionTaking.iS3;

    $scope.s3Range = $scope.uS3Range = $rootScope.getRangeValue($scope.positionTaking.s4, 0.01, 100);
    $scope.s3 = $scope.positionTaking.s3;
    $scope.uS3 = $scope.positionTaking.uS3;

    $scope.maxPTRange = $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.s4 - $scope.positionTaking.s3, 0.01, 100);
    $scope.l3 = $scope.positionTaking.l3;
    $scope.n3 = $scope.positionTaking.n3;

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.comm4, 0.0005, 10000);
    $scope.comm3 = $scope.positionTaking.comm3;

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSavedPT = value;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.monitorSaveButton = function () {
        if ($scope.iS3 != $scope.positionTaking.iS3) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        else {
            if (($scope.l3 != $scope.positionTaking.l3)
                || ($scope.n3 != $scope.positionTaking.n3)
                || ($scope.comm3 != $scope.positionTaking.comm3)) {
                $scope.broadcastOpusSetting(false);
                return;
            }
            else {
                if (($scope.positionTaking.iS3 && ($scope.uS3 != $scope.positionTaking.uS3)) || (!$scope.positionTaking.iS3 && ($scope.s3 != $scope.positionTaking.s3))) {
                    $scope.broadcastOpusSetting(false);
                    return;
                }
            }
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.n3Constrain = function () {
        if ($scope.positionTaking.l3 <= $scope.positionTaking.n3) {
            $scope.positionTaking.n3 = $scope.positionTaking.l3;
        }
        $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.l3, 0.01, 100);
    };

    $scope.l3Constrain = function () {
        if ($scope.positionTaking.iS3) {
            $scope.maxPTRange = $rootScope.getRangeValue($scope.positionTaking.s4, 0.01, 100);
        }
        else {
            $scope.positionTaking.l3 = Math.round(($scope.positionTaking.s4 - $scope.positionTaking.s3) * 100) / 100;
            $scope.maxPTRange = $rootScope.getRangeValue($scope.positionTaking.l3, 0.01, 100);
        }
    };

    $scope.$watch('[positionTaking.iS3, positionTaking.s3]', function () {
        $scope.l3Constrain();
        $scope.n3Constrain();
        $scope.monitorSaveButton();
    }, true);

    $scope.$watch('positionTaking.l3', function () {
        $scope.n3Constrain();
        $scope.monitorSaveButton();
    }, true);

    $scope.$watch('[positionTaking.n3, positionTaking.comm3]', function () {
        $scope.monitorSaveButton();
    }, true)
});