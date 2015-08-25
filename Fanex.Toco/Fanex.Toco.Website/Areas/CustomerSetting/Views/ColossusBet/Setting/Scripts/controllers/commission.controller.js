"use strict";

ColossusBetModule.controller('commissionController', function ($scope, colossusBetRepositories, commonService) {
    colossusBetRepositories.getData().then(
        function () {
            $scope.$watch('comissionPartial.commisionValue', function (newValue, oldValue) {
                var element = $("#commissionInputId");
                if (newValue !== oldValue) {
                    if ($scope.comissionPartial.commisionValue > $scope.comissionPartial.maxCommisionValue) {
                        commonService.createTooltip(element, "Input must be less than " + $scope.comissionPartial.maxCommisionValue);
                        element.addClass('invalidValue');
                        $scope.comissionPartial.isCommisionUpdated = false;
                        return;
                    }

                    element.removeClass('invalidValue');
                    $scope.comissionPartial.isCommisionUpdated = true;
                }
            }, true);

        }, function (reason) { }, function (update) { });
});

