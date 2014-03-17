"use strict";

KenoModule.directive('agentPositionTakingElement', ['commonService', function (commonService) {
    return {
        restrict: 'A',
        scope: {
            positionTaking: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/AgentPositionTaking.htm',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope.positionTaking);

            $scope.originalPositionTaking = angular.copy($scope.positionTaking);

            $scope.m2Range = $scope.uM2Range = commonService.getRangePTValue($scope.positionTaking.m3, 0);
            $scope.maxPTRange = {};
            $scope.minPTRange = {};
            $scope.commRange = commonService.getRangeCommValue($scope.positionTaking.mComm, 0);

            $scope.monitorSaveButton = function () {
                if ($scope.positionTaking.isUpdateUM2
                    || $scope.positionTaking.isUpdateM2
                    || $scope.positionTaking.isUpdateN2
                    || $scope.positionTaking.isUpdateACO) {
                    $scope.positionTaking.isUpdatePT = true;
                }
                else {
                    $scope.positionTaking.isUpdatePT = false;
                }
                $scope.$emit('positionTakingRow');
            };

            $scope.$watch('[positionTaking.m2, positionTaking.uM2, positionTaking.iM2]', function () {
                if ($scope.originalPositionTaking.iM2) { // Load in case within isAuto
                    if ($scope.positionTaking.iM2) {
                        $scope.positionTaking.l2 = $scope.positionTaking.m3;
                        $scope.positionTaking.isUpdateM2 = false;
                        $scope.positionTaking.isUpdateUM2 = (($scope.originalPositionTaking.uM2 != $scope.positionTaking.uM2)
                                                    || ($scope.originalPositionTaking.iM2 != $scope.positionTaking.iM2)) == true ? true : false;
                        $scope.positionTaking.isConfirmIsAuto = false;
                        $scope.$emit('confirmIsAuto', { "betTypeId": $scope.positionTaking.betTypeId, "isAdd": false });
                    }
                    else {
                        $scope.positionTaking.l2 = Math.round(($scope.positionTaking.m3 - $scope.positionTaking.m2) * 100) / 100;
                        $scope.positionTaking.isUpdateM2 = true;
                        $scope.positionTaking.isUpdateUM2 = true;
                        var newMemberValueMax = Math.min($scope.positionTaking.l3, $scope.positionTaking.m3) - $scope.positionTaking.a2;
                        $scope.memberNewValueRange = commonService.getRangePTValue(newMemberValueMax, 0);
                        $scope.$emit('confirmIsAuto', { "betTypeId": $scope.positionTaking.betTypeId, "isAdd": true });
                    }
                }
                else {
                    if ($scope.positionTaking.iM2) {
                        $scope.positionTaking.l2 = $scope.positionTaking.m3;
                        $scope.positionTaking.isUpdateM2 = true;
                        $scope.positionTaking.isUpdateUM2 = true;
                    }
                    else {
                        $scope.positionTaking.l2 = Math.round(($scope.positionTaking.m3 - $scope.positionTaking.m2) * 100) / 100;
                        $scope.positionTaking.isUpdateM2 = $scope.originalPositionTaking.m2 != $scope.positionTaking.m2 ? true : false;
                        $scope.positionTaking.isUpdateUM2 = false;
                    }
                }

                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.l2', function () {
                if ($scope.positionTaking.l2 < $scope.positionTaking.a2) {
                    $scope.positionTaking.a2 = $scope.positionTaking.l2;
                }

                $scope.maxPTRange = commonService.getRangePTValue($scope.positionTaking.l2, $scope.positionTaking.n2);
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.a2', function () {
                $scope.positionTaking.isUpdateM2 = $scope.originalPositionTaking.a2 != $scope.positionTaking.a2 ? true : false;

                if ($scope.positionTaking.a2 <= $scope.positionTaking.n2) {
                    $scope.positionTaking.n2 = $scope.positionTaking.a2;
                }

                $scope.minPTRange = commonService.getRangePTValue($scope.positionTaking.a2, 0);
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.n2', function () {
                $scope.positionTaking.isUpdateN2 = $scope.originalPositionTaking.n2 != $scope.positionTaking.n2 ? true : false;
                $scope.maxPTRange = commonService.getRangePTValue($scope.positionTaking.l2, $scope.positionTaking.n2);
                $scope.monitorSaveButton();
            }, true)

            $scope.$watch('positionTaking.aComm', function () {
                $scope.positionTaking.isUpdateACO = $scope.originalPositionTaking.aComm != $scope.positionTaking.aComm ? true : false;
                $scope.monitorSaveButton();
            }, true)

        }
    };
} ]);
