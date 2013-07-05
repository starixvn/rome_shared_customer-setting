/// <reference path="_references.js" />

if (typeof $ == 'undefined' || typeof jQuery == 'undefined') {
    alert("Require jQuery.js");
}

if (typeof Fanex == 'undefined') {
    Fanex = {};
}

if (typeof Fanex.CustomerSetting == 'undefined') {
    Fanex.CustomerSetting = {};
}

Fanex.CustomerSetting.Core = {
    $this: null,
    BlockedUI: false,

    Init: function () {
        $this = this;
        $.blockUI.defaults.css.border = '0px solid #aaa';
        $.blockUI.defaults.css.backgroundColor = "transparent";
        $.blockUI.defaults.overlayCSS.backgroundColor = "transparent";
    },

    BlockUI: function () {
        if (!$this.BlockedUI) {
            $.blockUI({
                message: '<img src="/Areas/CustomerSetting/Images/Loading.gif" />',
                onBlock: function () {
                    $this.BlockedUI = true;
                },
                onUnblock: function () {
                    $this.BlockedUI = false;
                }
            });
        }
    },

    UnBlockUI: function () {
        $.unblockUI(document);
    },

    DecodeHtml: function (str) {
        return $("<div/>").html(str).text();
    },

    EncodeHtml: function (str) {
        return $('<div/>').text(str).html();
    },

    FormatNumber: function (number) { // Format a number to 1,234,567.89
        number += '';

        x = number.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    },

    ReplaceAll: function (src, args, replace) {
        while (src.indexOf(args) != -1) {
            src = src.replace(args, replace);
        }

        return src;
    },

    CheckNumber: function (evt, ele) {
        ele.value = Fanex.CustomerSetting.Core.FormatNumber(Fanex.CustomerSetting.Core.ReplaceAll(ele.value, ',', ''));

        var charCode = (evt.which) ? evt.which : event.keyCode;
        var ret = true;

        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            ret = false;
        }

        if (ele && ele.value.length > 1 && ele.value.indexOf('0') == 0) {
            while (ele.value.indexOf('0') == 0 && ele.value.length > 1) {
                ele.value = ele.value.substring(1);
                ele.value = Fanex.CustomerSetting.Core.FormatNumber(Fanex.CustomerSetting.Core.ReplaceAll(ele.value, ',', ''));
            }
        }
        else if (ele && ele.value.length == 0) {
            ele.value = '0';
        }

        return ret;
    }
}

String.prototype.toFixed = function (precision, optionals) {
    var value = parseFloat(this);

    var power = Math.pow(10, precision),
            output;

    // Multiply up by precision, round accurately, then divide and use native toFixed():
    output = (Math.round(value * power) / power).toFixed(precision);

    if (optionals) {
        var optionalsRegExp = new RegExp('0{1,' + optionals + '}$');
        output = output.replace(optionalsRegExp, '');
    }

    return output;
}

String.prototype.formatErrorMessage = function () {
    var msg = this;

    var indexOfStartParenthesis = msg.indexOf('(');
    var indexOfEndParenthesis = msg.indexOf(')');
    var orgMsg = msg.substring(indexOfStartParenthesis, indexOfEndParenthesis + 1);
    var preMsg = msg.substring(0, indexOfStartParenthesis);
    var subMsg = msg.substring(indexOfEndParenthesis + 1, msg.length);

    var tmp = msg.substring(indexOfStartParenthesis, indexOfStartParenthesis + 4);
    var hasUsername = preMsg.indexOf(':') != -1;
    var username = '';

    if (hasUsername) {
        username = preMsg.substring(0, preMsg.indexOf(':'));
    }

    var formatMsg = '<font color="#ff5411">' + preMsg + '</font>' + orgMsg +
    '<font color="#ff5411">' + subMsg + '</font>';

    var msg = '';

    if (!hasUsername) {
        msg = orgMsg + subMsg.indexOf(':') > 0 ? ('<div style="float: left; color: Blue; font-weight: bold">' + subMsg.substring(0, subMsg.indexOf(':')) + ':</div><br /><font color="#ff5411">' + subMsg.substring(subMsg.indexOf(':') + 1) + '</font>') : ('<font color="#ff5411">' + subMsg + '</font>');
    }
    else {
        msg = '<div style="float: left; color: Blue; font-weight: bold">' + username + ':</div><br /><font color="#ff5411">' + preMsg.substring(preMsg.indexOf(':') + 1, preMsg.length) + orgMsg + subMsg + '</font>';
    }

    var ret = preMsg.length > 0 && !hasUsername ? formatMsg : msg;

    return ret;
};

$(document).ready(function () {
    Fanex.CustomerSetting.Core.Init();
});