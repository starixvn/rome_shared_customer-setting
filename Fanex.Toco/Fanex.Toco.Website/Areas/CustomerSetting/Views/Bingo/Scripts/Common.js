function PTBlockUI() {
    if (parent.BlockUI) {
        try { parent.BlockUI(); } catch (e) { }
    }
    //Called inside a frame set (#centerFr)
    else {
        if (parent.parent.BlockUI) {
            parent.parent.BlockUI();
        }
    }
    //Called from popup
    if (opener) {
        if (opener.BlockUI) {
            opener.BlockUI();
        }
        opener.parent.BlockUI();
    }
}
function PTUnBlockUI() {
    if (parent.jQuery && parent.jQuery.unblockUI) {
        parent.jQuery.unblockUI();
    }
    //Called inside a frame set (#centerFr)
    if (parent.parent.jQuery && parent.parent.jQuery.unblockUI) {
        parent.parent.jQuery.unblockUI();
    }
    //Called from popup
    if (opener) {
        if (opener.parent.jQuery.unblockUI) {
            opener.parent.jQuery.unblockUI();
        }
    }
}
function uInteger(str) {
    str = str.replace(/^\s+|\s+$/g, '');
    return /^[0-9]+$/.test(str);
}
function IsSyncData(obj) {
    var tblUpdate = $("#tblUpdate");
    if (tblUpdate && obj.checked) {
        tblUpdate.css({ "opacity": "1", "filter": "alpha(opacity=100)" });
        tblUpdate.click(function () {
            updateBingoPTSetting();
        });
    }
    else {
        tblUpdate.css({ "opacity": "0.5", "filter": "alpha(opacity=50)" });
        tblUpdate.unbind('click');
    }
}
function updateBingoPTSetting() {
    var customer = customerPTBingoSetting.context;
    if (customer) {
        if (ptBingoCommon.isDirectDownline()) {
            var role = customer.roleId;
            switch (role) {
                case ptBingoCommon.superRoleId:
                    superBingoSetting.updatePT(customer);
                    break;

                case ptBingoCommon.masterRoleId:
                    masterBingoSetting.updatePT(customer);
                    break;

                case ptBingoCommon.agentRoleId:
                    agentBingoSetting.updatePT(customer);
                    break;

                case ptBingoCommon.memberRoleId:
                    memberBingoSetting.updatePT(customer);
                    break;

                default:
                    break;
            }
        }
        else {
            if (window.pageRole === ptBingoCommon.superRoleId) {
                superEditMemberBingoSetting.updatePT(customer);
            }
            else if (window.pageRole === ptBingoCommon.masterRoleId) {
                masterEditMemberBingoSetting.updatePT(customer);
            }
        }
    }
}