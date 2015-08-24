"use strict";

ColossusBetModule.controller('cashInShareController', function ($scope, colossusBetRepositories, commonService) {
    colossusBetRepositories.getData().then(
        function () {
            $scope.$watch('cashInSharePartial.cashInShareValue', function (newValue, oldValue) {
                var element = $("#cashInShareInputId");
                if (newValue !== oldValue) {
                    if ($scope.cashInSharePartial.cashInShareValue > $scope.cashInSharePartial.maxCashInShareValue) {
                        commonService.createTooltip(element, "Input must be less than " + $scope.cashInSharePartial.maxCashInShareValue);
                        element.addClass('invalidValue');
                        $scope.$parent.isCashInSharePartial = false;
                        return;
                    }

                    element.removeClass('invalidValue');
                    $scope.$parent.isCashInSharePartial = true;
                }
            }, true);

        }, function (reason) { }, function (update) { });
});