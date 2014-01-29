var OpusModule = angular.module('OpusCasino', []).run(['$http', '$rootScope', function ($http, $rootScope) {
    $rootScope.getRangeValue = function (max, stepValue, roundValue) {
        var rangeValueArray = new Array();
        while (max >= 0) {
            rangeValueArray.push(max);
            max = Math.round((max - stepValue) * roundValue) / roundValue;
        }
        return rangeValueArray;
    }
} ]);

OpusModule.controller('opusSettingController', function ($scope, $rootScope) {
    $scope.isDisabledUpdated = true;
    $scope.isDisabledSavedPT = true;
    $scope.isDisabledSaveBetSetting = true;
    $scope.isDisabledSaveGameSetting = true;
    $scope.isSaveCustomerinfo = true;

    $scope.$on('customerInfo', function (event, data) {
        $scope.isSaveCustomerinfo = data;
    });

    $scope.$on('positionTaking', function (event, data) {
        $scope.isDisabledSavedPT = data;
    });

    $scope.$on('betSetting', function (event, data) {
        $scope.isDisabledSaveBetSetting = data;
    });

    $scope.$on('gameSetting', function (event, data) {
        $scope.isDisabledSaveGameSetting = data;
    });

    $scope.$watch(function () {
        if (!$scope.isDisabledSavedPT || !$scope.isDisabledSaveBetSetting || !$scope.isDisabledSaveGameSetting || !$scope.isSaveCustomerinfo) {
            $scope.isDisabledUpdated = false;
            return;
        }
        $scope.isDisabledUpdated = true;
    });
});

OpusModule.directive('checkDisableElement', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                var className = attrs.ngClassName;
                scope.$eval(attrs.ngModel) == true ? element.attr("disabled", "disabled").addClass(className + "Disabled").removeClass(className) : element.removeAttr("disabled", "disabled").addClass(className).removeClass(className + "Disabled");
            }, true);
        }
    }
});

function validateNumber(event) {
    // Prevent shift key since its not needed
    if (event.shiftKey == true) {
        event.preventDefault();
    }
    // Allow Only: keyboard 0-9, numpad 0-9, backspace, tab, left arrow, right arrow, delete
    if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 9 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46) {
        // Allow normal operation
    } else {
        // Prevent the rest
        event.preventDefault();
    }
}
