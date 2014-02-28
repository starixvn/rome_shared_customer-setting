if (fanex.customerSetting) {
    fanex.customerSetting.controller('fanex.customerSetting.customerInformationController',
    [
        '$scope',
        '$rootScope',
        '$filter',
        function ($scope, $rootScope, $filter) {
            $scope.model = $rootScope.model.customerInformation;
            return {
                getScope: function () {
                    return $scope;
                }
            }
        } ]);
}