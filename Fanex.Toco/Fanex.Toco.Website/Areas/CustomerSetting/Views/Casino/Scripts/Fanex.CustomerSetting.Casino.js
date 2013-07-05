/// <reference path="../../../Scripts/_references.js" />
if (typeof $ == 'undefined' || typeof jQuery == 'undefined') {
    alert("Require jQuery.js");
}

if (typeof Fanex == 'undefined' || typeof Fanex.CustomerSetting == 'undefined') {
    alert("Require Fanex.CustomerSetting.Core.js");
};

Fanex.CustomerSetting.Casino = {
    CustomerModel: {},

    Init: function () {
        
    },

    RenderPT: function (pts) {
        var casinoGamePT = pts[0];
        var miniGamePT = pts[1];
        var uplineMin = 0;
        var uplineMax = 0;
        var uplinePreset = 0;
        var uplineAutoPreset = 0;
        var downlinePreset = 0;
        var downlinePreset2 = 0;
        var commMax = 0;
        var commPreset = 0;
        var max = 0;
        var synced = this.CustomerModel.CustomerModelInfo.casinoSync;

        // Renders Master-Agent PT
        //if (Fanex.CustomerSetting.CurrentLevel == Fanex.CustomerSetting.Master) {
        //PT Renderring for Casino Game
        //PT values
        uplineMax = synced ? casinoGamePT.maxPositionTakingUpline : casinoGamePT.maxPositionTaking;
        uplinePreset = synced ? casinoGamePT.sPositionTaking : casinoGamePT.sPositionTakingUpline;
        uplineAutoPreset = synced ? casinoGamePT.autoPositionTaking : casinoGamePT.autoPositionTakingUpline;
        downlinePreset = synced ? casinoGamePT.maxPositionTaking : casinoGamePT.maxPositionTakingUpline;
        downlinePreset2 = synced ? casinoGamePT.minPositionTaking : casinoGamePT.minPositionTakingUpline;
        commMax = synced ? casinoGamePT.sMAgentRollingRateUpline : casinoGamePT.sMAgentRollingRate;
        commPreset = synced ? casinoGamePT.mAgentRollingRate : casinoGamePT.sMAgentRollingRate;

        //Render super preset pt
        PT_RenderCombo(document.getElementById('pt_1'), uplineMin, uplineMax, 0.01, uplinePreset);
        //Render super auto preset pt
        PT_RenderCombo(document.getElementById('pt_2'), uplineMin, uplineMax, 0.01, uplineAutoPreset);
        if (!casinoGamePT.isAutoPositionTaking) {
            $('#pt_2').attr('disabled', 'disabled');
        }
        else {
            $('#pt_1').attr('disabled', 'disabled');
        }
        $('#chkAutoPT').attr('checked', casinoGamePT.isAutoPositionTaking);

        //Render Master Max PT
        PT_RenderCombo(document.getElementById('pt_3'), 0, (uplineMax - uplinePreset), 0.01, downlinePreset);

        //Render Master Min PT
        PT_RenderCombo(document.getElementById('pt_4'), 0, downlinePreset, 0.01, downlinePreset2);

        //Render Master Commission
        Comm_RenderCombo(document.getElementById('comm_5'), 0, commMax, 0.0005, commPreset);

        //PT Renderring for Mini Game
        //PT values
        uplineMax = synced ? miniGamePT.maxPositionTakingUpline : miniGamePT.maxPositionTaking;
        uplinePreset = synced ? miniGamePT.sPositionTaking : miniGamePT.sPositionTakingUpline;
        uplineAutoPreset = synced ? miniGamePT.autoPositionTaking : miniGamePT.autoPositionTakingUpline;
        downlinePreset = synced ? miniGamePT.maxPositionTaking : miniGamePT.maxPositionTakingUpline;
        downlinePreset2 = synced ? miniGamePT.minPositionTaking : miniGamePT.minPositionTakingUpline;
        commMax = synced ? miniGamePT.sMAgentRollingRateUpline : miniGamePT.sMAgentRollingRate;
        commPreset = synced ? miniGamePT.mAgentRollingRate : miniGamePT.sMAgentRollingRate;

        //Render super preset pt
        PT_RenderCombo(document.getElementById('pt_1_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render super auto preset pt
        PT_RenderCombo(document.getElementById('pt_2_1'), uplineMin, uplineMax, 0.01, uplineAutoPreset);
        if (!miniGamePT.isAutoPositionTaking) {
            $('#pt_2_1').attr('disabled', 'disabled');
        }
        else {
            $('#pt_1_1').attr('disabled', 'disabled');
        }
        $('#chkAutoPT_1').attr('checked', miniGamePT.isAutoPositionTaking);

        //Render Master Max PT
        PT_RenderCombo(document.getElementById('pt_3_1'), 0, (uplineMax - uplinePreset), 0.01, downlinePreset);

        //Render Master Min PT
        PT_RenderCombo(document.getElementById('pt_4_1'), 0, downlinePreset, 0.01, downlinePreset2);

        //Render Master Commission
        Comm_RenderCombo(document.getElementById('comm_5_1'), 0, commMax, 0.0005, commPreset);
        /*}
        else if (Fanex.CustomerSetting.CurrentLevel == Fanex.CustomerSetting.Agent) {
        //PT Renderring for Casino Game
        //PT values
        uplineMax = synced ? casinoGamePT.maxPositionTakingUpline : casinoGamePT.maxPositionTaking;
        uplinePreset = synced ? casinoGamePT.mPositionTaking : casinoGamePT.mPositionTakingUpline;
        downlinePreset = synced ? casinoGamePT.aPositionTaking : casinoGamePT.aPositionTakingUpline;
        downlinePreset2 = synced ? casinoGamePT.minPositionTaking : casinoGamePT.minPositionTakingUpline;
        commMax = synced ? casinoGamePT.mAgentRollingRateUpline : casinoGamePT.mAgentRollingRate;
        commPreset = synced ? casinoGamePT.agentRollingRate : casinoGamePT.mAgentRollingRate;

        if (!Fanex.CustomerSetting.isSuperEditAgent) {
        //Render Master preset pt
        PT_RenderCombo(document.getElementById('pt_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render Agent Max PT
        PT_RenderCombo(document.getElementById('pt_3'), 0, (uplineMax - uplinePreset), 0.01, downlinePreset);

        //Render Agent Min PT
        PT_RenderCombo(document.getElementById('pt_4'), 0, downlinePreset, 0.01, downlinePreset2);

        //Render Agent Commission
        Comm_RenderCombo(document.getElementById('comm_5'), 0, commMax, 0.0005, commPreset);
        }
        else {
        $('#chkEnable').attr('disabled', true);
        $('#maxTransfer').attr('disabled', true);
        $('#isAutoReset').attr('disabled', true);

        $('#mPT').html(casinoGamePT.mPositionTaking);
        $('#aPT').html(casinoGamePT.aPositionTaking);

        $('.buttonPopup').remove();
        }

        //PT Renderring for Mini Game
        //PT values
        uplineMax = synced ? miniGamePT.maxPositionTakingUpline : miniGamePT.maxPositionTaking;
        uplinePreset = synced ? miniGamePT.mPositionTaking : miniGamePT.mPositionTakingUpline;
        downlinePreset = synced ? miniGamePT.aPositionTaking : miniGamePT.aPositionTakingUpline;
        downlinePreset2 = synced ? miniGamePT.minPositionTaking : miniGamePT.minPositionTakingUpline;
        commMax = synced ? miniGamePT.mAgentRollingRateUpline : miniGamePT.mAgentRollingRate;
        commPreset = synced ? miniGamePT.agentRollingRate : miniGamePT.mAgentRollingRate;

        if (!Fanex.CustomerSetting.isSuperEditAgent) {
        //Render Master preset pt
        PT_RenderCombo(document.getElementById('pt_1_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render Agent Max PT
        PT_RenderCombo(document.getElementById('pt_3_1'), 0, (uplineMax - uplinePreset), 0.01, downlinePreset);

        //Render Agent Min PT
        PT_RenderCombo(document.getElementById('pt_4_1'), 0, downlinePreset, 0.01, downlinePreset2);

        //Render Agent Commission
        Comm_RenderCombo(document.getElementById('comm_5_1'), 0, commMax, 0.0005, commPreset);
        }
        else {
        $('#chkEnable').attr('disabled', true);
        $('#maxTransfer').attr('disabled', true);
        $('#isAutoReset').attr('disabled', true);

        $('#mPT_1').html(miniGamePT.mPositionTaking);
        $('#aPT_1').html(miniGamePT.aPositionTaking);
        }
        }
        else {
        // Renders member PT
        if (Fanex.CustomerSetting.isSuperEditMember) {
        $('#chkEnable').attr('disabled', true);
        $('#maxTransfer').attr('disabled', true);
        $('#isAutoReset').attr('disabled', true);

        //PT Renderring for Casino Game
        //PT values
        uplineMin = casinoGamePT.isAutoPositionTaking ? casinoGamePT.superMinPositionTaking - casinoGamePT.mPositionTaking - casinoGamePT.aPositionTaking : 0;
        uplineMax = casinoGamePT.superMaxPositionTaking - casinoGamePT.mPositionTaking - casinoGamePT.aPositionTaking;
        uplinePreset = casinoGamePT.sPositionTaking;
        uplineAutoPreset = casinoGamePT.autoPositionTaking;
        max = casinoGamePT.superMaxPositionTaking;

        uplineMin = uplineMin < 0 ? 0 : uplineMin;

        //Render Super preset pt
        PT_RenderCombo(document.getElementById('pt_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render super auto preset pt
        PT_RenderCombo(document.getElementById('pt_2'), uplineMin, max, 0.01, uplineAutoPreset);
        if (!casinoGamePT.isAutoPositionTaking) {
        $('#pt_2').attr('disabled', 'disabled');
        }
        else {
        $('#pt_1').attr('disabled', 'disabled');
        }
        $('#chkAutoPT').attr('checked', casinoGamePT.isAutoPositionTaking);

        //Render master preset PT
        $('#mPT').html(casinoGamePT.mPositionTaking);

        //Render agent preset PT
        $('#aPT').html(casinoGamePT.aPositionTaking);

        //PT Renderring for Mini Game
        //PT values
        uplineMin = miniGamePT.isAutoPositionTaking ? miniGamePT.superMinPositionTaking - miniGamePT.mPositionTaking - miniGamePT.aPositionTaking : 0;
        uplineMax = miniGamePT.superMaxPositionTaking - miniGamePT.mPositionTaking - miniGamePT.aPositionTaking;
        uplinePreset = miniGamePT.sPositionTaking;
        uplineAutoPreset = miniGamePT.autoPositionTaking;
        max = miniGamePT.superMaxPositionTaking;

        uplineMin = uplineMin < 0 ? 0 : uplineMin;

        //Render Super preset pt
        PT_RenderCombo(document.getElementById('pt_1_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render super auto preset pt
        PT_RenderCombo(document.getElementById('pt_2_1'), uplineMin, max, 0.01, uplineAutoPreset);
        if (!miniGamePT.isAutoPositionTaking) {
        $('#pt_2_1').attr('disabled', 'disabled');
        }
        else {
        $('#pt_1_1').attr('disabled', 'disabled');
        }
        $('#chkAutoPT_1').attr('checked', miniGamePT.isAutoPositionTaking);

        //Render master preset PT
        $('#mPT_1').html(miniGamePT.mPositionTaking);

        //Render agent preset PT
        $('#aPT_1').html(miniGamePT.aPositionTaking);
        }
        else if (Fanex.CustomerSetting.isMasterEditMember) {
        $('#chkEnable').attr('disabled', true);
        $('#maxTransfer').attr('disabled', true);
        $('#isAutoReset').attr('disabled', true);

        //PT Renderring for Casino Game
        //PT values
        uplineMax = casinoGamePT.masterMaxPositionTaking - casinoGamePT.aPositionTaking;
        uplinePreset = casinoGamePT.mPositionTaking;

        //Render Super preset pt
        PT_RenderCombo(document.getElementById('pt_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render agent preset PT
        $('#aPT').html(casinoGamePT.aPositionTaking);

        //PT Renderring for Mini Game
        //PT values
        uplineMax = miniGamePT.masterMaxPositionTaking - miniGamePT.aPositionTaking;
        uplinePreset = miniGamePT.mPositionTaking;

        //Render Super preset pt
        PT_RenderCombo(document.getElementById('pt_1_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render agent preset PT
        $('#aPT_1').html(miniGamePT.aPositionTaking);
        }
        else {
        //PT Renderring for Casino Game
        //PT values
        uplineMax = synced ? casinoGamePT.maxPositionTakingUpline : casinoGamePT.maxPositionTaking;
        uplinePreset = synced ? casinoGamePT.aPositionTaking : casinoGamePT.aPositionTakingUpline;
        commMax = synced ? casinoGamePT.agentRollingRateUpline : casinoGamePT.agentRollingRate;
        commPreset = synced ? casinoGamePT.memberRollingRate : casinoGamePT.agentRollingRate;

        uplineMin = uplineMax < 0 ? (synced ? casinoGamePT.minPositionTakingUpline : casinoGamePT.minPositionTaking) : uplineMin;
        uplineMax = uplineMax < 0 ? (synced ? casinoGamePT.aPositionTakingUpline : casinoGamePT.aPositionTaking) : uplineMax;

        //Render Master preset pt
        PT_RenderCombo(document.getElementById('pt_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render Agent Commission
        Comm_RenderCombo(document.getElementById('comm_2'), 0, commMax, 0.0005, commPreset);

        //PT Renderring for Mini Game
        //PT values
        uplineMax = synced ? miniGamePT.maxPositionTakingUpline : miniGamePT.maxPositionTaking;
        uplinePreset = synced ? miniGamePT.aPositionTaking : miniGamePT.aPositionTakingUpline;
        commMax = synced ? miniGamePT.agentRollingRateUpline : miniGamePT.agentRollingRate;
        commPreset = synced ? miniGamePT.memberRollingRate : miniGamePT.agentRollingRate;

        uplineMin = uplineMax < 0 ? (synced ? miniGamePT.minPositionTakingUpline : miniGamePT.minPositionTaking) : uplineMin;
        uplineMax = uplineMax < 0 ? (synced ? miniGamePT.aPositionTakingUpline : miniGamePT.aPositionTaking) : uplineMax;

        //Render Master preset pt
        PT_RenderCombo(document.getElementById('pt_1_1'), uplineMin, uplineMax, 0.01, uplinePreset);

        //Render Agent Commission
        Comm_RenderCombo(document.getElementById('comm_2_1'), 0, commMax, 0.0005, commPreset);
        }
        }*/
    }
}

function PT_Reduce(id, f) {
    var combo = document.getElementById(id);
    if (combo.disabled) return;

    var step = parseFloat(combo.getAttribute('step'));
    var roundPrec = step == 0.01 ? 100 : 10000;
    var prec = step == 0.01 ? 2 : 4;
    var cur = parseFloat(combo.value.toFixed(prec));    
    var option = document.createElement('option');
    var min = parseFloat(combo.getAttribute('min').toFixed(prec));
    var max = parseFloat(combo.getAttribute('max').toFixed(prec));
    var newValue = parseFloat((cur - step * f).toFixed(prec));
    var value = newValue < min ? min : newValue > max ? max : newValue;
    
    option.text = option.value = value * roundPrec / roundPrec;
    combo.length = 0;
    combo.options.add(option);

    $(combo).change();
}

function PT_Increase(id, f) {
    var combo = document.getElementById(id);
    if (combo.disabled) return;

    var step = parseFloat(combo.getAttribute('step'));
    var roundPrec = step == 0.01 ? 100 : 10000;
    var prec = step == 0.01 ? 2 : 4;
    var cur = parseFloat(combo.value.toFixed(prec));
    var option = document.createElement('option');
    var min = parseFloat(combo.getAttribute('min').toFixed(prec));
    var max = parseFloat(combo.getAttribute('max').toFixed(prec));
    var newValue = parseFloat((cur + step * f).toFixed(prec));
    var value = newValue < min ? min : newValue > max ? max : newValue;

    option.text = option.value = value * roundPrec / roundPrec;
    combo.length = 0;
    combo.options.add(option);

    $(combo).change();
}

function PT_RenderCombo(combo, min, max, step, selectedValue) {
    min = min > max ? max : min;

    if (selectedValue > max) {
        var opt = document.createElement("option");
        opt.text = opt.value = (selectedValue * 100) / 100;
        combo.options.add(opt);
    }

    var index = 0;
    var itemIndex = 0;

    if (!combo) return;
    combo.options.length = 0;

    var c = Math.round((max * 100 - min * 100) / (step * 100));

    if (c <= 0) {
        var opt = document.createElement("option");
        opt.text = opt.value = Math.round(min * 100) / 100;
        combo.options.add(opt);
        return;
    }

    for (var i = c; i >= 0; i--) {
        var f = Math.round(min * 100 + (i * step * 100)) / 100;
        var opt = document.createElement("option");
        if (selectedValue == f) {
            index = itemIndex;
            opt.selected = true;
        }

        opt.text = opt.value = f;
        combo.options.add(opt);
        itemIndex++;
    }


    if (selectedValue && selectedValue <= max) {
        if (document.all)
            combo.options[index].selected = true;
        else
            combo.value = selectedValue;
    }
}

function Comm_RenderCombo(combo, min, max, step, selectedValue) {
    min = min > max ? max : min;
    selectedValue = selectedValue > max ? max : selectedValue;

    var index = 0;
    var itemIndex = 0;

    if (!combo) return;
    combo.options.length = 0;

    var c = Math.round((max * 10000 - min * 10000) / (step * 10000));

    if (c <= 0) {
        var opt = document.createElement("option");
        opt.text = opt.value = Math.round(min * 10000) / 10000;
        combo.options.add(opt);
        return;
    }

    for (var i = c; i >= 0; i--) {
        var f = Math.round(min * 10000 + (i * step * 10000)) / 10000;
        var opt = document.createElement("option");

        if (selectedValue == f) {
            index = itemIndex;
            opt.selected = true;
        }

        opt.text = opt.value = f;
        combo.options.add(opt);
        itemIndex++;
    }

    if (selectedValue) {
        if (document.all)
            combo.options[index].selected = true;
        else
            combo.value = selectedValue;
    }
}

$(document).ready(function () {
    Fanex.CustomerSetting.Casino.Init();
});