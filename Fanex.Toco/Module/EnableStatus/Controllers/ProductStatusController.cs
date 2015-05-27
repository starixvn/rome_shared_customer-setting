// --------------------------------------------------------------------------
// <copyright file="ProductStatusController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductStatusController : BaseController
    {
        public ActionResult Index(int id, int uplineid, int userid, string productid, int level)
        {
            var model = new EnableStatusModel();
            var products = EnableStatusConfiguration.Instance.ProductCollection;

            return View("~/Areas/CustomerSetting/Views/EnableStatus/Index.cshtml");
        }

        public ActionResult Update(EnableStatusModel model)
        {
            return JsonBase(null);
        }
    }
}
