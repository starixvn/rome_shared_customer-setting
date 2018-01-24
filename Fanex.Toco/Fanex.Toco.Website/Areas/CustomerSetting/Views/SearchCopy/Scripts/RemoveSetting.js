var PTSetting = PTSetting || {};
PTSetting.SportType = new function () {

    var delayTime = 500;

    var RemoveButtons = function (frameId) {
        var $content = $(frameId).contents();
        setTimeout(function () {
            $content.find('.maincontent_header').remove();
            $content.find('table:has(td.custBtn1)').remove();
        }, delayTime);

        $content.find("#divShowInfo").remove();
    };

    var RemoveSportBookCustInfo = function () {
        var $content = $("#frmSportbook").contents();
        for (var i = 0; i < 6; i++) {
            $content.find(".tblcollapse tr:eq(0)").remove();
        }
        //disable edit
        setTimeout(function () {
            $content.find('.BettingLimitTextBox4').attr('readonly', 'readonly');
            $content.find('.BettingLimitTextBox5').attr('readonly', 'readonly');
            $content.find('.selectcomm').attr('disabled', 'disabled');
            $content.find('.PTSelect').attr('disabled', 'disabled');
        }, delayTime);
    };

    var RemoveSportBookMinPT = function () {
        var $content = $("#frmSportbookMinPT").contents();
        for (var i = 0; i < 3; i++) {
            $content.find('.tblcollapse tr:eq(0)').remove();
        }
        //disable edit
        setTimeout(function () {
            $content.find('.PTSelect').attr('disabled', true);
            $content.find('input:button').attr('disabled', true);
        }, delayTime);

    };

    var RemoveCasinoCustInfo = function () {
        var $content = $("#frmCasino").contents();
        for (var i = 0; i < 2; i++) {
            $content.find('.tblcollapse tr:eq(0)').remove();
        }
        $content.find('.maincontent_box').remove();

        //disable edit
        setTimeout(function () {
            $content.find('input:checkbox').attr('disabled', true);
            $content.find('input:text').attr('readonly', 'readonly');
            $content.find('select').attr('disabled', true);
            $content.find('a').css('display', 'none');
        }, delayTime);
    };

    var RemoveVirtualSportsCustInfo = function () {
        var $content = $("#frmVirtualSports").contents();
        $content.find('.tblcollapse tr:eq(0)').remove();
        //disable edit
        setTimeout(function () {
            $content.find('.BettingLimitTextBox4').attr('readonly', 'readonly');
            $content.find('.PTSelect').attr('disabled', true);
            $content.find('.btnUpdate').attr('disabled', true);
        }, delayTime);
    };

    this.SetFrameLoaded = function () {
        var tabId = $("#listSports").val();

        //remove settings
        switch (tabId) {
            case '1': RemoveSportBookCustInfo(); RemoveButtons('#frmSportbook'); break;
            case '2': RemoveSportBookMinPT(); RemoveButtons('#frmSportbookMinPT'); break;
            case '5': RemoveCasinoCustInfo(); RemoveButtons('#frmCasino'); break;
            case '6': RemoveVirtualSportsCustInfo(); RemoveButtons('#frmVirtualSports'); break;
        }
    }
};