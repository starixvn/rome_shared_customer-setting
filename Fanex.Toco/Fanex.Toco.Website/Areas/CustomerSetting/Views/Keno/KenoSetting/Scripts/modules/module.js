"use strict";

var fanex = fanex || {};

fanex.customerSetting = angular.module('fanex.customerSetting', []).run(['$http', '$rootScope', function ($http, $rootScope, $compile) {
    $rootScope.model = model;

    for (var i = 0; i < $rootScope.model.betSettings.length; i++) {
        var betSetting = $rootScope.model.betSettings[i];
        fanex.customerSetting.betSettingService.setAction(betSetting, $rootScope.model.formType);
    }

    for (var i = 0; i < $rootScope.model.positiongTakingSections.length; i++) {
        var positionTaking = $rootScope.model.positiongTakingSections[i].positionTaking;
        positionTaking.rangeOfMax = fanex.customerSetting.commonService.getRangeValue(positionTaking.upline.max, 0, 0.01, 100);
        positionTaking.rangeOfMin = fanex.customerSetting.commonService.getRangeValue(positionTaking.max, 0, 0.01, 100);
        positionTaking.rangeOfPreset = fanex.customerSetting.commonService.getRangeValue(positionTaking.preset, 0, 0.01, 100);
        fanex.customerSetting.positionTakingService.setAction(positionTaking, $rootScope.model.formType);

        var commissionSetting = $rootScope.model.positiongTakingSections[i].commissionSetting;
        commissionSetting.range = fanex.customerSetting.commonService.getRangeValue(commissionSetting.upline.value, 0, 0.0001, 10000);
        fanex.customerSetting.commissionSettingService.setAction(commissionSetting, $rootScope.model.formType);
    }
} ]);