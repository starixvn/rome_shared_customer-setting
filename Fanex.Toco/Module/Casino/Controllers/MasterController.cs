using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.ExternalAccount.Service.Impl.Customer.Casino;
using Fanex.ExternalAccount.DTO.Customer.Casino;
using System.Collections.Generic;
using Fanex.CustomerSetting.Module.Casino.ViewModels;

namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    public class MasterController : BaseController
    {
        public ActionResult EditMaster(int id, int uplineId, int roleId, int uplineRoleId)
        {
            var customerSetting = CasinoManager.Instance.MasterCasinoModel(id, uplineId);

            return PartialView("~/Areas/CustomerSetting/Views/Casino/Master/EditMaster.cshtml", customerSetting);
        }

        private List<GameKindDetail> GetGameKinds(int custId, int uplineId)
        {
            var custInfo = CasinoManager.Instance.GetCustInfoIBC(custId, uplineId);
            var gameKinds = new List<GameKindDetail>();
                        
            if (!custInfo.CasinoSync)
            {
                gameKinds = CasinoManager.Instance.GetGameKinds(uplineId);
            }
            else
            {
                gameKinds = CasinoManager.Instance.GetGameKinds(custId);
            }

            return gameKinds;
        }
    }
}