var app = angular.module('OpusCasino', []).run(['$http', '$rootScope', function ($http, $rootScope) {
    $rootScope.getRangeValue = function (max, stepValue, roundValue) {
        var rangeValueArray = new Array();
        while (max >= 0) {
            rangeValueArray.push(max);
            max = Math.round((max - stepValue) * roundValue) / roundValue;
        }
        return rangeValueArray;
    }
} ]);

app.controller('customerInfoController', function ($scope, $rootScope) {
    $scope.customerInfo = model.customerInformation;
    $scope.isDownlineDisabled = true;

    $scope.isGenerateDisable = true;

    $scope.$watch('customerInfo.isEnable', function () {
        $scope.isDownlineDisabled = $scope.customerInfo.isEnable == true ? true : false;

        return;
    }, true);

});

app.controller('positionTakinglController', function ($scope, $rootScope) {
    $scope.positionTaking = model.positionTaking;
    $scope.isUpdatedPT = true;

    $scope.maxPTRange = $rootScope.getRangeValue(1, 0.01, 100);
    $scope.positionTaking.l4 = $scope.maxPTRange[0];
    $scope.positionTaking.n4 = $scope.positionTaking.l4;

    $scope.commRange = $rootScope.getRangeValue($scope.positionTaking.commMax, 0.0005, 10000);
    $scope.positionTaking.comm4 = $scope.commRange[0];

    $scope.$watch('positionTaking.l4', function () {
        if ($scope.positionTaking.l4 <= $scope.positionTaking.n4) {
                $scope.positionTaking.n4 = $scope.positionTaking.l4;
        }
        $scope.minPTRange = $rootScope.getRangeValue($scope.positionTaking.l4, 0.01, 100);
        return;
    }, true);

});
