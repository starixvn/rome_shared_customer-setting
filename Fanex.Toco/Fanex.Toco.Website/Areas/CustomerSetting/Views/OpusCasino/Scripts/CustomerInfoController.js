app.controller('customerInfoController', function ($scope, $rootScope) {
    $scope.customerInfo = model.customerInformation;
    $scope.isDownlineDisabled = true;

    $scope.IsDownline = $scope.customerInfo.isDownline;
    $scope.isEnable = $scope.customerInfo.isEnable;

    $scope.isSaveCustomerinfo = true;

    $scope.roleName = '';

    $scope.getRoleName = function () {
        switch ($scope.customerInfo.roleID) {
            case 4:
                $scope.roleName = 'Super';
                break;
            case 3:
                $scope.roleName = 'Master';
                break;
            case 2:
                $scope.roleName = 'Agent';
                break;
            case 2:
                $scope.roleName = 'Member';
                break;
        }
    };

    $scope.checkDownlineButton = function () {
        $scope.isDownlineDisabled = $scope.customerInfo.isEnable == true ? true : false;
    };

    $scope.broadcastOpusSetting = function (value) {
        $scope.isSaveCustomerinfo = value;
        $scope.$emit('customerInfo', $scope.isSaveCustomerinfo);
    };

    $scope.$watch('customerInfo', function () {
        if (($scope.customerInfo.isEnable != $scope.isEnable) || (($scope.IsDownline != $scope.customerInfo.isDownline) && !$scope.customerInfo.isEnable)) {
            $scope.broadcastOpusSetting(false);
            return;
        }
        $scope.broadcastOpusSetting(true);
    }, true);

});

