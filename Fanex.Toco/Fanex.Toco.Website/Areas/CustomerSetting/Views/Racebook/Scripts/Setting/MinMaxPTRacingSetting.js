var minMaxPTRacingSetting = {
    roleId: {
        master: 3,
        agent: 2,
        masterMinMax: -4,
        agentMinMax: -5
    },
    currentSetting: {},
    initialize: function () {
        this.getMinMaxPTSetting();
    },
    getMinMaxPTSetting: function () {
        var param = {
            custId: window.custId,
            roleId: window.targetRoleId
        };

        $.ajax({
            type: 'POST',
            url: 'GetMinMaxPTSetting',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(param),
            beforeSend: function (jqXHR, settings) {

            },
            success: function (data, textStatus, jqXHR) {
                minMaxPTRacingSetting.currentSetting = JSON.parse(data);
                minMaxPTRacingSetting.initializeHtml(minMaxPTRacingSetting.currentSetting);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Init context error.');
            },
            complete: function (jqXHR, textStatus) {

            }
        });
    },
    initializeHtml: function (data) {
        if (data.RacingSportType == null || data.RacingSportType.length === 0) {
            alert('no data found');
            return;
        }
        var $container = $('#SettingContent');
        $container.html('')
            .append(this.initCutomerInformationSection(data.CustInfo))
            .append(this.initCopyAllPT(data))
            .append(this.initRacingSportType(data.RacingSportType));
        
        AssignCommission();
        AssignPT();
        /*if (Roles.CurrRole == Roles.SuperOnMember || Roles.CurrRole == Roles.MasterOnMember) {
            RacingSetting.validCopyAll();
        }*/
    },
    initCutomerInformationSection: function (customerInfo) {
        return '<table class="tblcollapse w1000 marginT6"><tbody>\
            <tr class="title1">\
                <td colspan="4">' + this.getRoleNameById(customerInfo.CustLevel) + ': ' + customerInfo.UserName + '</td>\
            </tr>\
            <tr>\
                <td width="20%" class="l">FirstName:</td>\
                <td width="30%" class="l">' + customerInfo.FirstName + '</td>\
                <td width="20%" class="l">Last Name:</td>\
                <td width="30%" class="l">' + customerInfo.LastName + '</td>\
            </tr>\
            <tr>\
                <td class="l">Phone:</td>\
                <td class="l">' + customerInfo.Phone + '</td>\
                <td class="l">Mobile Phone:</td>\
                <td class="l">' + customerInfo.MobilePhone + '</td>\
            </tr>\
            <tr>\
                <td class="l">Fax:</td>\
                <td class="l">' + customerInfo.Fax + '</td>\
                <td></td>\
                <td></td>\
            </tr>\
            </tbody></table>';
    },
    initCopyAllPT: function (data) {
        var html = '<div class="l"><input type="checkbox" onclick="minMaxPTRacingSetting.chkCopyAllSetting(this)" id="chkCopyAllSetting">&nbsp;<label for="chkCopyAllSetting"><b>Copy all '
            + (data.CustInfo.CustLevel > 0 ? 'Commission & ' : '') + 'Position Taking from ' + data.DefaultRacingSportType + ' - Win</b></label></div>';
        if (!data.IsHideCopyBetSetting) {
            html += '<div class="l"><input type="checkbox" onclick="ChkCopyAllBetSetting(this)" id="chkCopyAllBetting">&nbsp;<b>Copy all Bet Setting from ' + data.DefaultRacingSportType + ' - Win</b></div>';
        }
        html += '<div class="l"></div>';
        return html;
    },
    initRacingSportType: function (racingBox) {
        if (!racingBox || racingBox.length === 0) {
            return '';
        }
        var html = '';
        $(racingBox).each(function (index, sportType) {
            html += '<div class="l ' + (!sportType.Visible ? 'hidden' : '') + '"><table class="tblcollapse w1000 marginT6"><tbody>\
            <tr class="title1">\
                <td class="l"><span class="expanded" target="setting_' + sportType.Id + '">' + sportType.Name + '</span></td>\
            </tr>';
            if (sportType.Totes && sportType.Totes.length > 0) {
                html += '<tr id="setting_' + sportType.Id + '"><td class="l"><div>';
                $(sportType.Totes).each(function (ind, betType) {
                    html += minMaxPTRacingSetting.initRacingBetType(sportType.Id, betType);
                });
                html += '</div></td></tr>';
            }
            html += '</tbody></table></div>';
        });
        return html;
    },
    initRacingBetType: function (sportId, racingBetType) {
        var html = '<div class="' + (!racingBetType.Visible ? 'hidden' : '') + '"><table cellspacing="0" cellpadding="0" border="0" class="tblPTheader"><tbody>\
            <tr id="btntop" style="cursor: pointer;">\
                <td class="btnleft"></td>\
                     <td width="120" class="l btnmiddle" target="setting_' + racingBetType.Id + '" >\
                        <span class="mp">' + racingBetType.Name + '</span>\
                    </td>\
                <td width="20" class="setting_' + racingBetType.Id + ' r ' + (racingBetType.IsTop ? 'md2' : 'md3') + '"></td>\
            </tr></tbody></table>\
            <div id="' + racingBetType.Id + '_errmsg" class="w800 errmsg"></div>';
        if (racingBetType.Bettypes && racingBetType.Bettypes.length > 0) {
            html += '<div id="setting_' + racingBetType.Id + '" class="l" style="' + (!racingBetType.IsTop ? 'display:none;' : '') + '">';
            if (window.targetRoleId === minMaxPTRacingSetting.roleId.masterMinMax) {
                html += masterMinMaxPT.initMinMaxPTSetting(sportId, racingBetType);
            }
            else if (window.targetRoleId === minMaxPTRacingSetting.roleId.agentMinMax) {
                html += agentMinMaxPT.initMinMaxPTSetting(sportId, racingBetType);
            }
            html += '</div>';
        }
        html += '</div>'
        return html;
    },
    getRoleNameById: function (roleId) {
        var roleName = '';
        if (roleId === minMaxPTRacingSetting.roleId.master) {
            roleName = 'Master';
        }
        else if (roleId === minMaxPTRacingSetting.roleId.agent) {
            roleName = 'Agent';
        }
        return roleName;
    },
    // events handling
    chkCopyAllSetting: function (checkbox) {
        var isCopied = $(checkbox).prop('checked');
        var sportId;
        var winId;
        var placeId;
        var chkCopyFromWin = 'chkCopyFromWin';
        for (var i = 0; i < this.currentSetting.RacingSportType.length; i++) {
            sportId = this.currentSetting.RacingSportType[i].Id;
            for (var j = 0; j < this.currentSetting.RacingSportType[i].Totes.length; j++) {
                var obj = this.currentSetting.RacingSportType[i].Totes[j];
                winId = sportId + '_' + obj.Bettypes[0].Id;
                placeId = sportId + '_' + obj.Bettypes[1].Id;
                if (obj.Bettypes.length > 0 && (winId == this.currentSetting.DefaultRacingSportTypeId)) {
                    if (isCopied) {
                        $('#' + placeId + '_minpt').attr('disabled', 'disabled');
                        $('#' + placeId + '_maxpt').attr('disabled', 'disabled');
                        $('#' + winId + '_' + chkCopyFromWin).attr('disabled', 'disabled');
                    }
                    else {
                        $('#' + winId + '_' + chkCopyFromWin).removeAttr('disabled');

                        if ($('#' + winId + '_' + chkCopyFromWin).prop('checked') === false) {
                            $('#' + placeId + '_minpt').removeAttr('disabled');
                            $('#' + placeId + '_maxpt').removeAttr('disabled');
                        }
                    }
                }
                else {
                    if (isCopied) {
                        $('#' + winId + '_' + chkCopyFromWin).attr('disabled', 'disabled');
                        $('#' + winId + '_minpt').attr('disabled', 'disabled');
                        $('#' + placeId + '_minpt').attr('disabled', 'disabled');
                        $('#' + winId + '_maxpt').attr('disabled', 'disabled');
                        $('#' + placeId + '_maxpt').attr('disabled', 'disabled');
                    }
                    else {
                        $('#' + winId + '_' + chkCopyFromWin).removeAttr('disabled');
                        $('#' + winId + '_minpt').removeAttr('disabled');
                        $('#' + winId + '_maxpt').removeAttr('disabled');

                        if ($('#' + winId + '_' + chkCopyFromWin).prop('checked') === false) {
                            $('#' + placeId + '_minpt').removeAttr('disabled');
                            $('#' + placeId + '_maxpt').removeAttr('disabled');
                        }
                    }
                }
            }
        }
    },
    chkCopyFromWin: function (obj) {
        var IdWin = $(obj).prop('id').split('_')[1];
        var IdPlace = $(obj).prop('id').split('_')[0] + '_' + minMaxPTRacingSetting.getIdPlace(IdWin);

        var isCopied = $(obj).prop('checked');
        if (isCopied) {
            $('#' + IdPlace + '_minpt').attr('disabled', 'disabled');
            $('#' + IdPlace + '_maxpt').attr('disabled', 'disabled');
        }
        else {
            $('#' + IdPlace + '_minpt').removeAttr('disabled');
            $('#' + IdPlace + '_maxpt').removeAttr('disabled');
        }
    },
    changeMaxPT: function (obj) {
        var IdWin = $(obj).prop('id').split('_')[0] + '_' + $(obj).prop('id').split('_')[1];
        var max = $(obj).val();
        var master_obj = $('#' + IdWin + '_' + 'minpt');

        max = Math.min(master_obj.attr('max'), max)
        var curr = Math.min(master_obj.val(), max);
        CreateOption(master_obj, curr, max, 0, 0.1, 2);
    },
    changeMinPT: function (obj) {
        var IdWin = $(obj).attr('id').split('_')[0] + '_' + $(obj).attr('id').split('_')[1];
        var min_of_max = $(obj).val();
        var max_obj = $('#' + IdWin + '_' + 'maxpt');
        var max_of_max = max_obj.attr('max');
        var cur_of_max = max_obj.val();
        CreateOption(max_obj, cur_of_max, max_of_max, min_of_max, 0.1, 2);
    },
    // helper methods
    getIdPlace: function (IdWin) {
        return parseInt(IdWin) + 1;
    },
    getId: function(index) {
        return this.parent.parent.data.Id + "_" + this.data.Bettypes[index].Id;
    }
};
$(function () {
    minMaxPTRacingSetting.initialize();
});