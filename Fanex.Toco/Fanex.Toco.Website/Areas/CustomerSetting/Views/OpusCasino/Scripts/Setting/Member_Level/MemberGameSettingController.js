OpusModule.controller('memberGameSettingController', function ($scope) {
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
            if ($scope.gameSettingList[i].stakeChoosen.iD != $scope.orignalGameSettingList[i].stakeChoosen.iD) {
                $scope.broadcastOpusSetting(false);
                return;
            }
        }
        $scope.broadcastOpusSetting(true);
    };

    $scope.$watch(function () {

        return;
    });

});
