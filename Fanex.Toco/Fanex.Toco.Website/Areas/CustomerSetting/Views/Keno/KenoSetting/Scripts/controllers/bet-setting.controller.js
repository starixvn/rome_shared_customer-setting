if (fanex.customerSetting) {
    fanex.customerSetting.controller('fanex.customerSetting.betSettingController',
    [
        '$scope',
        '$rootScope',
        '$filter',
        function ($scope, $rootScope, $filter) {
            $scope.model = $rootScope.model.betSettings;
            $scope.betSettingService = fanex.customerSetting.betSettingService;

            $scope.getChangedData = function () {
                return $scope.betSettingService.getChangedData($scope.model);
            }

            $scope.isValidData = function () {
                return $scope.betSettingService.isValidData($scope.model);
            };

            return {
                getScope: function () {
                    return $scope;
                }
            }
        } ]);
}