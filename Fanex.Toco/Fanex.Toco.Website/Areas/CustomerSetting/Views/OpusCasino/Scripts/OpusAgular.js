var app = angular.module('OpusCasino', []);

app.controller('customerInfoController', function ($scope, $rootScope) {
    $scope.customerInfo = model.customerInformation;

    $scope.isGenerateDisable = true;
});