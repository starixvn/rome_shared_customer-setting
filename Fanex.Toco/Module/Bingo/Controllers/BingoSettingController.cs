// ************************************************************************************
// <copyright file="BingoSettingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.Bingo.Controllers
{
    using System;
    using System.Web.Mvc;
    using Bingo.Helper;
    using Bingo.Services;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.CustomerSetting.Security.Configurations;
    using Newtonsoft.Json;
    using Sunplus.Bingo.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BingoSettingController : BaseController
    {
        private readonly IBingoSettingService _bingoService = null;

        public BingoSettingController()
            : this(null)
        {
        }

        public BingoSettingController(IBingoSettingService bingoService)
        {
            _bingoService = _bingoService ?? new BingoSettingService();
        }

        #region Getting methods
        
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Bingo")]
        [ActionName("Super")]
        public ActionResult SuperEdit(int id = 0, int uplineId = 0, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            return View("~/Areas/CustomerSetting/Views/Bingo/Setting/Super.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Bingo")]
        [ActionName("Master")]
        public ActionResult MasterEdit(int id = 0, int uplineId = 0, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            return View("~/Areas/CustomerSetting/Views/Bingo/Setting/Master.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Bingo")]
        [ActionName("Agent")]
        public ActionResult AgentEdit(int id = 0, int uplineId = 0, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            return View("~/Areas/CustomerSetting/Views/Bingo/Setting/Agent.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Bingo")]
        [ActionName("Member")]
        public ActionResult MemberEdit(int id = 0, int uplineId = 0, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            return View("~/Areas/CustomerSetting/Views/Bingo/Setting/Member.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Bingo")]
        public ActionResult SuperEditMember(int id = 0, int uplineId = 0, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            return View("~/Areas/CustomerSetting/Views/Bingo/Setting/SuperEditMember.cshtml");
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Bingo")]
        public ActionResult MasterEditMember(int id = 0, int uplineId = 0, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(id, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            return View("~/Areas/CustomerSetting/Views/Bingo/Setting/MasterEditMember.cshtml");
        }

        [HttpPost]
        public ActionResult GetCustomerSetting(int custId, int updatedByRoleID)
        {
            Customer customerSetting = _bingoService.GetCustomerSetting(custId, updatedByRoleID);
            return Json(JsonConvert.SerializeObject(customerSetting, Formatting.Indented, SerializeHelper.GetJsonSerializerSetting()), JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region Updading methods

        [HttpPost]
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Bingo")]
        public ActionResult UpdateCustomerSetting(Customer customer, int? updatedByRoleID, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(customer.ID, null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            IResult result = null;
            if (updatedByRoleID.HasValue && updatedByRoleID.Value > 0)
            {
                result = _bingoService.UpdateCustomerSetting(
                    customer, 
                    updatedByRoleID.Value, 
                    FromSystem,
                    AppSession.CurrentSession.UserId,
                    AppSession.CurrentSession.UserName);
            }

            return JsonBase(result);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Bingo")]
        public ActionResult ResetMaxTransfer(int? custId, string groupName = null)
        {
            var obj = Activator.CreateInstance(Type.GetType(CustomerSettingSection.Instance.AuthenticateFactory)) as IAuthenticate;
            if (!obj.IsBelong(Utils.ToInt32(custId), null, groupName))
            {
                return this.ActionDenied(this.Request);
            }

            IResult result = null;
            bool validCustId = custId.HasValue && custId.Value > 0;
            if (validCustId)
            {
                result = _bingoService.ResetMaxTransfer(
                    custId.Value,
                    FromSystem,
                    AppSession.CurrentSession.UserId,
                    AppSession.CurrentSession.UserName);
            }

            return JsonBase(result);
        }

        #endregion
    }
}
