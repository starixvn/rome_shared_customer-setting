"use strict";

KenoModule.directive('memberPositionTakingElement', ['commonService', function (commonService) {
    return {
        restrict: 'A',
        scope: {
            positionTaking: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/MemberPositionTaking.htm',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope.positionTaking);

            $scope.originalPositionTaking = angular.copy($scope.positionTaking);

            $scope.a1Range = commonService.getRangePTValue($scope.positionTaking.a2, 0);
            if ($scope.positionTaking.a2 < $scope.positionTaking.a1) {
                $scope.a1Range.unshift($scope.positionTaking.a1);
            }

            $scope.commRange = commonService.getRangeCommValue($scope.positionTaking.aComm, 0);

            $scope.monitorSaveButton = function () {
                if ($scope.positionTaking.isUpdateA1
                    || $scope.positionTaking.isUpdateCO) {
                    $scope.positionTaking.isUpdatePT = true;
                }
                else {
                    $scope.positionTaking.isUpdatePT = false;
                }
                $scope.$emit('positionTakingRow');
            };

            $scope.$watch('positionTaking.aComm', function () {
                $scope.positionTaking.isUpdateCO = $scope.originalPositionTaking.aComm != $scope.positionTaking.aComm ? true : false;
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.a1', function () {
                if ($scope.positionTaking.a2 > $scope.positionTaking.a1) {
                    $scope.a1Range = commonService.getRangePTValue($scope.positionTaking.a2, 0);
                }

                $scope.positionTaking.isUpdateA1 = $scope.originalPositionTaking.a1 != $scope.positionTaking.a1 ? true : false;
                $scope.monitorSaveButton();
            }, true);
        }
    };
} ]);
