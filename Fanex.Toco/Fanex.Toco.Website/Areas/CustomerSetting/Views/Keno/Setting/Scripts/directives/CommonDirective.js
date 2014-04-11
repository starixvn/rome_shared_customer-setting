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

KenoModule.directive('betSettingConstrain', ['$filter',
    function ($filter) {
        return {
            require: '?ngModel',
            link: function ($scope, element, attrs, ctrl) {
                if (!ctrl) return;

                var valueModel = eval("$scope." + attrs.ngModel);

                if (valueModel > 0 && valueModel < MAX_VALUE) {
                    var symbol = attrs.betSettingConstrain == 1 ? " >= " : " <= ";

                    element.text(symbol + $scope.formatNumber(valueModel));
                }
            }
        };
    }
]);