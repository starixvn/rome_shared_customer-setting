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


//KenoModule.directive('betSettingElement', function () {
//    return {
//        restrict: 'A',
//        scope: {},
//        replace: true,
//        templateUrl: '/Areas/CustomerSetting/Views/Keno/Scripts/directives/_BetSetting.html'
//    }
//});

KenoModule.directive('betSettingElement', function () {
    return {
        restrict: 'EA',
        scope: {
            betSetting: "=",
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Scripts/directives/_BetSetting.html',
        link: function ($scope, elem, attr, ctrl) {
            console.log($scope);
        }
    };
});
