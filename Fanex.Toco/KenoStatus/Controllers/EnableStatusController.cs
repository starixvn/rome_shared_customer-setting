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
        public ActionResult Index(int id, int uplineId, int roleId)
        {
            //if (!this.IsBelong(Request["groupName"], id, null, true))
            //{
            //    return this.ActionDenied(this.Request);
            //}

            ViewBag.roleId = roleId;
            ViewBag.id = id;
            ViewBag.uplineId = uplineId;

            return this.PartialViewBase(null, "~/Areas/CustomerSetting/Views/Keno/EnableStatus/Index.cshtml");
        }

        /// <summary>
        /// Gets racing status.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="uplineId">Upper customer id.</param>
        /// <param name="roleId">Customer role id.</param>
        /// <returns>ActionResult object.</returns>        
        public ActionResult GetKenoStatusData(int id, int uplineId, int roleId)
        {
            ViewBag.roleId = roleId;
            ViewBag.id = id;
            ViewBag.uplineId = uplineId;

            var model = _statusRepository.GetStatus(new SelectStatusCriteria() { 
                CustID=id
            });

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

        public ActionResult Update(string model)
        {
            return this.JsonBase(new { ErrorCode = 0, ErrorMessage = string.Empty });
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
