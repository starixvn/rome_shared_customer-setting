angular.module('enableStatusApp', []);

angular.element($('#ng-app')).ready(function () {
    $('#ng-app').show();
});

function EnableStatusController($scope) {
    $scope.model = model;
    $scope.isDisabledAll = true;

    var length = $scope.model.entities.length;
    for (var i = 0; i < length; i++) {
        if (!$scope.model.entities[i].disabledUplines) {
            $scope.isDisabledAll = false;
            break;
        }
    }

    $scope.Init = function () {
        var allChecked = true;
        for (var i = 0; i < $scope.model.entities.length; i++) {
            if ((!$scope.model.entities[i].disabled && $scope.model.entities[i].disabledUplines) || $scope.isDisabledAll) {
                allChecked = false;
                break;
            }
        }

        if (allChecked)
            document.getElementById('chkAll').checked = true;
    };
}