"use strict";

KenoModule.directive('masterOnMemberPositionTakingElement', ['commonService', function (commonService) {
    return {
        restrict: 'A',
        scope: {
            positionTaking: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/MasterOnMemberPositionTaking.htm',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope.positionTaking);

            $scope.originalPositionTaking = angular.copy($scope.positionTaking);

            $scope.m1Range = commonService.getRangePTValue($scope.positionTaking.m1max, $scope.positionTaking.m1um1min);
            $scope.uM1Range = commonService.getRangePTValue($scope.positionTaking.m3, $scope.positionTaking.m1um1min);

            $scope.monitorSaveButton = function () {
                if ($scope.positionTaking.isUpdateM1
                    || $scope.positionTaking.isUpdateUM1) {
                    $scope.positionTaking.isUpdatePT = true;
                }
                else {
                    $scope.positionTaking.isUpdatePT = false;
                }

                $scope.$emit('positionTakingRow');
            };

            $scope.$watch('[positionTaking.m1, positionTaking.uM1,  positionTaking.iM1]', function () {
                if ($scope.originalPositionTaking.iM1) { // Load in case within isAuto
                    if ($scope.positionTaking.iM1) {
                        $scope.positionTaking.isUpdateM1 = false;
                        $scope.positionTaking.isUpdateUM1 = (($scope.originalPositionTaking.uM1 != $scope.positionTaking.uM1)
                                                    || ($scope.originalPositionTaking.iM1 != $scope.positionTaking.iM1)) == true ? true : false;
                    }
                    else {
                        $scope.positionTaking.isUpdateM1 = true;
                        $scope.positionTaking.isUpdateUM1 = true;
                    }
                }
                else {
                    if ($scope.positionTaking.iM1) {
                        $scope.positionTaking.isUpdateM1 = true;
                        $scope.positionTaking.isUpdateUM1 = true;
                    }
                    else {
                        $scope.positionTaking.isUpdateM1 = $scope.originalPositionTaking.m1 != $scope.positionTaking.m1 ? true : false;
                        $scope.positionTaking.isUpdateUM1 = false;
                    }
                }

                $scope.monitorSaveButton();
            }, true);
        }
    };
} ]);
