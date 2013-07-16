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
    combo.options.length = 0;

    if (selectedValue > max || combo.getAttribute('breakpt')) {
        if (!combo.getAttribute('breakpt')) {
            combo.setAttribute('breakpt', selectedValue);
        }
        else {
            selectedValue = combo.getAttribute('breakpt');
        }

        var opt = document.createElement("option");
        opt.text = opt.value = (selectedValue * 100) / 100;
        combo.options.add(opt);

        var opt = document.createElement("option");
        opt.text = opt.value = 0;

        combo.options.add(opt);

        return;
    }
    
    var index = 0;
    var itemIndex = 0;

    if (!combo) return;    

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