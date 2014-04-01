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
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.KenoStatus.Criteria;
    using Fanex.CustomerSetting.Module.KenoStatus.Repositories.Impl;
    using Fanex.CustomerSetting.Module.KenoStatus.Repositories.Interface;
    using Fanex.CustomerSetting.Module.KenoStatus.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Enable Status Controller.
    /// </summary>
    public class EnableStatusController : KenoBaseController
    {
        private static IStatusRepository _statusRepository;

        public EnableStatusController()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnableStatusController"/> class.
        /// </summary>
        /// <param name="statusRepository">The status repository.</param>
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
        /// <param name="subAccountId">Customer sub account id.</param>
        /// <param name="userId">User login id.</param>
        /// <returns>ActionResult object.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Keno")]
        public ActionResult Index(int id, int uplineId, int roleId, int subAccountId = 0, int userId = 0)
        {
            ViewBag.roleId = roleId;
            ViewBag.id = id;
            ViewBag.uplineId = uplineId;
            ViewBag.subAccountId = subAccountId;
            ViewBag.userId = userId;
            return this.PartialViewBase(null, "~/Areas/CustomerSetting/Views/Keno/EnableStatus/Index.cshtml");
        }

        /// <summary>
        /// Gets racing status.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="uplineId">Upper customer id.</param>
        /// <param name="roleId">Customer role id.</param>
        /// <param name="subAccountId">Customer sub account id.</param>
        /// <param name="userId">User login id.</param>
        /// <returns>ActionResult object.</returns>
        public ActionResult GetKenoStatusData(int id, int uplineId, int roleId, int subAccountId = 0, int userId = 0)
        {
            ViewBag.subAccountId = subAccountId;
            var selectCriteria = new SelectStatusCriteria();
            selectCriteria.CustID = id;
            var model = _statusRepository.GetStatus(selectCriteria);
            model.CustID = selectCriteria.CustID;
            model.RoleID = roleId;
            model.UplineID = uplineId;
            model.SubAccID = subAccountId;
            model.UserID = userId;
            model.Language = AppSession.Language;
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

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult object.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "Keno")]
        public ActionResult Update(string model)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var newStatus = JsonConvert.DeserializeObject<KenoEnableStatus>(model, settings);
            var criteria = new UpdateStatusCriteria()
            {
                CustId = newStatus.CustID,
                UserID = newStatus.UserID,
                UplineID = newStatus.UplineID,
                SubAccID = newStatus.SubAccID,
                Language = TWLang(AppSession.Language),
                IP = AppSession.CurrentContext.Ip4Address
            };
            List<string> errCode = new List<string>();
            List<string> errMsg = new List<string>();

            foreach (BetTypeStatus status in newStatus.BetTypeStatusList)
            {
                if (!status.IsDisabledUpline)
                {
                    criteria.Bettype = status.BetTypeID;
                    criteria.Disable = status.IsDisabled;
                    criteria.DisableDownLines = status.IsDisabledDownlines;
                    var result = _statusRepository.UpdateStatus(criteria);
                    errCode.Add(result["errcode"].ToString());
                    errMsg.Add(result["errmsg"].ToString());
                }
            }

            return this.JsonBase(new
                {
                    ErrorCode = string.Join("\u0007", errCode.ToArray()),
                    ErrorMessage = string.Join("\u0007", errMsg.ToArray())
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