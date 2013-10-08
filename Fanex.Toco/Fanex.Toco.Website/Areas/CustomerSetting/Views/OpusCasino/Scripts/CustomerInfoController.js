app.controller('customerInfoController', function ($scope, $rootScope) {
    $scope.customerInfo = model.customerInformation;
    $scope.isDownlineDisabled = true;

    $scope.IsDownline = $scope.customerInfo.isDownline;
    $scope.isEnable = $scope.customerInfo.isEnable;

    $scope.isSaveCustomerinfo = true;

    $scope.checkDownlineButton = function () {
        $scope.isDownlineDisabled = $scope.customerInfo.isEnable == true ? true : false;
    };

    $scope.$watch(function () {
        if (($scope.customerInfo.isEnable != $scope.isEnable) || ($scope.IsDownline != $scope.customerInfo.isDownline)) {
            $scope.isSaveCustomerinfo = false;
            $scope.$emit('customerInfo', $scope.isSaveCustomerinfo);
            return;
        }
        $scope.isSaveCustomerinfo = true;
        $scope.$emit('customerInfo', $scope.isSaveCustomerinfo);
    });

});

