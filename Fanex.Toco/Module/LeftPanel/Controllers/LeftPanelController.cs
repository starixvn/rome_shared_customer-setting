// --------------------------------------------------------------------------
// <copyright file="LeftPanelController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.07.25">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.LeftPanel.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.LeftPanel.ViewModels;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Controller for left panel menu.
    /// </summary>
    public class LeftPanelController : BaseController
    {
        public ActionResult Index(int id, int uplineId)
        {
            var model = new CustomerModel();

            return this.View("~/Areas/CustomerSetting/Views/LeftPanel/Index.cshtml", model);
        }

        public ActionResult Search(string username)
        {
            var model = new CustomerModel();

            return this.JsonBase(model);
        }

        public ActionResult SearchSuper(string superName)
        {
            var model = new CustomerModel();

            return this.JsonBase(model);
        }

        public ActionResult SearchMaster(string masterName)
        {
            var model = new CustomerModel();

            return this.JsonBase(model);
        }

        public ActionResult SearchAgent(string agentName)
        {
            var model = new CustomerModel();

            return this.JsonBase(model);
        }

        public ActionResult SearchMember(string memberName)
        {
            var model = new CustomerModel();

            return this.JsonBase(model);
        }

        public ActionResult UpdatePost(string model)
        {
            var customerModel = JsonConvert.DeserializeObject<CustomerModel>(model);

            return this.JsonBase(null);
        }
    }
}