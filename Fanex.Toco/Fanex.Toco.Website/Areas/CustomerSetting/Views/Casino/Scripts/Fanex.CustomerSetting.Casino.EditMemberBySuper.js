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

Fanex.CustomerSetting.Casino.EditMemberBySuper = {
    SettingModel: {},
    CustomerModel: {},
    IsMultiple: false,
    CustomerInfo: { id: 1373580, uplineId: 1373389, userId: 1373389, username: 'pgmAndy' },
    CustIds: [],
    Usernames: [],
    Enabled: false,
    ErrorMessage: '',
    Data: {},

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

        $('#chkEnable').attr('disabled', true);
        $('#isAutoReset').attr('disabled', true);
        $('#maxTransfer').attr('disabled', true);

        var model = this.SettingModel;
        var selects = $('select');
        var checkboxes = $('input[name=chkAutoPT]');
        var isPtByGameType = model.isPtByGameType;

        // Add events for is auto pt
        for (var i = 0; i < checkboxes.length; i++) {
            $(checkboxes[i]).click(function () {
                var checkbox = this;
                var subid = checkbox.id.substring(9);
                var comboSuperPrest = $('#pt_1' + subid);
                var comboSuperAutoPreset = $('#pt_2' + subid);
                var max = parseFloat(comboSuperPrest.attr('max')) - parseFloat(comboSuperPrest.val());

                if (checkbox.checked) {
                    comboSuperPrest.attr('disabled', true);
                    comboSuperAutoPreset.attr('disabled', false);
                }
                else {
                    comboSuperPrest.attr('disabled', false);
                    comboSuperAutoPreset.attr('disabled', true);

                    comboSuperPrest.click();
                }

                if (subid.length == 2 && isPtByGameType) {
                    // Process checking for each game type
                    for (var j = 0; j < checkboxes.length; j++) {
                        var chk = checkboxes[j];
                        var curSubId = chk.id.substring(9);

                        comboSuperPrest = $('#pt_1' + curSubId);
                        comboSuperAutoPreset = $('#pt_2' + curSubId);

                        if (curSubId.length > 2 && curSubId.substring(0, 2) == subid && checkbox.checked) {
                            chk.checked = true;

                            comboSuperPrest.attr('disabled', true);
                            comboSuperAutoPreset.attr('disabled', false);
                        }
                        else if (curSubId.length > 2 && curSubId.substring(0, 2) == subid && !checkbox.checked) {
                            chk.checked = false;
                            comboSuperPrest.attr('disabled', false);
                            comboSuperAutoPreset.attr('disabled', true);

                            comboSuperPrest.click();
                        }
                    }
                }
            });
        }

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
                        // Change pt (S3, M3, N3) for each game type
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMemberBySuper.ChangeSuperPtByGameType(selects[j], subid);
                        }
                    }
                }
                else if (select.id.indexOf('pt_2') != -1) {
                    // Super auto pt is changing
                    if (isPtByGameType) {
                        // Change pt (Us3) for each game type
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMemberBySuper.ChangeSuperAutoPtByGameType(selects[j], subid);
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

    ChangeSuperPtByGameType: function (select, subid) {
        if (select.id.indexOf('pt_1' + subid) != -1) {
            // Change all S3
            var curSubid = select.id.substring(4);
            var cur = parseFloat($('#pt_1' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                PT_RenderCombo(select, cur, cur, 0.01, cur);

                document.getElementById('pt_1' + curSubid).click();

                // Check to show warning asterisk for S3
                if (isWarning) {
                    $('#lbl_pt_1' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_pt_1' + curSubid).attr('class', 'hideWarningPT');
                }
            }
        }
    },

    ChangeSuperAutoPtByGameType: function (select, subid) {
        if (select.id.indexOf('pt_2' + subid) != -1) {
            // Change all Us3
            var curSubid = select.id.substring(4);
            var cur = parseFloat($('#pt_2' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                PT_RenderCombo(select, cur, cur, 0.01, cur);

                document.getElementById('pt_2' + curSubid).click();

                // Check to show warning asterisk for Us3
                if (isWarning) {
                    $('#lbl_pt_2' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_pt_2' + curSubid).attr('class', 'hideWarningPT');
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
        Fanex.CustomerSetting.Casino.EditMemberBySuper.Enabled = customer.enableDownline = $('#chkEnableDownline').is(':checked');

        // Assign setting values
        $(gameGroups).each(function (i, gameGroup) {
            // Assign game group pt and commission
            gameGroup.s3 = $('#pt_1_' + i).val();
            gameGroup.is3 = $('#chkAutoPT_' + i).is(':checked');
            gameGroup.us3 = $('#pt_2_' + i).val();

            // Assign game type pt and commission
            var gameTypes = gameGroup.gameTypes;
            $(gameTypes).each(function (j, gameType) {
                gameType.s3 = $('#pt_1_' + i + '_' + j).val();
                gameType.is3 = $('#chkAutoPT_' + i + '_' + j).is(':checked');
                gameType.us3 = $('#pt_2_' + i + '_' + j).val();

                // Assign betting limit
                var bettingLimits = gameType.bettingLimits;
                $(bettingLimits).each(function (k, stakeLevel) {
                    var id = '#stake-' + stakeLevel.stakeLevelID;
                    stakeLevel.isChecked = $(id).is(':checked');
                });
            });
        });

        this.Data = {
            id: this.CustomerInfo.id,
            uplineId: this.CustomerInfo.uplineId,
            userId: this.CustomerInfo.userId,
            username: this.CustomerInfo.username,
            settingModel: JSON.stringify(this.SettingModel),
            customerModel: JSON.stringify(this.CustomerModel)
        };

        if (this.IsMultiple) {
            this.ProcessUpdate(0);
        }
        else {
            this.ProcessUpdate(null);
        }
    },

    ProcessUpdate: function (custIdIndex) {
        var data = this.Data;
        data.id = custIdIndex == null ? this.CustomerInfo.id : Fanex.CustomerSetting.Casino.EditMember.CustIds[custIdIndex];

        $.ajax({
            url: toUrl('Member/UpdateMemberBySuperPost'),
            data: data,
            type: 'post',
            dataType: 'json'
        })
        .success(function (result) {
            if (result.errorCode != 0) {
                Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage += result.errorMessage.formatErrorMessage() + '<br />';
            }

            if (!Fanex.CustomerSetting.Casino.EditMemberBySuper.IsMultiple) {
                if (Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage != '') {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage);
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return false;
                }
                else {
                    $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return true;
                }
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMemberBySuper.CustIds.length - 1) {
                    if (Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage != '') {
                        $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage);
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return false;
                    }
                    else {
                        $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return true;
                    }
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMemberBySuper.ProcessUpdate(custIdIndex++);
                }
            }
        })
        .error(function (response) {
            if (typeof console != 'undefined') {
                console.log(response);
            }

            Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage += ('Not casino user: ' + Fanex.CustomerSetting.Casino.EditMemberBySuper.Usernames[custIdIndex]).formatErrorMessage() + '<br />';

            if (!Fanex.CustomerSetting.Casino.EditMemberBySuper.IsMultiple) {
                if (Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage != '') {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage);
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return false;
                }
                else {
                    $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return true;
                }
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMemberBySuper.CustIds.length - 1) {
                    if (Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage != '') {
                        $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage);
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return false;
                    }
                    else {
                        $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberBySuper.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return true;
                    }
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMemberBySuper.ProcessUpdate(++custIdIndex);
                }
            }
        });
    }
};

$(document).ready(function () {
    //This line is for testing only, if called in another project, please call as below
    Fanex.CustomerSetting.Casino.EditMemberBySuper.Init(1400567, 1373389, 1373389, 'pgmAndy'/*, true, [1, 2], ['a', 'b']*/);
});