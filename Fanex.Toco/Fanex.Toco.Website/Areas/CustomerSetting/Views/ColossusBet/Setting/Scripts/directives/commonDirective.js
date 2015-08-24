"use strict";

ColossusBetModule.directive('checkDisableElement', function() {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function(scope, element, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function() {
                var className = attrs.ngClassName;
                scope.$eval(attrs.ngModel) == false ? element.attr("disabled", "disabled").addClass(className + "Disabled").removeClass(className) : element.removeAttr("disabled", "disabled").addClass(className).removeClass(className + "Disabled");
            }, true);
        }
    }
});



ColossusBetModule.directive('inputFormat', ['$filter',
    function($filter) {
        return {
            require: '?ngModel',
            link: function($scope, element, attrs, ctrl) {
                if (!ctrl) {
                    return;
                }

                ctrl.$formatters.unshift(function() {
                    return $filter('number')(ctrl.$modelValue);
                });

                ctrl.$parsers.unshift(function(viewValue) {
                    var plainNumber = viewValue.replace(/[\,]/g, ''),
                        b = $filter('number')(plainNumber);

                    element.val(b);

                    return plainNumber;
                });
            }
        };
    }
]);