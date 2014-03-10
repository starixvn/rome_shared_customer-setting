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

            $scope.$on('changeSetting', function (event) {
                var a = 1;
            });

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

            $scope.maxbet = function () {
                var nbb = "fdasfdsa";
                return false;
            };

        } ]
    };
});

KenoModule.directive('format', ['$filter', '$parse', function ($filter, $parse) {
    return {
        require: '?betSettingElement',
        //        require: '?ngModel',
        link: function (scope, element, attrs, ctrl) {
            if (!ctrl) return;

            
//            var initValue = eval(attrs.initValue);

//            ctrl.$formatters.unshift(function (a) {
//                return $filter(attrs.format)(ctrl.$modelValue);
//            });

//            ctrl.$parsers.unshift(function (viewValue) {
//                var plainNumber = viewValue.replace(/[^\d|\-+|\.+]/g, '');
//                ctrl.$modelValue = eval(plainNumber);
//                element.val($filter(attrs.format)(plainNumber));
//                return plainNumber;
//            });

//            element.bind('blur', function (viewValue) {
//                var maxValue = eval(attrs.maxValue);
//                var minValue = eval(attrs.minValue);

//                if (ctrl.$modelValue < minValue) {
//                    alert("Value must be more than or equal to " + minValue);
//                    ctrl.$modelValue = initValue;
//                    ctrl.$setViewValue($filter('number')(initValue));
//                    scope.$emit('changeSetting');
//                    eval("scope." + attrs.ngModel + "=" + initValue);
//                    scope.$apply();
//                    return false;
//                }

//                if (ctrl.$modelValue > maxValue) {
//                    alert("Value must be less than or equal to " + maxValue);
//                    ctrl.$modelValue = initValue;
//                    ctrl.$setViewValue($filter('number')(initValue));
//                    return false;
//                }

//            });
        }
    };
} ]);

//KenoModule.directive('format', ['$filter', '$parse', function ($filter, $parse) {
//    return {
//                require: '^betSettingElement',
////        require: '?ngModel',
////        priority: 3,
//        link: function (scope, element, attrs, ctrl) {
//            if (!ctrl) return;

//            var initValue = eval(attrs.initValue);

//            ctrl.$formatters.unshift(function (a) {
//                return $filter(attrs.format)(ctrl.$modelValue);
//            });

//            ctrl.$parsers.unshift(function (viewValue) {
//                var plainNumber = viewValue.replace(/[^\d|\-+|\.+]/g, '');
//                ctrl.$modelValue = eval(plainNumber);
//                element.val($filter(attrs.format)(plainNumber));
//                return plainNumber;
//            });

//            element.bind('blur', function (viewValue) {
//                var maxValue = eval(attrs.maxValue);
//                var minValue = eval(attrs.minValue);

//                if (ctrl.$modelValue < minValue) {
//                    alert("Value must be more than or equal to " + minValue);
//                    ctrl.$modelValue = initValue;
//                    ctrl.$setViewValue($filter('number')(initValue));
//                    scope.$emit('changeSetting');
//                    eval("scope." + attrs.ngModel + "=" + initValue);
//                    scope.$apply();
//                    return false;
//                }

//                if (ctrl.$modelValue > maxValue) {
//                    alert("Value must be less than or equal to " + maxValue);
//                    ctrl.$modelValue = initValue;
//                    ctrl.$setViewValue($filter('number')(initValue));
//                    return false;
//                }

//            });
//        }
//    };
//} ]);

