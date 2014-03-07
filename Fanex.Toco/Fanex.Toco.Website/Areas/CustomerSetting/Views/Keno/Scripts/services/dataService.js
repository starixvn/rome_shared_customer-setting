"use strict";

KenoModule.service('dataService', function () {
    var createCustomerInfo = function (roleId) {
        return {
            "custId": 122222,
            "roleId": roleId,
            "firstName": "Vu",
            "lastName": "Tran",
            "phone": "---",
            "mobilePhone": "---",
            "fax": "---",
            "username": "Super001"
        };
    };

    var createBetSetting = function (betTypeId, betValue, isEnabled, isDownlinesEnabled) {
        return {
            "betTypeId": betTypeId,
            "betTypeName": "Beijing" + betTypeId,
            "minBet": betValue,
            "maxBet": betValue * 10,
            "maxPerMatch": betValue * 100,
            "isEnabled": isEnabled,
            "isBetSettingUpdated": false,
            "isDownlinesEnabled": isDownlinesEnabled,
            "upline": {
                "minBet": betValue - 1,
                "maxBet": (betValue + 1) * 10,
                "maxPerMatch": (betValue + 1) * 100,
                "isEnabled": isEnabled,
                "isDownlinesEnabled": isDownlinesEnabled
            }
        };
    };

    var createBetSettingList = function () {
        var betSettings = [];

        for (var i = 1; i < 6; i++) {
            var betSettingTmp = createBetSetting(i, 100 * i, true, false);
            betSettings.push(betSettingTmp);
        }

        return betSettings;
    };


    var createPositionTaking = function (betTypeId, boolValue1, boolValue2, betTypeName, roleId) {
        var positionTaking = {
            "sportTypeId": betTypeId,
            "betTypeId": 202,
            "betTypeName": betTypeName,
            "s4": 0.9,
            "m4": 0.6,
            "a4": 0.4,
            "n4": 0.1,
            "l4": 0.9,
            "sComm": 0.0095,
            "sCommMax": 0.01,
            "s3": 0.1,
            "m3": 0.6,
            "uS3": 0.5,
            "iS3": boolValue1,
            "n3": 0.1,
            "l3": 0.8,
            "mComm": 0.0075,
            "m2": 0.2,
            "a2": 0.4,
            "uM2": 0.45,
            "iM2": boolValue1,
            "n2": 0.1,
            "l2": 0.6,
            "aComm": 0.0045,
            "a1": 0.5,
            "comm": 0.0035,
            "s1": 0.2,
            "uS1": 0.4,
            "iS1": boolValue2,
            "m1": 0.1,
            "uM1": 0.2,
            "iM1": boolValue2
        };

        switch (roleId) {
            case 4:
                positionTaking.isUpdatePT = false;
                positionTaking.isUpdateS4 = false;
                positionTaking.isUpdateN4 = false;
                positionTaking.isUpdateSCo = false;
                break;
            case 3:
                positionTaking.isUpdatePT = false;
                positionTaking.IsUpdateS3 = false;
                positionTaking.IsUpdateUS3 = false;
                positionTaking.IsUpdateN3 = false;
                positionTaking.IsUpdateMComm = false;
                positionTaking.IsUpdateAllMember = false;
                positionTaking.NewMemberPT = 0;
                break;
            case 2:
                positionTaking.IsDisabledMPT = false;
                positionTaking.isUpdatePT = false;
                positionTaking.IsUpdateM2 = false;
                positionTaking.IsUpdateUM2 = false;
                positionTaking.IsUpdateN2 = false;
                positionTaking.IsUpdateACO = false;
                positionTaking.IsUpdateAllMember = false;
                positionTaking.NewMemberPT = 0;
                break;
            default:
                break;
        };

        return positionTaking;
    };

    var createPositionTakingList = function (roleId) {
        var positionTakings = [];

        for (var i = 1; i < 6; i++) {
            positionTakings.push(createPositionTaking(i, true, true, "bettypeName" + i, roleId));
        }

        return positionTakings;
    };

    this.getKenoSetting = function (roleId) {
        var kenoSetting = {};
        kenoSetting.customerInformation = createCustomerInfo(roleId);
        kenoSetting.betSettings = createBetSettingList();
        kenoSetting.positionTakings = createPositionTakingList(roleId);
        return kenoSetting;
    };
});