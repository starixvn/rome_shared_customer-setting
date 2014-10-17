var PTSetting = PTSetting || {};
PTSetting.SportType = new function () {

    var RemoveButtons = function (frameId) {
        var $content = $(frameId).contents();
        $content.find('.maincontent_header').css('display', 'none');
        $content.find('table:has(td.custBtn1)').css('display', 'none');
        $content.find("#divShowInfo").css('display', 'none');
    };

    var RemoveSportBookCustInfo = function () {
        var $content = $("#frmSportbook").contents();
        for (var i = 0; i < 6; i++) {
            $content.find(".tblcollapse tr:eq(0)").css('display', 'none');
        }
        //disable edit
        $content.find('.BettingLimitTextBox4').attr('readonly', 'readonly');
        $content.find('.BettingLimitTextBox5').attr('readonly', 'readonly');
        $content.find('.selectcomm').attr('disabled', 'disabled');
        $content.find('.PTSelect').attr('disabled', 'disabled');
    };

    var RemoveSportBookMinPT = function () {
        var $content = $("#frmSportbookMinPT").contents();
        for (var i = 0; i < 3; i++) {
            $content.find('.tblcollapse tr:eq(0)').css('display', 'none');
        }
        //disable edit
        $content.find('.PTSelect').attr('disabled', true);
        $content.find('input:button').attr('disabled', true);
    };

    var RemoveRacingCustInfo = function () {
        var $content = $("#frmRacing").contents();
        $content.find('#divCustInfo').css('display', 'none');
        //disable edit
        $content.find('input:checkbox').attr('disabled', true);
        $content.find('input:text').attr('disabled', true);
        $content.find('select').attr('disabled', true);
    };

    var RemoveRacingMinPT = function () {
        var $content = $("#frmRacingMinPT").contents();

        $content.find('.maincontent_box').css('display', 'none');
        $content = $content.find('#SettingContent');
        $content.find('.tblcollapse')[0].css('display', 'none');

        //disable edit
        $content.find('input:checkbox').attr('disabled', true);
        $content.find('select').attr('disabled', true);
    };

    var RemoveCasinoCustInfo = function () {
        var $content = $("#frmCasino").contents();
        for (var i = 0; i < 2; i++) {
            $content.find('.tblcollapse tr:eq(0)').css('display', 'none');
        }
        $content.find('.maincontent_box').css('display', 'none');

        //disable edit
        $content.find('input:checkbox').attr('disabled', true);
        $content.find('input:text').attr('readonly', 'readonly');
        $content.find('select').attr('disabled', true);
        $content.find('a').css('display', 'none');
    };

    var RemoveVirtualSportsCustInfo = function () {
        var $content = $("#frmVirtualSports").contents();
        $content.find('.tblcollapse tr:eq(0)').css('display', 'none');
        //disable edit
        $content.find('.BettingLimitTextBox4').attr('readonly', 'readonly');
        $content.find('.PTSelect').attr('disabled', true);
        $content.find('.btnUpdate').attr('disabled', true);
    };

    var RemoveBingoCustInfo = function () {
        var $content = $("#frmBingo").contents();
        for (var i = 0; i < 5; i++) {
            $content.find('.tblcollapse tr:eq(0)').css('display', 'none');
        }
        $content.find('.maincontent_box').css('display', 'none');
        $content.find('#tblHideUpdate').css('display', 'none');

        //disable edit
        $content.find('input:checkbox').attr('disabled', true);
        $content.find('input:text').attr('readonly', 'readonly');
        $content.find('select').attr('disabled', true);
        $content.find('.canchange').css('display', 'none');
    };

    var RemoveLiveCasinoCustInfo = function () {
        var $content = $("#frmLiveCasino").contents();
        for (var i = 0; i < 2; i++) {
            $content.find('.tblcollapse tr:eq(0)').css('display', 'none');
        }
        $content.find('.maincontent_box').css('display', 'none');
        //disable edit
        $content.find('input:checkbox').attr('disabled', true);
        $content.find('select').attr('disabled', true);
        $content.find('.btnUpdateSetting').css('display', 'none');
    };

    this.SetFrameLoaded = function () {
        var tabId = $("#listSports").val();

        //remove settings
        switch (tabId) {
            case '1': RemoveSportBookCustInfo(); RemoveButtons('#frmSportbook'); break;
            case '2': RemoveSportBookMinPT(); RemoveButtons('#frmSportbookMinPT'); break;
            case '3': RemoveRacingCustInfo(); RemoveButtons('#frmRacing'); break;
            case '4': RemoveRacingMinPT(); RemoveButtons('#frmRacingMinPT'); break;
            case '5': RemoveCasinoCustInfo(); RemoveButtons('#frmCasino'); break;
            case '6': RemoveVirtualSportsCustInfo(); RemoveButtons('#frmVirtualSports'); break;
            case '7': RemoveBingoCustInfo(); RemoveButtons('#frmBingo'); break;
            case '8': RemoveLiveCasinoCustInfo(); RemoveButtons('#frmLiveCasino'); break;
        }
    }
};