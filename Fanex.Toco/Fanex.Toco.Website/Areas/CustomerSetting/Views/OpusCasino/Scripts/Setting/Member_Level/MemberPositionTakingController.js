app.controller('memberPositionTakingController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isDisabledSavedPT = true;

    $scope.iM2 = $scope.positionTaking.iM2;

    $scope.m2Range = $scope.uM2Range =  $rootScope.getRangeValue($scope.positionTaking.m3, 0.01, 100);
    $scope.m2 = $scope.positionTaking.m2 = 0;
    $scope.uM2 = $scope.positionTaking.uM2 = 0;

    $scope.maxPTRange =$scope.minPTRange= $rootScope.getRangeValue($scope.positionTaking.l2, 0.01, 100);
    $scope.l2 = $scope.positionTaking.l2 = $scope.maxPTRange[0];
    $scope.n2 = $scope.positionTaking.n2 = 0;

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.commMax, 0.0005, 10000);
    $scope.comm2 = $scope.positionTaking.comm2 = $scope.commRange[0];

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSavedPT = value;
        $scope.$emit('positionTaking', $scope.isDisabledSavedPT);
    };

    $scope.monitorSaveButton = function () {
        if ($scope.iM2 != $scope.positionTaking.iM2) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        else {
            if (($scope.l2 != $scope.positionTaking.l2)
                || ($scope.n2 != $scope.positionTaking.n2)
                || ($scope.comm2 != $scope.positionTaking.comm2)) {
                $scope.broadcastOpusSetting(false);
                return;
            }
            else {
                if (($scope.positionTaking.iM2 && ($scope.uM2 != $scope.positionTaking.uM2)) || (!$scope.positionTaking.iM2 && ($scope.m2 != $scope.positionTaking.m2))) {
                    $scope.broadcastOpusSetting(false);
                    return;
                }
            }
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.n2Constrain = function () {
        if ($scope.positionTaking.l2 <= $scope.positionTaking.n2) {
            $scope.positionTaking.n2 = $scope.positionTaking.l2;
        }
        $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.l2, 0.01, 100);
    };

    $scope.l2Constrain = function () {
        if ($scope.positionTaking.iM2) {
            $scope.maxPTRange = $rootScope.getRangeValue($scope.positionTaking.m3, 0.01, 100);
        }
        else {
            $scope.positionTaking.l2 = Math.round(($scope.positionTaking.m3 - $scope.positionTaking.m2) * 100) / 100;
            $scope.maxPTRange = $rootScope.getRangeValue($scope.positionTaking.l2, 0.01, 100);
        }
    };

    $scope.$watch('[positionTaking.iM2, positionTaking.m2]', function () {
        $scope.l2Constrain();
        $scope.n2Constrain();
        $scope.monitorSaveButton();
    }, true);

    $scope.$watch('positionTaking.l2', function () {
        $scope.n2Constrain();
        $scope.monitorSaveButton();
    }, true);

    $scope.$watch('[positionTaking.n2, positionTaking.comm2]', function () {
        $scope.monitorSaveButton();
    }, true)
});