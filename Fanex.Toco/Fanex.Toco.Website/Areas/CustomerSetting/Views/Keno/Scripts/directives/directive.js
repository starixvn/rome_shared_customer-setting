"use strict";

KenoModule.directive('checkDisableElement', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                var className = attrs.ngClassName;
                scope.$eval(attrs.ngModel) == false ? element.attr("disabled", "disabled").addClass(className + "Disabled").removeClass(className) : element.removeAttr("disabled", "disabled").addClass(className).removeClass(className + "Disabled");
            }, true);
        }
    }
});

KenoModule.directive('betSettingElement', function () {
    return {
        restrict: 'A',
        scope: {
            betSetting: "=ngModel",
            index: "=index"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/BetSetting/_BetSetting.htm',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope);

            $scope.originalBetSetting = angular.copy($scope.betSetting);

            $scope.$watch('betSetting.maxPerMatch', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if (($scope.betSetting.maxPerMatch != $scope.originalBetSetting.maxPerMatch)) {
                        $scope.betSetting.isBetSettingUpdated = true;
                        $scope.$emit('betSetting');
                        return true;
                    }

                    $scope.betSetting.isBetSettingUpdated = false;
                    $scope.$emit('betSetting');
                }
            }, true);

        }
    };
});


KenoModule.directive('format', ['$filter', function ($filter) {
    return {
        require: '?ngModel',

        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;

            ctrl.$formatters.unshift(function (a) {
                return $filter(attrs.format)(ctrl.$modelValue)
            });

            ctrl.$parsers.unshift(function (viewValue) {
                var maxValue = eval(attrs.maxValue);
                var minValue = eval(attrs.minValue);

                var plainNumber = viewValue.replace(/[^\d|\-+|\.+]/g, '');
                if (plainNumber == "") {
                    plainNumber = "0"
                }

                ctrl.$modelValue = eval(plainNumber);
                if (ctrl.$modelValue > maxValue) {
                    alert("Value must be less than or equal to " + maxValue);
                    ctrl.$modelValue = maxValue;
                    //                    return ctrl.$modelValue;
                }

                if (ctrl.$modelValue < minValue) {
                    alert("Value must be more than or equal to " + minValue);
                    ctrl.$modelValue = minValue;
                    //                    return ctrl.$modelValue;
                }


                elem.val($filter('number')(plainNumber));
                return plainNumber;
            });
        }
    };
} ]);

