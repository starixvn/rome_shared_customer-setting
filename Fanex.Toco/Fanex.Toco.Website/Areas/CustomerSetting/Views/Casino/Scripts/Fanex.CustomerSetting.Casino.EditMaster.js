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

Fanex.CustomerSetting.Casino.EditMaster = {
    SettingModel: {},
    CustomerModel: {},
    IsMultiple: false,
    CustomerInfo: { id: 1373580, uplineId: 1373389, roleId: 3, uplineRoleId: 4, userId: 1373389, username: 'pgmAndy' },
    CustIds: [],
    Usernames: [],
    Enabled: false,
    ErrorMessage: '',

    Init: function (id, uplineId, roleId, uplineRoleId, userId, username, isMultiple, custIds, usernames) {
        this.CustomerInfo.id = id;
        this.CustomerInfo.uplineId = uplineId;
        this.CustomerInfo.roleId = roleId;
        this.CustomerInfo.uplineRoleId = uplineRoleId;
        this.CustomerInfo.userId = userId;
        this.CustomerInfo.username = username;
        this.CustomerModel = Fanex.CustomerSetting.Casino.CustomerModel;

        if (isMultiple) {
            this.IsMultiple = true;

            if (!custIds) {
                alert('Please input custIds parameter is array');
            }
            else {
                this.CustIds = custIds;
            }

            if (!usernames) {
                alert('Please input usernames parameter is array');
            }
            else {
                this.Usernames = usernames;
            }
        }

        if (this.IsMultiple) {
            $('#userInfo').remove();

            var usernames = '';
            $(this.Usernames).each(function (index, username) {
                usernames += username + ', ';

                usernames = usernames.substring(0, usernames.length - 2);
            });

            $('#names').html(usernames);
            $('#names').show();
        }
        else {
            $('#arrowUpDown').remove();
            $('#statusInfo').remove();
        }

        var level = $('#downline').html();
        var timeout = setTimeout(function () {
            clearTimeout(timeout);
            Fanex.CustomerSetting.Casino.EditMaster.Enabled = $('#chkEnable').is(':checked');
        }, 2000);

        $('#chkEnable').bind('click', function () {
            if (this.checked) {
                if (!Fanex.CustomerSetting.Casino.EditMaster.Enabled) {
                    $('#chkEnableDownline').show();
                    $('#enableDownlineText').show();
                }

                $('#downline').html(level);
            }
            else {
                $('#chkEnableDownline').hide();
                $('#enableDownlineText').hide();

                if (Fanex.CustomerSetting.Casino.EditMaster.Enabled) {
                    $('#downline').html(level + '&nbsp;' + "<font color='Red'>(Casino downlines will be disabled as well)</font>");
                }
            }
        });

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
                var comboMasterMax = $('#pt_3' + subid);
                var max = parseFloat(comboSuperPrest.attr('max')) - parseFloat(comboSuperPrest.val());

                if (checkbox.checked) {
                    comboSuperPrest.attr('disabled', true);
                    comboSuperAutoPreset.attr('disabled', false);

                    comboMasterMax.attr('max', comboSuperAutoPreset.attr('max'));
                }
                else {
                    comboSuperPrest.attr('disabled', false);
                    comboSuperAutoPreset.attr('disabled', true);

                    comboMasterMax.attr('max', max.toFixed(2));
                    comboSuperPrest.click();
                }

                if (subid.length == 2 && isPtByGameType) {
                    // Process checking for each game type
                    for (var j = 0; j < checkboxes.length; j++) {
                        var chk = checkboxes[j];
                        var curSubId = chk.id.substring(9);

                        comboSuperPrest = $('#pt_1' + curSubId);
                        comboSuperAutoPreset = $('#pt_2' + curSubId);
                        comboMasterMax = $('#pt_3' + curSubId);

                        if (curSubId.length > 2 && curSubId.substring(0, 2) == subid && checkbox.checked) {
                            chk.checked = true;

                            comboSuperPrest.attr('disabled', true);
                            comboSuperAutoPreset.attr('disabled', false);

                            comboMasterMax.attr('max', comboSuperAutoPreset.attr('max'));
                        }
                        else if (curSubId.length > 2 && curSubId.substring(0, 2) == subid && !checkbox.checked) {
                            chk.checked = false;
                            comboSuperPrest.attr('disabled', false);
                            comboSuperAutoPreset.attr('disabled', true);

                            max = parseFloat(comboSuperPrest.attr('max')) - parseFloat(comboSuperPrest.val())
                            comboMasterMax.attr('max', max.toFixed(2));
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

            // Rerender pt values constraint when comboboxes is clicked
            $(selects[i]).click(function () {
                var select = this;
                var subid = select.id.substring(4);
                var value = parseFloat(select.value);

                if (select.id.indexOf('pt_1') != -1) {
                    // Change master max pt                    
                    var comboMax = $('#pt_3' + subid)[0];
                    var maxMax = parseFloat(select.getAttribute('max')) - value;
                    var curMax = parseFloat(comboMax.value);

                    curMax = curMax > maxMax ? maxMax : curMax;
                    curMax = parseFloat(curMax.toFixed(2)) * 100 / 100;

                    PT_RenderCombo(comboMax, curMax, curMax, 0.01, curMax);
                    comboMax.setAttribute('max', maxMax.toFixed(2));

                    var comboMin = $('#pt_4' + subid)[0];
                    var maxMin = parseFloat(comboMax.getAttribute('max'));
                    var curMin = parseFloat(comboMin.value);

                    curMin = curMin > maxMin ? maxMin : curMin;
                    curMin = parseFloat(curMin.toFixed(2)) * 100 / 100;

                    PT_RenderCombo(comboMin, curMin, curMin, 0.01, curMin);
                    comboMin.setAttribute('max', maxMin.toFixed(2));
                }
                else if (select.id.indexOf('pt_3') != -1) {
                    // Master max pt is changing then change master min pt also
                    var combo = $('#pt_4' + subid)[0];
                    var max = parseFloat(select.value); // M3
                    var curMin = parseFloat(combo.value); // N3
                    curMin = curMin > max ? max : curMin;

                    PT_RenderCombo(combo, curMin, curMin, 0.01, curMin);
                    combo.setAttribute('max', max.toFixed(2));
                }
            });

            // Handle changing pt and comm values
            $(selects[i]).change(function () {
                var select = this;
                var value = parseFloat(select.value);
                var subid = select.id.substring(4);

                if (select.id.indexOf('pt_1') != -1) {
                    // Change master max pt                    
                    var comboMax = $('#pt_3' + subid)[0];
                    var maxMax = parseFloat(select.getAttribute('max')) - value;
                    var curMax = parseFloat(comboMax.value);

                    curMax = curMax > maxMax ? maxMax : curMax;
                    curMax = parseFloat(curMax.toFixed(2)) * 100 / 100;

                    PT_RenderCombo(comboMax, curMax, curMax, 0.01, curMax);
                    comboMax.setAttribute('max', maxMax.toFixed(2));

                    var comboMin = $('#pt_4' + subid)[0];
                    var maxMin = parseFloat(comboMax.getAttribute('max'));
                    var curMin = parseFloat(comboMin.value);

                    curMin = curMin > maxMin ? maxMin : curMin;
                    curMin = parseFloat(curMin.toFixed(2)) * 100 / 100;

                    PT_RenderCombo(comboMin, curMin, curMin, 0.01, curMin);
                    comboMin.setAttribute('max', maxMin.toFixed(2));

                    if (isPtByGameType) {
                        // Change pt (S3, M3, N3) for each game type
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeSuperPtByGameType(selects[j], subid);
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeMasterMaxPtByGameType(selects[j], subid);
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeMasterMinPtByGameType(selects[j], subid);
                        }
                    }
                }
                else if (select.id.indexOf('pt_2') != -1) {
                    // Super auto pt is changing
                    if (isPtByGameType) {
                        // Change pt (Us3) for each game type
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeSuperAutoPtByGameType(selects[j], subid);
                        }
                    }
                }
                else if (select.id.indexOf('pt_3') != -1) {
                    // Master max pt is changing then change master min pt also
                    var combo = $('#pt_4' + subid)[0];
                    var max = parseFloat(select.value); // M3
                    var curMin = parseFloat(combo.value); // N3
                    curMin = curMin > max ? max : curMin;

                    PT_RenderCombo(combo, curMin, curMin, 0.01, curMin);
                    combo.setAttribute('max', max.toFixed(2));

                    if (isPtByGameType) {
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeMasterMaxPtByGameType(selects[j], subid);
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeMasterMinPtByGameType(selects[j], subid);
                        }
                    }
                }
                else if (select.id.indexOf('pt_4') != -1) {
                    if (isPtByGameType) {
                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeMasterMinPtByGameType(selects[j], subid);
                        }
                    }
                }
                else if (select.id.indexOf('comm_5') != -1) {
                    if (isPtByGameType) {
                        subid = select.id.substring(6);

                        for (var j = 0; j < selects.length; j++) {
                            Fanex.CustomerSetting.Casino.EditMaster.ChangeMasterCommByGameType(selects[j], subid);
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

    ChangeMasterMaxPtByGameType: function (select, subid) {
        if (select.id.indexOf('pt_3' + subid) != -1) {
            // Change all M3
            var curSubid = select.id.substring(4);
            var cur = parseFloat($('#pt_3' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                PT_RenderCombo(select, cur, cur, 0.01, cur);

                document.getElementById('pt_3' + curSubid).click();

                // Check to show warning asterisk for M3
                if (isWarning) {
                    $('#lbl_pt_3' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_pt_3' + curSubid).attr('class', 'hideWarningPT');
                }
            }
        }
    },

    ChangeMasterMinPtByGameType: function (select, subid) {
        if (select.id.indexOf('pt_4' + subid) != -1) {
            // Change all N3
            var curSubid = select.id.substring(4);
            var cur = parseFloat($('#pt_4' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                PT_RenderCombo(select, cur, cur, 0.01, cur);

                // Check to show warning asterisk for N3
                if (isWarning) {
                    $('#lbl_pt_4' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_pt_4' + curSubid).attr('class', 'hideWarningPT');
                }
            }
        }
    },

    ChangeMasterCommByGameType: function (select, subid) {
        if (select.id.indexOf('comm_5' + subid) != -1) {
            // Change all N3
            var curSubid = select.id.substring(6);
            var cur = parseFloat($('#comm_5' + subid).val());
            var min = parseFloat(select.getAttribute('min'));
            var max = parseFloat(select.getAttribute('max'));
            var isWarning = cur > max;
            cur = cur > max ? max : cur;

            if (curSubid.length > 2) {
                Comm_RenderCombo(select, cur, cur, 0.0005, cur);

                // Check to show warning asterisk for Master commission
                if (isWarning) {
                    $('#lbl_comm_5' + curSubid).attr('class', 'showWarningPT');
                }
                else {
                    $('#lbl_comm_5' + curSubid).attr('class', 'hideWarningPT');
                }
            }
        }
    },

    Update: function () {
        Fanex.CustomerSetting.Core.BlockUI();

        var gameGroups = this.SettingModel.gameGroups;
        var customer = this.CustomerModel;

        // Assign customer info
        customer.casinoDisabled = !$('#chkEnable').is(':checked');
        customer.maxTransferCredit = $('#maxTransfer').val();
        customer.isAutoReset = $('#isAutoReset').is(':checked');

        // Assign setting values
        $(gameGroups).each(function (i, gameGroup) {
            // Assign game group pt and commission
            gameGroup.s3 = $('#pt_1_' + i).val();
            gameGroup.is3 = $('#chkAutoPT_' + i).is(':checked');
            gameGroup.us3 = $('#pt_2_' + i).val();
            gameGroup.m3 = $('#pt_3_' + i).val();
            gameGroup.n3 = $('#pt_4_' + i).val();
            gameGroup.masterCommission = $('#comm_5_' + i).val();

            // Assign game type pt and commission
            var gameTypes = gameGroup.gameTypes;
            $(gameTypes).each(function (j, gameType) {
                gameType.s3 = $('#pt_1_' + i + '_' + j).val();
                gameType.is3 = $('#chkAutoPT_' + i + '_' + j).is(':checked');
                gameType.us3 = $('#pt_2_' + i + '_' + j).val();
                gameType.m3 = $('#pt_3_' + i + '_' + j).val();
                gameType.n3 = $('#pt_4_' + i + '_' + j).val();
                gameType.masterCommission = $('#comm_5_' + i + '_' + j).val();

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
            id: custIdIndex == null ? this.CustomerInfo.id : Fanex.CustomerSetting.Casino.EditMaster.CustIds[custIdIndex],
            uplineId: this.CustomerInfo.uplineId,
            roleId: this.CustomerInfo.roleId,
            uplineRoleId: this.CustomerInfo.uplineRoleId,
            userId: this.CustomerInfo.userId,
            username: this.CustomerInfo.username,
            settingModel: JSON.stringify(this.SettingModel),
            customerModel: JSON.stringify(this.CustomerModel)
        };

        $.ajax({
            url: toUrl('Master/UpdateMasterPost'),
            data: data,
            type: 'post',
            dataType: 'json'
        })
        .success(function (result) {
            if (!result.isSuccess) {
                Fanex.CustomerSetting.Casino.EditMaster.ErrorMessage += result.errorMessage.formatErrorMessage() + '<br />';
            }

            if (!Fanex.CustomerSetting.Casino.EditMaster.IsMultiple) {
                $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMaster.ErrorMessage);
                $('#errMsg').show();
                Fanex.CustomerSetting.Casino.EditMaster.ErrorMessage = '';
                Fanex.CustomerSetting.Core.UnBlockUI();
            }
            else {
                if (custIdIndex == Fanex.CustomerSetting.Casino.EditMaster.CustIds.length - 1) {
                    $('#errMsg').html(Fanex.CustomerSetting.Casino.EditMaster.ErrorMessage);
                    $('#errMsg').show();
                    Fanex.CustomerSetting.Casino.EditMaster.ErrorMessage = '';
                    Fanex.CustomerSetting.Core.UnBlockUI();
                }
                else {
                    Fanex.CustomerSetting.Casino.EditMaster.ProcessUpdate(custIdIndex++);
                }
            }
        })
        .error(function (data) {
            if (typeof console != 'undefined') {
                $('#errMsg').html(data.responseText);
                $('#errMsg').show();
            }

            Fanex.CustomerSetting.Core.UnBlockUI();
        });
    }
};

$(document).ready(function () {
    //This line is for testing only, if called in another project, please call as below
    Fanex.CustomerSetting.Casino.EditMaster.Init(1373580, 1373389, 3, 4, 1373389, 'pgmAndy');
});