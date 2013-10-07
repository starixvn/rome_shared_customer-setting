app.controller('customerInfoController', function ($scope, $rootScope) {
    $scope.customerInfo = model.customerInformation;
    $scope.isDownlineDisabled = true;

    $scope.isGenerateDisable = true;

    $scope.$watch('customerInfo.isEnable', function () {
        $scope.isDownlineDisabled = $scope.customerInfo.isEnable == true ? true : false;

        return;
    }, true);

});

