app.controller('sMAGameSettingController', function ($scope) {
    $scope.gameSettingList = model.gameSettingList;
    $scope.orignalGameSettingList = angular.copy(model.gameSettingList); 

    $scope.isDisabledSaveGameSetting = true;

    $scope.broadcastOpusSetting = function (value) {
        $scope.isDisabledSaveGameSetting = value;
        $scope.$emit('gameSetting', $scope.isDisabledSaveGameSetting);
    };

    $scope.changeStatusCheckbox = function () {
        var gameSettingLength = $scope.gameSettingList.length;
        for (var i = 0; i < gameSettingLength; i++) {
            var betlimitLength = $scope.gameSettingList[i].bettingLimitList.length;
            for (var j = 0; j < betlimitLength; j++) {
                var oldValue = $scope.orignalGameSettingList[i].bettingLimitList[j].isChecked;
                var newValue = $scope.gameSettingList[i].bettingLimitList[j].isChecked;
                if (oldValue != newValue) {
                    $scope.broadcastOpusSetting(false);
                    return;
                }
            }
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.$watch(function () {

        return;
    });

});
