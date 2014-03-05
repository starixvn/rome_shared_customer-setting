"use strict";

KenoModule.directive('superPositionTakingElement', ['commonService', function (commonService) {
    return {
        restrict: 'A',
        scope: {
            positionTaking: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/SuperPositionTaking.htm',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope.positionTaking);

            $scope.originalPositionTaking = angular.copy($scope.positionTaking);

            $scope.maxPTRange = commonService.getRangePTValue($scope.positionTaking.l4, 0);
            $scope.commRange = commonService.getRangeCommValue($scope.positionTaking.sCommMax, 0);

            $scope.monitorSaveButton = function () {
                if ($scope.positionTaking.isUpdateS4
            || $scope.positionTaking.isUpdateSCO
            || $scope.positionTaking.isUpdateN4) {
                    $scope.positionTaking.isUpdatePT = true;
                }
                else {
                    $scope.positionTaking.isUpdatePT = false;
                }
                $scope.$emit('positionTakingRow');
            };

            $scope.$watch('positionTaking.s4', function () {
                $scope.positionTaking.isUpdateS4 = $scope.originalPositionTaking.s4 != $scope.positionTaking.s4 ? true : false;

                if ($scope.positionTaking.s4 <= $scope.positionTaking.n4) {
                    $scope.positionTaking.n4 = $scope.positionTaking.s4;
                }

                $scope.minPTRange = commonService.getRangePTValue($scope.positionTaking.s4, 0);
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.sComm', function () {
                $scope.positionTaking.isUpdateSCO = $scope.originalPositionTaking.sComm != $scope.positionTaking.sComm ? true : false;
                $scope.monitorSaveButton();
            }, true);

            $scope.$watch('positionTaking.n4', function () {
                $scope.positionTaking.isUpdateN4 = $scope.originalPositionTaking.n4 != $scope.positionTaking.n4 ? true : false;
                $scope.monitorSaveButton();
            }, true);

        }
    };
} ]);


//KenoModule.directive('format', ['$filter', function ($filter) {
//    return {
//        require: '?ngModel',

//        link: function (scope, elem, attrs, ctrl) {
//            if (!ctrl) return;

//            ctrl.$formatters.unshift(function (a) {
//                return $filter(attrs.format)(ctrl.$modelValue)
//            });

//            ctrl.$parsers.unshift(function (viewValue) {
//                var maxValue = eval(attrs.maxValue);
//                var minValue = eval(attrs.minValue);

//                var plainNumber = viewValue.replace(/[^\d|\-+|\.+]/g, '');
//                if (plainNumber == "") {
//                    plainNumber = "0"
//                }

//                ctrl.$modelValue = eval(plainNumber);
//                if (ctrl.$modelValue > maxValue) {
//                    alert("Value must be less than or equal to " + maxValue);
////                    ctrl.$modelValue = maxValue;
////                    plainNumber = maxValue.toString();
//                    //                    return ctrl.$modelValue;
//                }

//                if (ctrl.$modelValue < minValue) {
//                    alert("Value must be more than or equal to " + minValue);
////                    ctrl.$modelValue = minValue;
////                    plainNumber = minValue.toString();
//                    //                    return ctrl.$modelValue;
//                }


//                elem.val($filter('number')(plainNumber));
//                return plainNumber;
//            });
//        }
//    };
//} ]);

