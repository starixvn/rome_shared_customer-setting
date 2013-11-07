angular.module('SearchCopy.service', []).
 value('message', {
     // Success=0, Error=1, Warning=2
     showAlertMessage: function (containerid, msg, type) {
         $("#" + containerid).removeClass("hidden");
         switch (type) {
             case 0:
                 document.getElementById(containerid).innerHTML = "<div class='alertboxscc'><div>" + msg + "</div></div>";
                 break;
             case 1:
                 document.getElementById(containerid).innerHTML = "<div class='alertboxerr'><div>" + msg + "</div></div>";
                 break;
             case 2:
                 document.getElementById(containerid).innerHTML = "<div class='alertboxwrn'><div>" + msg + "</div></div>"; ;
                 break;
             default:
                 break
         }
     },
     hideAlertMessage: function (containerid) {
         $("#" + containerid).addClass("hidden");
     }
 }).
 value('PTSetting', {
     RemoveSportBookCustInfo: function () { 
         var $content = $("#frmSportbook").contents();
         for (var i = 0; i < 6; i++) {
             $content.find(".tblcollapse tr:eq(0)").remove();
         }
     },

     RemoveSportBookMinPT: function () {
         $('.tblcollapse').remove();
     },

     RemoveRacingCustInfo: function () {
         $('#tbCustomerInfo').remove();
     },

     RemoveRacingMinPT: function () {
         $('.tblcollapse').remove();
     },

     RemoveCasinoCustInfo: function () {
         for (var i = 0; i < 2; i++) {
             $('.tblcollapse tr:eq(0)').remove();
         }
     },

     RemoveVirtualSportsCustInfo: function () {
         $('.tblcollapse').remove();
     },

     RemoveBingoCustInfo: function () {
         for (var i = 0; i < 5; i++) {
             $('.tblcollapse tr:eq(0)').remove();
         }
     },

     RemoveLiveCasinoCustInfo: function () {
         for (var i = 0; i < 2; i++) {
             $('.tblcollapse tr:eq(0)').remove();
         }
     },

     RemoveCustInfoSportType: function (sportId) {
         switch (sportId) {
             case '1':
                 RemoveSportBookCustInfo();
                 break;
             case '2':
                 RemoveSportBookMinPT();
                 break;
             case '3':
                 RemoveRacingMinPT();
                 break;
             case '151':
                 RemoveRacingCustInfo();
                 break;
             case '162':
                 RemoveLiveCasinoCustInfo();
                 break;
             case '180':
                 RemoveVirtualSportsCustInfo();
                 break;
             case '251':
                 RemoveCasinoCustInfo();
                 break;
             case '252':
                 RemoveBingoCustInfo();
                 break;

         }
     }
 });

angular.module('SearchCopy.directive', []);
angular.module('SearchCopy.filter', []);