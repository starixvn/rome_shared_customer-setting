"use strict";

KenoModule.controller('customerInfoController', function ($scope, kenoRepositories, dataService) {
    //    $scope.customerInfo = kenoRepositories.kenoSetting.customerInformation;
    $scope.customerInfo = dataService.createCustomerInfo();
    return {
        getScope: function () {
            return $scope;
        }
    }
});
