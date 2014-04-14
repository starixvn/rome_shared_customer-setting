"use strict";

var KenoModule = angular.module('kenoSettingModule', ['ngDialog']);

KenoModule.run(function ($templateCache) {
    $templateCache.put('betSettingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/BetSetting/BetSetting.htm');
    $templateCache.put('superPositionTakingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/PositionTaking/SuperPositionTaking.htm');
    $templateCache.put('masterPositionTakingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/PositionTaking/MasterPositionTaking.htm');
    $templateCache.put('agentPositionTakingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/PositionTaking/AgentPositionTaking.htm');
    $templateCache.put('memberPositionTakingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/PositionTaking/MemberPositionTaking.htm');
    $templateCache.put('superOnMemberPositionTakingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/PositionTaking/SuperOnMemberPositionTaking.htm');
    $templateCache.put('masterOnMemberPositionTakingTemplate', domain + 'Areas/CustomerSetting/Views/Keno/Setting/Views/Partial/PositionTaking/MasterOnMemberPositionTaking.htm');
});

KenoModule.directive('ieSelectFix', function ($timeout) {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attrs, ctrl) {
            attrs.$observe(
                "ieSelectFix",
                function (value) {
                    var isIE = document.attachEvent;
                    if (isIE) {
                        var control = element[0];
                        var option = document.createElement("option");
                        control.add(option, null);
                        control.remove(control.options.length - 1);

                        var tmp = angular.element('<option></option>');
                        element.append(tmp);
                        tmp.remove();
                    }
                });
        }
    }
});