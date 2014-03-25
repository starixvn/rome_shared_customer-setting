"use strict";

KenoModule.directive('superOnMemberPositionTakingElement', ['commonService', function (commonService) {
    return {
        restrict: 'A',
        scope: {
            positionTaking: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/SuperOnMemberPositionTaking.htm',
        link: function ($scope, elem, attr, ctrl) {
//            console.log($scope.positionTaking);

            $scope.originalPositionTaking = angular.copy($scope.positionTaking);

            $scope.s1Range = commonService.getRangePTValue($scope.positionTaking.s1max, $scope.positionTaking.s1us1min);
            $scope.uS1Range = commonService.getRangePTValue($scope.positionTaking.s4, $scope.positionTaking.s1us1min);

            if ($scope.positionTaking.s1max < $scope.positionTaking.s1) {
                $scope.s1Range.unshift($scope.positionTaking.s1);
            }

            $scope.monitorSaveButton = function () {
                if ($scope.positionTaking.isUpdateS1
                    || $scope.positionTaking.isUpdateUS1) {
                    $scope.positionTaking.isUpdatePT = true;
                }
                else {
                    $scope.positionTaking.isUpdatePT = false;
                }

                $scope.$emit('positionTakingRow');
            };

            $scope.$watch('[positionTaking.s1, positionTaking.uS1,  positionTaking.iS1]', function () {
                if ($scope.positionTaking.s1max >= $scope.positionTaking.s1) {
                    $scope.m1Range = commonService.getRangePTValue($scope.positionTaking.s1max, $scope.positionTaking.s1us1min);
                }

                if ($scope.originalPositionTaking.iS1) { // Load in case within isAuto
                    if ($scope.positionTaking.iS1) {
                        $scope.positionTaking.isUpdateS1 = false;
                        $scope.positionTaking.isUpdateUS1 = (($scope.originalPositionTaking.uS1 != $scope.positionTaking.uS1)
                                                    || ($scope.originalPositionTaking.iS1 != $scope.positionTaking.iS1)) == true ? true : false;
                    }
                    else {
                        $scope.positionTaking.isUpdateS1 = true;
                        $scope.positionTaking.isUpdateUS1 = true;
                    }
                }
                else {
                    if ($scope.positionTaking.iS1) {
                        $scope.positionTaking.isUpdateS1 = true;
                        $scope.positionTaking.isUpdateUS1 = true;
                    }
                    else {
                        $scope.positionTaking.isUpdateS1 = $scope.originalPositionTaking.s1 != $scope.positionTaking.s1 ? true : false;
                        $scope.positionTaking.isUpdateUS1 = false;
                    }
                }

                $scope.monitorSaveButton();
            }, true);
        }
    };
} ]);
