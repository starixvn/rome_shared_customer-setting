"use strict";

KenoModule.directive('betSettingElement', function () {
    return {
        restrict: 'EA',
        priority: 1,
        scope: {
            betSetting: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/BetSetting/BetSetting.htm',
        controller: ['$scope',
            function ($scope) {
                //            console.log($scope);

                $scope.originalBetSetting = angular.copy($scope.betSetting);

                $scope.isStatuschkDisabled = false;
                $scope.isDownlinechkDisabled = true;
                $scope.valids = {
                    "minBet": true,
                    "maxBet": true,
                    "maxPerMatch": true
                };

                if (!$scope.betSetting.upline.isEnabled || !$scope.betSetting.upline.isDownlinesEnabled) {
                    $scope.isStatuschkDisabled = true;
                }

                $scope.$on('betSetting.minBet', function (event, isValid) {
                    $scope.valids.minBet = isValid;
                });

                $scope.$on('betSetting.maxBet', function (event, isValid) {
                    $scope.valids.maxBet = isValid;
                });

                $scope.$on('betSetting.maxPerMatch', function (event, isValid) {
                    $scope.valids.maxPerMatch = isValid;
                });

                $scope.$watch('[betSetting.minBet,betSetting.maxBet,betSetting.maxPerMatch, valids]', function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        if ($scope.valids.minBet && $scope.valids.maxBet && $scope.valids.maxPerMatch) {
                            $scope.betSetting.isValid = true;
                        } else {
                            $scope.betSetting.isValid = false;
                        }

                        if (($scope.betSetting.minBet != $scope.originalBetSetting.minBet) || ($scope.betSetting.maxBet != $scope.originalBetSetting.maxBet) || ($scope.betSetting.maxPerMatch != $scope.originalBetSetting.maxPerMatch)) {
                            $scope.betSetting.isBetSettingUpdated = true;
                        } else {
                            $scope.betSetting.isBetSettingUpdated = false;
                        }

                        $scope.$emit('betSettingChecking');
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
                            } else {
                                $scope.betSetting.isDownlinesEnabled = $scope.originalBetSetting.isDownlinesEnabled;
                                $scope.betSetting.isStatusUpdated = false;
                            }
                        } else {
                            $scope.betSetting.isStatusUpdated = $scope.betSetting.isEnabled != $scope.originalBetSetting.isEnabled;
                            $scope.isDownlinechkDisabled = $scope.betSetting.isEnabled == false;
                        }

                        $scope.$emit('betSettingChecking');
                    }
                }, true);
            }
        ]
    };
});


KenoModule.directive('betSettingInput', ['$filter', '$parse',
    function ($filter, $parse) {
        return {
            require: '?ngModel',
            link: function (scope, element, attrs, ctrl) {
                if (!ctrl) return;

                scope.formatNumber = function (value) {
                    return $filter("number")(value);
                };

                var initValueModel = eval("scope." + attrs.ngModel);
                var initValueViewModel = scope.formatNumber(initValueModel);

                ctrl.$formatters.unshift(function () {
                    return scope.formatNumber(ctrl.$modelValue);
                });

                ctrl.$parsers.unshift(function (viewValue) {
                    var plainNumber = viewValue.replace(/[^\d|\-+|\.+]/g, '');
                    ctrl.$modelValue = eval(plainNumber);
                    element.val(scope.formatNumber(plainNumber));
                    return plainNumber;
                });

                scope.title = "141";

                scope.createTooltip = function () {
                    element.parent().tipsy({
                        delayIn: 0,
                        delayOut: 0,
                        gravity: "s",
                        opacity: 1,
                        title: function () {
                            return scope.title;
                        }
                    });
                };



                scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        var maxValue = eval(attrs.maxValue);
                        var minValue = eval(attrs.minValue);

                        var resetValue = function () {
                            ctrl.$modelValue = initValueModel;
                            //                        ctrl.$setViewValue(initValueViewModel);
                            //                        scope.$apply();
                        };

                        if (ctrl.$modelValue < minValue) {
                            //                        alert("Value must be more than or equal to " + minValue);
                            console.log("Value must be more than or equal to " + minValue);
                            resetValue();
                            element.addClass('invalidValue');
                            scope.$emit(attrs.ngModel, false);

                            scope.title = "Value must be more than or equal to " + minValue;
                            //                          element.parent().tipsy({
                            //                            delayIn: 0,
                            //                            delayOut: 0,
                            //                            gravity: "s",
                            //                            opacity: 1,
                            //                            title: function() {
                            //                                return scope.title;
                            //                            }
                            //                        });

                            return false;
                        }

                        if (ctrl.$modelValue > maxValue) {
                            //                    alert("Value must be less than or equal to " + maxValue);
                            console.log("Value must be less than or equal to " + maxValue);
                            resetValue();
                            element.addClass('invalidValue');
                            scope.$emit(attrs.ngModel, false);

                            scope.title = "Value must be less than or equal to " + maxValue;
                            //                          element.parent().tipsy({
                            //                            delayIn: 0,
                            //                            delayOut: 0,
                            //                            gravity: "s",
                            //                            opacity: 1,
                            //                            title: function() {
                            //                                return scope.title;
                            //                            }
                            //                        });
                            return false;
                        }

                        scope.title = "";

                        //                    element.parent().tipsy({
                        //                            delayIn: 0,
                        //                            delayOut: 0,
                        //                            gravity: "s",
                        ////                            live: true,  
                        //                            opacity: 1,
                        //                            title: function() {
                        //                                return scope.title;
                        //                            },
                        ////                            trigger: 'manual'
                        //                            //                html: true
                        //                        });

                        element.removeClass('invalidValue');
                        scope.$emit(attrs.ngModel, true);
                    }
                }, true);

                //            element.bind('blur', function () {
                //                var maxValue = eval(attrs.maxValue);
                //                var minValue = eval(attrs.minValue);

                //                var resetValue = function () {
                //                    ctrl.$modelValue = initValueModel;
                //                    ctrl.$setViewValue(initValueViewModel);
                //                    scope.$apply();
                //                };

                //                if (ctrl.$modelValue < minValue) {
                //                    alert("Value must be more than or equal to " + minValue);
                //                      console.log("Value must be more than or equal to " + minValue);
                //                    resetValue();
                //                    return false;
                //                }

                //                if (ctrl.$modelValue > maxValue) {
                ////                    alert("Value must be less than or equal to " + maxValue);
                //                     console.log("Value must be less than or equal to " + maxValue);
                //                    resetValue();
                //                    return false;
                //                }

                //            });
            }
        };
    }
]);

KenoModule.directive('betSettingConstrain', ['$filter',
    function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, element, attrs, ctrl) {
                if (!ctrl) return;

                var valueModel = eval("scope." + attrs.ngModel);

                if (valueModel > 0) {
                    var symbol = attrs.betSettingConstrain == 1 ? " >= " : " <= ";

                    element.text(symbol + scope.formatNumber(valueModel));
                }
            }
        };
    }
]);




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