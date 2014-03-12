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


KenoModule.directive('confirmIsAuto', function () {
    return {
        restrict: 'EA',
        scope: {
            positionTaking: "=ngModel",
            isAuto: "=isAuto"
        },
        templateUrl: '/Areas/CustomerSetting/Views/Keno/Setting/Partial/PositionTaking/ConfirmIsAuto.htm',
        controller: ['$scope', 'commonService', function ($scope, commonService) {
//            console.log($scope.positionTaking);
            $scope.memberNewValueRange = commonService.getRangePTValue($scope.positionTaking.l3, 0);
        } ]
    };
});
