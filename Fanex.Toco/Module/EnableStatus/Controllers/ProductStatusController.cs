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
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Services;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductStatusController : BaseController
    {
        private IEnableStatusService service = new EnableStatusService();

        public ActionResult Index(RequestContext context)
        {
            var controlName = typeof(ProductStatusController).Name;
            var _mainAction = Helper.Products.Where(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Function;
            var actionPath = controlName + "/" + _mainAction;
            var _authenticationType = AuthenticateAction.AuthenticationType.Read;
            var authenticateFactory = AuthenticateFactory.Create(_authenticationType, _mainAction);
            
            if (!authenticateFactory.AllowRead(actionPath))
            {
                return ActionDenied(Request);
            }

            var sp = Helper.GetStoredProcedures(context.ProductId).Where(p => p.Type == StoredProcedure.SpType.SELECT).FirstOrDefault();            
            var model = service.GetProductStatus(sp, context);            

            return View("~/Areas/CustomerSetting/Views/EnableStatus/Index.cshtml", model);
        }

        public ActionResult Update(string model)
        {
            var enableStatusModel = JsonConvert.DeserializeObject<EnableStatusModel>(model);
            var controlName = typeof(ProductStatusController).Name;
            var _mainAction = Helper.Products.Where(p => p.Id.Equals(enableStatusModel.RequestContext.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Function;
            var actionPath = controlName + "/" + _mainAction;
            var _authenticationType = AuthenticateAction.AuthenticationType.Host;
            var authenticateFactory = AuthenticateFactory.Create(_authenticationType, _mainAction);

            if (!authenticateFactory.AllowUpdate(actionPath))
            {
                return ActionDenied(Request);
            }
                        
            var sp = Helper.GetStoredProcedures(enableStatusModel.RequestContext.ProductId).Where(p => p.Type == StoredProcedure.SpType.UPDATE).FirstOrDefault();
            var results = service.UpdateProductStatus(enableStatusModel, sp);

            return JsonBase(results);
        }
    }
}
