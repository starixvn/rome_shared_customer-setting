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

app.controller('opusSettingController', function ($scope, $rootScope) {
  
});

