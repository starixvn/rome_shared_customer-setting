/// <reference path="../../../Scripts/_references.js" />
if (typeof $ == 'undefined' || typeof jQuery == 'undefined') {
    alert("Require jQuery.js");
}

if (typeof Fanex == 'undefined' || typeof Fanex.CustomerSetting == 'undefined') {
    alert("Require Fanex.CustomerSetting.Core.js");
};

if (typeof Fanex.CustomerSetting.Casino == 'undefined') {
    alert("Require Fanex.CustomerSetting.Casino.js");
};

Fanex.CustomerSetting.Casino.EditMember = {
    SettingModel: {},
    CustomerModel: {},
    IsMultiple: false,
    CustomerInfo: { id: 1373580, uplineId: 1373389, userId: 1373389, username: 'pgmAndy' },
    CustIds: [],
    Usernames: [],
    Enabled: false,
    ErrorMessage: '',

    Init: function (id, uplineId, userId, username, isMultiple, custIds, usernames) {
        this.CustomerInfo.id = id;
        this.CustomerInfo.uplineId = uplineId;
        this.CustomerInfo.userId = userId;
        this.CustomerInfo.username = username;
        this.CustomerModel = Fanex.CustomerSetting.Casino.CustomerModel;

        if (isMultiple) {
            this.IsMultiple = true;

            if (!custIds || !(custIds instanceof Array)) {
                alert('Please input custIds parameter is array');
            }
            else {
                this.CustIds = custIds;
            }

            if (!usernames || !(usernames instanceof Array)) {
                alert('Please input usernames parameter is array');
            }
            else {
                this.Usernames = usernames;
            }
        }

        if (this.IsMultiple) {
            $('#userInfo').remove();

            var names = "";
            $(this.Usernames).each(function (index, username) {
                names += username + ", ";
            });

            Fanex.CustomerSetting.Core.ProcessDisplayMultiple(125, names);
            names = names.substring(0, names.length - 2);

            $('#names').html(names);
            $('#names').show();
        }
        else {
            $('#arrowUpDown').remove();
            $('#statusInfo').remove();
        }

        $('#downline').html('');
        var timeout = setInterval(function () {
            if ($('#chkEnable').get(0)) {
                clearInterval(timeout);
                Fanex.CustomerSetting.Casino.EditMember.Enabled = $('#chkEnable').is(':checked');
            }
        }, 100);

        var model = this.SettingModel;
        var selects = $('select');
        var isPtByGameType = model.isPtByGameType;

        // Add events for comboboxes
        for (var i = 0; i < selects.length; i++) {
            // Preload pt values when mouse is entered into comboboxes
            $(selects[i]).mouseenter(function () {
                var select = this;
                var min = parseFloat(select.getAttribute('min'));
                var max = parseFloat(select.getAttribute('max'));
                var step = parseFloat(select.getAttribute('step'));

                if (this.id.indexOf('pt') != -1 && select.options.length == 1) {
                    PT_RenderCombo(select, min, max, step, parseFloat(this.value));
                }
                else if (select.options.length == 1) {
                    Comm_RenderCombo(select, min, max, step, parseFloat(this.value));
                }
            });

            // Handle changing pt and comm values
            $(selects[i]).change(function () {
                var select = this;
                var value = parseFloat(select.value);
                var subid = select.id.substring(4);

                if (select.id.indexOf('pt_1') != -1) {
                    if (isPtByGameType) {
                        // Change pt (M2, A2, N2) for each game type
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMember.ChangeAgentPtByGameType(selects[j], subid);
                        }
                    }
                }
                else if (select.id.indexOf('comm_2') != -1) {
                    if (isPtByGameType) {
                        subid = select.id.substring(6);

                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMember.ChangeMemberCommByGameType(selects[j], subid);
                        }
                    }
                }
            });

            // Set only selected value into comboboxes when it is blurred
            $(selects[i]).blur(function () {
                var value = this.value;
                this.options.length = 0;
                var opt = document.createElement('option');
                opt.text = opt.value = value;
                this.options.add(opt);
            });
        }
    },

    ChangeAgentPtByGameType: function (select, subid) {
        if (select.id.indexOf('pt_1' + subid) != -1) {
            // Change all A2
            var curSubid = select.id.substring(4);
            var cur = parseFloat($('#pt_1' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                PT_RenderCombo(select, cur, cur, 0.01, cur);

                document.getElementById('pt_1' + curSubid).click();

                // Check to show warning asterisk for A2
                if (isWarning) {
                    $('#lbl_pt_1' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_pt_1' + curSubid).attr('class', 'hideWarningPT');
                }
            }
        }
    },

    ChangeMemberCommByGameType: function (select, subid) {
        if (select.id.indexOf('comm_2' + subid) != -1) {
            // Change all Aco
            var curSubid = select.id.substring(6);
            var cur = parseFloat($('#comm_2' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                Comm_RenderCombo(select, cur, cur, 0.0005, cur);

                // Check to show warning asterisk for agent commission
                if (isWarning) {
                    $('#lbl_comm_2' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_comm_2' + curSubid).attr('class', 'hideWarningPT');
                }
            }
        }
    },

    Update: function () {
        var gameGroups = this.SettingModel.gameGroups;
        var customer = this.CustomerModel;

        // Assign customer info
        var min = parseFloat(Fanex.CustomerSetting.Core.ReplaceAll($('#minCredit').html(), ',', ''));
        var max = parseFloat(Fanex.CustomerSetting.Core.ReplaceAll($('#maxCredit').html(), ',', ''));
        var cur = parseFloat(Fanex.CustomerSetting.Core.ReplaceAll($('#maxTransfer').val(), ',', ''));

        if (isNaN(cur) || cur < min || cur > max) {
            Validators.show($('#maxTransfer').get(0), 'Member Max Transfer Credit must be between ' + $('#minCredit').html() + ' and ' + $('#maxCredit').html(), undefined, undefined, 1500);
            return;
        }

        Fanex.CustomerSetting.Core.BlockUI();

        customer.casinoDisabled = !$('#chkEnable').is(':checked');
        customer.maxTransferCredit = $('#maxTransfer').val();
        customer.isAutoReset = $('#isAutoReset').is(':checked');
        Fanex.CustomerSetting.Casino.EditMember.Enabled = customer.enableDownline = $('#chkEnableDownline').is(':checked');

        // Assign setting values
        $(gameGroups).each(function (i, gameGroup) {
            // Assign game group pt and commission
            gameGroup.m1 = $('#pt_1_' + i).val();
            gameGroup.co = $('#comm_2_' + i).val();

            // Assign game type pt and commission
            var gameTypes = gameGroup.gameTypes;
            $(gameTypes).each(function (j, gameType) {
                gameType.m1 = $('#pt_1_' + i + '_' + j).val();
                gameType.co = $('#comm_2_' + i + '_' + j).val();

                // Assign betting limit
                var bettingLimits = gameType.bettingLimits;
                $(bettingLimits).each(function (k, stakeLevel) {
                    var id = '#stake-' + stakeLevel.stakeLevelID;
                    stakeLevel.isChecked = $(id).is(':checked');
                });
            });
        });

        if (this.IsMultiple) {
            this.ProcessUpdate(0);
        }
        else {
            this.ProcessUpdate(null);
        }
    },

    ProcessUpdate: function (custIdIndex) {
        var data = {
            id: custIdIndex == null ? this.CustomerInfo.id : Fanex.CustomerSetting.Casino.EditMember.CustIds[custIdIndex],
            uplineId: this.CustomerInfo.uplineId,
            userId: this.CustomerInfo.userId,
            username: this.CustomerInfo.username,
            settingModel: JSON.stringify(this.SettingModel),
            customerModel: JSON.stringify(this.CustomerModel)
        };

        $.ajax({
            url: toUrl('Member/UpdateMemberPost'),
            data: data,
            type: 'post',
            dataType: 'json'
        })
        .success(function (result) {
            if (result.errorCode != 0) {
                Fanex.CustomerSetting.Casino.EditMember.ErrorMessage += result.errorMessage.formatErrorMessage() + '<br />';
            }

            if (!Fanex.CustomerSetting.Casino.EditMember.IsMultiple) {
                if (Fanex.CustomerSetting.Casino.EditMember.ErrorMessage != '') {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMember.ErrorMessage);
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return false;
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return true;
                }
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMember.CustIds.length - 1) {
                    if (Fanex.CustomerSetting.Casino.EditMember.ErrorMessage != '') {
                        $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMember.ErrorMessage);
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return false;
                    }
                    else {
                        Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return true;
                    }
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMember.ProcessUpdate(custIdIndex++);
                }
            }
        })
        .error(function (response) {
            if (typeof console != 'undefined') {
                console.log(response);
            }

            Fanex.CustomerSetting.Casino.EditMember.ErrorMessage += ('Not casino user: ' + Fanex.CustomerSetting.Casino.EditMember.Usernames[custIdIndex]).formatErrorMessage() + '<br />';

            if (!Fanex.CustomerSetting.Casino.EditMember.IsMultiple) {
                if (Fanex.CustomerSetting.Casino.EditMember.ErrorMessage != '') {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMember.ErrorMessage);
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return false;
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return true;
                }
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMember.CustIds.length - 1) {
                    if (Fanex.CustomerSetting.Casino.EditMember.ErrorMessage != '') {
                        $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMember.ErrorMessage);
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return false;
                    }
                    else {
                        Fanex.CustomerSetting.Casino.EditMember.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return true;
                    }
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMember.ProcessUpdate(++custIdIndex);
                }
            }
        });
    }
};

$(document).ready(function () {
    //This line is for testing only, if called in another project, please call as below
    Fanex.CustomerSetting.Casino.EditMember.Init(1400567, 1374738, 1374738, 'pgmAndy'/*, true, [1, 2], ['a', 'b']*/);
});