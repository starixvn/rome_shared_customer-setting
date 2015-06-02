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
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Services;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductStatusController : BaseController
    {
        private IEnableStatusService service = new EnableStatusService();

        public ActionResult Index(RequestContext requestContext)
        {
            var sp = Helper.GetStoredProcedures(requestContext.ProductId).Where(p => p.Type == StoredProcedure.SpType.SELECT).FirstOrDefault();            
            var model = service.GetProductStatus(sp, requestContext);            

            return View("~/Areas/CustomerSetting/Views/EnableStatus/Index.cshtml", model);
        }

        public ActionResult Update(EnableStatusModel model)
        {
            var sp = Helper.GetStoredProcedures(model.RequestContext.ProductId).Where(p => p.Type == StoredProcedure.SpType.UPDATE).FirstOrDefault();
            var results = service.UpdateProductStatus(model, sp);

            return JsonBase(results);
        }
    }
}
