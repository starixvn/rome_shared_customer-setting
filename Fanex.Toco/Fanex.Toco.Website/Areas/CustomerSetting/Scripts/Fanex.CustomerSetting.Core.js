if (typeof Fanex == 'undefined') {
    Fanex = {};
}

if (typeof Fanex.CustomerSetting == 'undefined') {
    Fanex.CustomerSetting = {};
}

Fanex.CustomerSetting.Core = {

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