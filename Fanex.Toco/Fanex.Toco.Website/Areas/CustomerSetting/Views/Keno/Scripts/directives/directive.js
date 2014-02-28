fanex.customerSetting.directive('checkDisabledElement', function () {
    return {
        restrict: 'A',
        scope: {
            betSetting: '='
        },
        link: function (scope, element, attrs, ctrl) {
            var betSetting = scope.betSetting;
            scope.$watch(attrs.betSetting, function () {
                if (betSetting.isEnabled == false) {
                    element.attr("disabled", "disabled").removeAttr("checked", "checked");
                    betSetting.isDownlinesEnabled = false;
                }
                else {
                    element.removeAttr("disabled", "disabled");
                }
            }, true);
        }
    }
});