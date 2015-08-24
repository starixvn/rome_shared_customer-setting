"use strict";

ColossusBetModule.controller('statusController', function ($scope, colossusBetRepositories) {
    colossusBetRepositories.getData().then(function () {
        $scope.$watch('statusPartial.isEnabled', function (newValue, oldValue) {
            if (newValue !== oldValue) {
                if ($scope.originalstatusPartial.isEnabled) {
                    $scope.isDownlinechkDisabled = true;
                    if ($scope.statusPartial.isEnabled != $scope.originalstatusPartial.isEnabled) {
                        if ($scope.originalstatusPartial.isDownlinesEnabled) {
                            $scope.statusPartial.isDownlinesEnabled = !$scope.originalstatusPartial.isDownlinesEnabled;
                        }
                        $scope.statusPartial.isStatusUpdated = true;
                    } else {
                        $scope.statusPartial.isDownlinesEnabled = $scope.originalstatusPartial.isDownlinesEnabled;
                        $scope.statusPartial.isStatusUpdated = false;
                    }
                } else {
                    $scope.statusPartial.isStatusUpdated = $scope.statusPartial.isEnabled != $scope.originalstatusPartial.isEnabled;
                    $scope.isDownlinechkDisabled = !$scope.statusPartial.isEnabled;
                }
            }
        }, true);

        $scope.$watch('isDownlinechkDisabled', function (newValue, oldValue) {
            if (newValue !== oldValue) {
                if ($scope.isDownlinechkDisabled) {
                    $scope.statusPartial.isDownlinesEnabled = false;
                }
            }
        }, true);

    }, function (reason) { }, function (update) { });
});