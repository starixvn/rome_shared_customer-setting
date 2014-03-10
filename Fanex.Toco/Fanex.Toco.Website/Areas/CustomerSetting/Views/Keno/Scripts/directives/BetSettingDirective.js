"use strict";

KenoModule.directive('betSettingElement', function () {
    return {
        restrict: 'EA',
        //        priority: 2, 
        scope: {
            betSetting: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/BetSetting/BetSetting.htm',
        controller: ['$scope', function ($scope) {
            //            console.log($scope);

            $scope.originalBetSetting = angular.copy($scope.betSetting);

            $scope.isStatuschkDisabled = false;
            $scope.isDownlinechkDisabled = true;

            if (!$scope.betSetting.upline.isEnabled || !$scope.betSetting.upline.isDownlinesEnabled) {
                $scope.isStatuschkDisabled = true;
            }

            $scope.$watch('[betSetting.minBet,betSetting.maxBet,betSetting.maxPerMatch]', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if (($scope.betSetting.minBet != $scope.originalBetSetting.minBet)
                        || ($scope.betSetting.maxBet != $scope.originalBetSetting.maxBet)
                        || ($scope.betSetting.maxPerMatch != $scope.originalBetSetting.maxPerMatch)) {
                        $scope.betSetting.isBetSettingUpdated = true;
                    }
                    else {
                        $scope.betSetting.isBetSettingUpdated = false;
                    }

                    $scope.$emit('betSettingRow');
                }
            }, true);

            $scope.$watch('betSetting.isEnabled', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if ($scope.originalBetSetting.isEnabled) {
                        $scope.isDownlinechkDisabled = true;
                        if ($scope.betSetting.isEnabled != $scope.originalBetSetting.isEnabled) {
                            if ($scope.originalBetSetting.isDownlinesEnabled) {
                                $scope.betSetting.isDownlinesEnabled = !$scope.originalBetSetting.isDownlinesEnabled;
                            }
                            $scope.betSetting.isStatusUpdated = true;
                        }
                        else {
                            $scope.betSetting.isDownlinesEnabled = $scope.originalBetSetting.isDownlinesEnabled;
                            $scope.betSetting.isStatusUpdated = false;
                        }
                    }
                    else {
                        $scope.betSetting.isStatusUpdated = $scope.betSetting.isEnabled != $scope.originalBetSetting.isEnabled;
                        $scope.isDownlinechkDisabled = $scope.betSetting.isEnabled == false;
                    }

                    $scope.$emit('betSettingRow');
                }
            }, true);
        } ]
    };
});

KenoModule.directive('format', ['$filter', '$parse', function ($filter, $parse) {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ctrl) {
            if (!ctrl) return;

            var initValue = eval("scope." + attrs.ngModel);

            ctrl.$formatters.unshift(function () {
                return $filter(attrs.format)(ctrl.$modelValue);
            });

            ctrl.$parsers.unshift(function (viewValue) {
                var plainNumber = viewValue.replace(/[^\d|\-+|\.+]/g, '');
                ctrl.$modelValue = eval(plainNumber);
                element.val($filter(attrs.format)(plainNumber));
                return plainNumber;
            });

            element.bind('blur', function () {
                var maxValue = eval(attrs.maxValue);
                var minValue = eval(attrs.minValue);

                if (ctrl.$modelValue < minValue) {
                    alert("Value must be more than or equal to " + minValue);
                    ctrl.$modelValue = initValue;
                    ctrl.$setViewValue($filter('number')(initValue));
                    scope.$apply();
                    return false;
                }

                if (ctrl.$modelValue > maxValue) {
                    alert("Value must be less than or equal to " + maxValue);
                    ctrl.$modelValue = initValue;
                    ctrl.$setViewValue($filter('number')(initValue));
                    scope.$apply();
                    return false;
                }

            });
        }
    };
} ]);


//KenoModule.directive('format', ['$filter', function ($filter) {
//    return {
//        require: '?betSettingElement',
//        link: function (scope, element, attrs) {
//            var modelValueName = "scope." + attrs.modelName;
//            var initValue = eval(modelValueName);

//            scope.setModel = function (value) {
//                scope.setViewModel(value);
//                scope.setRealModel(value);
//                scope.$apply();
//            };

//            scope.getViewModel = function () {
//                return element.val().replace(/[^\d|\-+|\.+]/g, '');
//            };

//            scope.setViewModel = function (value) {
//                element.val($filter(attrs.format)(value));
//            };

//            scope.setRealModel = function (value) {
//                eval(modelValueName + "=" + value);
//            };

//            scope.setViewModel(initValue);

//            element.bind('keyup ', function () {
//                var value = scope.getViewModel();
//                scope.setModel(value);
//            });

//            element.bind('blur', function () {
//                var value = scope.getViewModel();
//                var maxValue = eval(attrs.maxValue);
//                var minValue = eval(attrs.minValue);

//                if (value < minValue) {
//                    alert("Value must be more than or equal to " + minValue);
//                    scope.setModel(initValue);
//                    return false;
//                }

//                if (value > maxValue) {
//                    alert("Value must be less than or equal to " + maxValue);
//                    scope.setModel(initValue);
//                    return false;
//                }
//            });
//        }
//    };
//} ]);
