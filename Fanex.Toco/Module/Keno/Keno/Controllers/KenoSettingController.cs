// --------------------------------------------------------------------------
// <copyright file="KenoSettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Helper;
    using Fanex.CustomerSetting.Module.Keno.Services;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class KenoSettingController : KenoSettingBaseController
    {
        private static IKenoSettingService _kenoSettingService;

        public KenoSettingController()
            : this(null)
        {
        }

        public KenoSettingController(IKenoSettingService kenoSettingService)
        {
            _kenoSettingService = kenoSettingService ?? new KenoSettingService();
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of EditSuper.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditSuper")]
        public ActionResult EditSuper(int custId)
        {
            var customerSetting = _kenoSettingService.GetSuperSetting(custId);
            return View(Constants.EDIT_SUPER_CSHTML, customerSetting);
        }

        [HttpPost]
        public JsonResult EditSuper(SuperSetting superSetting)
        {
            IList<Result> results = _kenoSettingService.UpdateSuperCustomerSetting(superSetting);

            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of EditMaster.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditMaster")]
        public ActionResult EditMaster(int custId)
        {
            return View(Constants.EditMasterCshtml);
        }

        [HttpPost]
        public JsonResult EditMaster(MasterSetting masterSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of EditAgent.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditAgent")]
        public ActionResult EditAgent(int custId)
        {
            return View(Constants.EditAgentCshtml);
        }

        [HttpPost]
        public JsonResult EditAgent(MasterSetting agentSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of SuperEditMember.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SuperEditMember")]
        public ActionResult SuperEditMember(int custId)
        {
            return View(Constants.SuperEditMemberCshtml);
        }

        [HttpPost]
        public JsonResult SuperEditMember(MemberSetting memberSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of MasterEditMember.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "MasterEditMember")]
        public ActionResult MasterEditMember(int custId)
        {
            return View(Constants.MasterEditMemberCshtml);
        }

        [HttpPost]
        public JsonResult MasterEditMember(MemberSetting memberSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of MasterEditMember.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "AgentEditMember")]
        public ActionResult AgentEditMember(int custId)
        {
            return View(Constants.AgentEditMemberCshtml);
        }

        [HttpPost]
        public JsonResult AgentEditMember(MemberSetting memberSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }
    }
}
