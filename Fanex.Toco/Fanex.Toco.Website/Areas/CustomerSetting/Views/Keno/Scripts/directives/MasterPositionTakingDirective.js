"use strict";

KenoModule.directive('masterPositionTakingElement', ['commonService', function (commonService) {
    return {
        restrict: 'A',
        scope: {
            positionTaking: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/MasterPositionTaking.htm',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope.positionTaking);

            $scope.originalPositionTaking = angular.copy($scope.positionTaking);

            $scope.s3Range = $scope.uS3Range = commonService.getRangePTValue($scope.positionTaking.s4, 0);
            $scope.maxPTRange = {};
            $scope.minPTRange = {};
            $scope.commRange = commonService.getRangeCommValue($scope.positionTaking.sComm, 0);

            //            $scope.broadcastUpdateIsAuto = function () {
            //                $scope.$emit('confirmIsAuto', true);
            //            };

            $scope.monitorSaveButton = function () {
                if ($scope.positionTaking.isUpdateUS3
                    || $scope.positionTaking.isUpdateS3
                    || $scope.positionTaking.isUpdateN3
                    || $scope.positionTaking.isUpdateMComm) {
                    $scope.positionTaking.isUpdatePT = true;
                }
                else {
                    $scope.positionTaking.isUpdatePT = false;
                }

                $scope.$emit('positionTakingRow');
            };

            $scope.$watch('[positionTaking.s3, positionTaking.uS3, positionTaking.iS3]', function () {
                if ($scope.originalPositionTaking.iS3) { // Load in case within isAuto
                    if ($scope.positionTaking.iS3) {
                        $scope.positionTaking.l3 = $scope.positionTaking.s4;
                        $scope.positionTaking.isUpdateS3 = false;
                        $scope.positionTaking.isUpdateUS3 = (($scope.originalPositionTaking.uS3 != $scope.positionTaking.uS3)
                                                    || ($scope.originalPositionTaking.iS3 != $scope.positionTaking.iS3)) == true ? true : false;
                    }
                    else {
                        $scope.positionTaking.l3 = Math.round(($scope.positionTaking.s4 - $scope.positionTaking.s3) * 100) / 100;
                        $scope.positionTaking.isUpdateS3 = true;
                        $scope.positionTaking.isUpdateUS3 = true;
                        $scope.positionTaking.newMemberPTMax = $scope.positionTaking.l3;
                        $scope.$emit('confirmIsAuto', $scope.betTypeId);
                    }
                }
                else {
                    if ($scope.positionTaking.iS3) {
                        $scope.positionTaking.l3 = $scope.positionTaking.s4;
                        $scope.positionTaking.isUpdateS3 = true;
                        $scope.positionTaking.isUpdateUS3 = true;
                    }
                    else {
                        $scope.positionTaking.l3 = Math.round(($scope.positionTaking.s4 - $scope.positionTaking.s3) * 100) / 100;
                        $scope.positionTaking.isUpdateS3 = $scope.originalPositionTaking.s3 != $scope.positionTaking.s3 ? true : false;
                        $scope.positionTaking.isUpdateUS3 = false;
                    }
                }

                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.l3', function () {
                if ($scope.positionTaking.l3 < $scope.positionTaking.m3) {
                    $scope.positionTaking.m3 = $scope.positionTaking.l3;
                }

                $scope.maxPTRange = commonService.getRangePTValue($scope.positionTaking.l3, $scope.positionTaking.n3);
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.m3', function () {
                $scope.positionTaking.isUpdateS3 = $scope.originalPositionTaking.m3 != $scope.positionTaking.m3 ? true : false;

                if ($scope.positionTaking.m3 <= $scope.positionTaking.n3) {
                    $scope.positionTaking.n3 = $scope.positionTaking.m3;
                }

                $scope.minPTRange = commonService.getRangePTValue($scope.positionTaking.m3, 0);
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.n3', function () {
                $scope.positionTaking.isUpdateN3 = $scope.originalPositionTaking.n3 != $scope.positionTaking.n3 ? true : false;
                $scope.maxPTRange = commonService.getRangePTValue($scope.positionTaking.l3, $scope.positionTaking.n3);
                $scope.monitorSaveButton();
            }, true)

            $scope.$watch('positionTaking.mComm', function () {
                $scope.positionTaking.isUpdateMComm = $scope.originalPositionTaking.mComm != $scope.positionTaking.mComm ? true : false;
                $scope.monitorSaveButton();
            }, true)

        }
    };
} ]);
