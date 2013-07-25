namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Casino.ViewModels;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Newtonsoft.Json;

    public class MemberController : BaseController
    {
        public ActionResult EditMember(int id, int uplineId)
        {
            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = CasinoManager.Instance.MemberCasinoModel(id, uplineId);
            }
            catch
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml");
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Member/EditMember.cshtml", customerSetting);
        }

        public ActionResult EditMemberByMaster(int id, int uplineId)
        {
            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = CasinoManager.Instance.MemberCasinoModelByMaster(id, uplineId);
            }
            catch
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml");
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Member/EditMemberByMaster.cshtml", customerSetting);
        }

        public ActionResult EditMemberBySuper(int id, int uplineId)
        {
            var customerSetting = new CasinoSettingModel();

            try
            {
                customerSetting = CasinoManager.Instance.MemberCasinoModelBySuper(id, uplineId);
            }
            catch
            {
                return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Error.cshtml");
            }

            return this.PartialView("~/Areas/CustomerSetting/Views/Casino/Member/EditMemberBySuper.cshtml", customerSetting);
        }

        public ActionResult UpdateMemberPost(int id, int uplineId, int userId, string username, string settingModel, string customerModel)
        {
            var ret = CasinoManager.Instance.UpdateMember(id, uplineId, Request.UserHostAddress, userId, username, BaseController.FromSystem, JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), JsonConvert.DeserializeObject<CasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }

        public ActionResult UpdateMemberBySuperPost(int id, int uplineId, int userId, string username, string settingModel, string customerModel)
        {
            var ret = CasinoManager.Instance.UpdateMemberBySuper(id, uplineId, Request.UserHostAddress, userId, username, BaseController.FromSystem, JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), JsonConvert.DeserializeObject<CasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }

        public ActionResult UpdateMemberByMasterPost(int id, int uplineId, int userId, string username, string settingModel, string customerModel)
        {
            var ret = CasinoManager.Instance.UpdateMemberByMaster(id, uplineId, Request.UserHostAddress, userId, username, BaseController.FromSystem, JsonConvert.DeserializeObject<CasinoSettingModel>(settingModel), JsonConvert.DeserializeObject<CasinoCustomer>(customerModel));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}