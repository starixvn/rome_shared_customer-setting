namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Keno controller.
    /// </summary>
    public class ColossusBetSettingController : ColossusBetSettingBaseController
    {
        private ColossusBetModel modelFake = new ColossusBetModel()
        {
            CustomerInformationPartial = new CustomerInformation()
            {
                Username = "master1",
                NickName = "bear",
                FirstName = "Tran",
                LastName = "Vu",
                Phone = "096414444",
                MobilePhone = "1234543463",
                Fax = "45645646512",
                RoleId = 4
            },
            StatusPartial = new Status()
            {
                IsEnabled = true,
                IsDownlinesEnabled = true,
                IsStatusUpdated = false,
                Upline = new Status
                {
                    IsEnabled = true,
                    IsDownlinesEnabled = true
                }
            },
            ComissionPartial = new Comission()
           {
               CommisionValue = 6.75,
               MaxCommisionValue = 6.75,
               IsCommisionUpdated = false
           },
            CashInSharePartial = new CashInShare()
            {
                CashInShareValue = 14.5,
                MaxCashInShareValue = 22.5,
                IsCashInShareUpdated = false
            }
        };

        ////[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditSuper()
        {
            return View(Constants.EditColossusBetCshtml, modelFake);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public JsonResult UpdateSuper()
        {      
            return Json(string.Empty);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditMaster()
        {
            return View(Constants.EditColossusBetCshtml, modelFake);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "ColossusBetSetting")]
        public JsonResult UpdateMaster()
        {
            return Json(string.Empty);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "ColossusBetSetting")]
        public ActionResult EditAgent()
        {
            return View(Constants.EditColossusBetCshtml, modelFake);
        }

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "ColossusBetSetting")]
        public JsonResult UpdateAgent()
        {
            return Json(string.Empty);
        }

        private void SetTitleForMultipleEdit(ColossusBetModel customerInformationViewModel)
        {
            ////ViewBag.Title = customerInformationViewModel.IsMultipleEdit == 1 ? Resources.LayoutTexts.EditMultipleKeno : Resources.LayoutTexts.UpdateColossusBetSetting;
        }
    }
}
