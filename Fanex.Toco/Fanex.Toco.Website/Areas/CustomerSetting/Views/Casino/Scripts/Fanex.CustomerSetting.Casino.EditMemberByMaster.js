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

Fanex.CustomerSetting.Casino.EditMemberByMaster = {
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
                        // Change pt (S3, M3, N3) for each game type
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMemberByMaster.ChangeMasterPtByGameType(selects[j], subid);
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

    ChangeMasterPtByGameType: function (select, subid) {
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

    Update: function () {
        var gameGroups = this.SettingModel.gameGroups;
        var customer = this.CustomerModel;

        Fanex.CustomerSetting.Core.BlockUI();

        customer.casinoDisabled = !$('#chkEnable').is(':checked');
        Fanex.CustomerSetting.Casino.EditMemberByMaster.Enabled = customer.enableDownline = $('#chkEnableDownline').is(':checked');

        // Assign setting values
        $(gameGroups).each(function (i, gameGroup) {
            // Assign game group pt and commission
            gameGroup.m3 = $('#pt_1_' + i).val();
            
            // Assign game type pt and commission
            var gameTypes = gameGroup.gameTypes;
            $(gameTypes).each(function (j, gameType) {
                gameType.m3 = $('#pt_1_' + i + '_' + j).val();
                
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
            url: toUrl('Member/UpdateMemberByMasterPost'),
            data: data,
            type: 'post',
            dataType: 'json'
        })
        .success(function (result) {
            if (result.errorCode != 0) {
                Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage += result.errorMessage.formatErrorMessage() + '<br />';
            }

            if (!Fanex.CustomerSetting.Casino.EditMemberByMaster.IsMultiple) {
                if (Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage != '') {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage);
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return false;
                }
                else {
                    $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return true;
                }
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMemberByMaster.CustIds.length - 1) {
                    if (Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage != '') {
                        $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage);
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return false;
                    }
                    else {
                        $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return true;
                    }
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMemberByMaster.ProcessUpdate(custIdIndex++);
                }
            }
        })
        .error(function (response) {
            if (typeof console != 'undefined') {
                console.log(response);
            }

            Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage += ('Not casino user: ' + Fanex.CustomerSetting.Casino.EditMemberByMaster.Usernames[custIdIndex]).formatErrorMessage() + '<br />';

            if (!Fanex.CustomerSetting.Casino.EditMemberByMaster.IsMultiple) {
                if (Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage != '') {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage);
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return false;
                }
                else {
                    $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                    $('#errMsg').show();

                    Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();

                    return true;
                }
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMemberByMaster.CustIds.length - 1) {
                    if (Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage != '') {
                        $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage);
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return false;
                    }
                    else {
                        $('#errMsg').html('<font color="Blue">Updated successfully</font>');
                        $('#errMsg').show();

                        Fanex.CustomerSetting.Casino.EditMemberByMaster.ErrorMessage = '';
                        Fanex.CustomerSetting.Core.UnBlockUI();

                        return true;
                    }
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMemberByMaster.ProcessUpdate(++custIdIndex);
                }
            }
        });
    }
};

$(document).ready(function () {
    //This line is for testing only, if called in another project, please call as below
    Fanex.CustomerSetting.Casino.EditMemberByMaster.Init(1400567, 1373580, 1373580, 'pgmAndy'/*, true, [1, 2], ['a', 'b']*/);
});