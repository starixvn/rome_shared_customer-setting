var PTSetting = PTSetting || {};
PTSetting.SportType = new function () {

    var RemoveButtons = function () {
        var $content = $("#frmSport").contents();
        $content.find('.maincontent_header').remove();
        $content.find('table:has(td.custBtn1)').remove();

        $content.find("#divShowInfo").remove();
    };

    var RemoveSportBookCustInfo = function () {
        var $content = $("#frmSport").contents();
        for (var i = 0; i < 6; i++) {
            $content.find(".tblcollapse tr:eq(0)").remove();
        }
    };

    var RemoveSportBookMinPT = function () {        
        var $content = $("#frmSport").contents();
        $content.find('.tblcollapse')[0].remove();
    };

    var RemoveRacingCustInfo = function () {
        var $content = $("#frmSport").contents();
        $content.find('#divCustInfo').remove();
    };

    var RemoveRacingMinPT = function () {
        
        setTimeout(function () {
            //something you want delayed
            var $content = $("#frmSport").contents();
            $content.find('.maincontent_box').remove();

            $content = $content.find('#SettingContent');
            $content.find('.tblcollapse')[0].remove();
        }, 100); // how long do you want the delay to be?         
    };

    var RemoveCasinoCustInfo = function () {
        var $content = $("#frmSport").contents();
        for (var i = 0; i < 2; i++) {
            $content.find('.tblcollapse tr:eq(0)').remove();
        }
        $content.find('.maincontent_box').remove();
    };

    var RemoveVirtualSportsCustInfo = function () {
        var $content = $("#frmSport").contents();
        $content.find('.tblcollapse')[0].remove();
    };

    var RemoveBingoCustInfo = function () {
        var $content = $("#frmSport").contents();
        for (var i = 0; i < 5; i++) {
            $content.find('.tblcollapse tr:eq(0)').remove();
        }
        $content.find('.maincontent_box').remove();
    };

    var RemoveLiveCasinoCustInfo = function () {
        var $content = $("#frmSport").contents();
        for (var i = 0; i < 2; i++) {
            $content.find('.tblcollapse tr:eq(0)').remove();
        }
        $content.find('.maincontent_box').remove();
    };

    this.SetFrameLoaded = function () {
        var tabId = '';
        $(".tabsports div").each(function () {
            if (this.className.indexOf('tab-active') > -1) {
                tabId = this.id;
            }
        });
        //remove settings
        switch (tabId) {
            case 'tabSportsbook': RemoveSportBookCustInfo(); break;
            case 'tabSportsbookMinPT': RemoveSportBookMinPT(); break;
            case 'tabRacing': RemoveRacingCustInfo(); break;
            case 'tabRacingMinPT': RemoveRacingMinPT(); break;
            case 'tabCasino': RemoveCasinoCustInfo(); break;
            case 'tabVirtualSports': RemoveVirtualSportsCustInfo(); break;
            case 'tabBingo': RemoveBingoCustInfo(); break;
            case 'tabLiveCasino': RemoveLiveCasinoCustInfo(); break;
        }

        RemoveButtons();
    }
};