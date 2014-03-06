// ************************************************************************************
// <copyright file="EnableStatusController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) FilterConfig.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Ethan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.KenoStatus.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Fanex.CustomerSetting.Module.KenoStatus.Repositories.Interface;
    using Fanex.CustomerSetting.Module.KenoStatus.Criteria;
    using Fanex.CustomerSetting.Module.KenoStatus.Repositories.Impl;
    using Fanex.CustomerSetting.Module.KenoStatus.ViewModels;
    using System.Collections;
    using Fanex.CustomerSetting.Security.Authenticate;

    public class EnableStatusController: BaseController
    {
        private static IStatusRepository _statusRepository;

        public EnableStatusController()
            : this(null)
        {
        }

        public EnableStatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository ?? new StatusRepository();
        }

        /// <summary>
        /// Gets enable racing page.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="uplineId">Upper customer id.</param>
        /// <param name="roleId">Customer role id.</param>
        /// <returns>ActionResult object.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Keno")]
        public ActionResult Index(int id, int uplineId, int roleId, int subAccountId = 0)
        {
            //if (!this.IsBelong(Request["groupName"], id, null, true))
            //{
            //    return this.ActionDenied(this.Request);
            //} EnableStatus/Keno

            ViewBag.roleId = roleId;
            ViewBag.id = id;
            ViewBag.uplineId = uplineId;
            ViewBag.subAccountId = subAccountId;

            return this.PartialViewBase(null, "~/Areas/CustomerSetting/Views/Keno/EnableStatus/Index.cshtml");
        }

        /// <summary>
        /// Gets racing status.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="uplineId">Upper customer id.</param>
        /// <param name="roleId">Customer role id.</param>
        /// <returns>ActionResult object.</returns>        
        public ActionResult GetKenoStatusData(int id, int uplineId, int roleId, int subAccountId = 0)
        {
            var model = _statusRepository.GetStatus(new SelectStatusCriteria() { 
                CustID=id
            });

            model.RoleID = roleId;
            model.UplineID = uplineId;
            model.SubAccID = subAccountId;

            var result = "var model = " + JsonConvert.SerializeObject(
                model,
                Formatting.None,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }) + ";";

            return this.Content(result);
        }

        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Keno")]
        public ActionResult Update(string model)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var newStatus = JsonConvert.DeserializeObject<KenoEnableStatus>(model, settings);
            var criteria = new UpdateStatusCriteria()
            {
                CustId=newStatus.CustID,
                UserID=newStatus.UserID,
                SubAccID=newStatus.SubAccID,
                Language=AppSession.CurrentSession.Language,
                IP = AppSession.CurrentContext.Ip4Address
            };
            List<string> errCode = new List<string>();
            List<string> errMsg = new List<string>();

            foreach (BetTypeStatus status in newStatus.BetTypeStatusList)
            {
                criteria.Bettype = status.BetTypeID;
                criteria.Disable = status.IsDisabled;
                criteria.DisableDownLines = status.IsDisabledDownlines;
                var result=_statusRepository.UpdateStatus(criteria);
                errCode.Add(Convert.ToString(result["Errcode"]));
                errMsg.Add(Convert.ToString(result["ErrMsg"]));
            }

            return this.JsonBase(new {
                ErrorCode = String.Join("\u0007", errCode.ToArray()),
                ErrorMessage = String.Join("\u0007", errMsg.ToArray()) 
                });
        }

        /// <summary>
        /// Returns Partial View associated with model object.
        /// </summary>
        /// <param name="objModel">Model object.</param>
        /// <param name="viewName">URL to view.</param>
        /// <returns>PartialViewResult object.</returns>
        protected virtual ActionResult PartialViewBase(object objModel = null, string viewName = null)
        {
            return string.IsNullOrWhiteSpace(viewName) ? this.PartialView(objModel) : this.PartialView(viewName, objModel);
        }

    }
}
