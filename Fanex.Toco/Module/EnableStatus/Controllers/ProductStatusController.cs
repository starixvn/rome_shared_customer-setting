// --------------------------------------------------------------------------
// <copyright file="ProductStatusController.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created.</change>
//   <change who="Andy.Nguyen" date="2016.2.2">Documented.</change>
// </history>

using System;
using System.Linq;
using System.Web.Mvc;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Services;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Fanex.CustomerSetting.Security.Authenticate;
using Newtonsoft.Json;

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Controllers
{
    /// <summary>
    /// Product status controller to use for NAP, ALPHA, EXTNAP.
    /// </summary>
    public class ProductStatusController : BaseController
    {
        /// <summary>
        /// Enable status service.
        /// </summary>
        private readonly IEnableStatusService _service = new EnrichedEnableStatusService(new EnableStatusService());

        /// <summary>
        /// Gets index page of enable status form.
        /// </summary>
        /// <param name="context">Request context to get enable product status form.</param>
        /// <returns>ActionResult object.</returns>
        [HttpGet]
        public ActionResult Index(RequestContext context)
        {
            var controlName = typeof(ProductStatusController).Name;
            var mainAction = Helper.Products.FirstOrDefault(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).Function;
            var actionPath = controlName + "/" + mainAction;
            var authenticationType = AuthenticateAction.AuthenticationType.Read;
            var authenticateFactory = AuthenticateFactory.Create(authenticationType, mainAction);

            if (!authenticateFactory.AllowRead(actionPath) || !authenticateFactory.IsBelong(context.CustId, context.Username, AppSession.CurrentSession.CurrencyGroup))
            {
                return ActionDenied(Request);
            }

            var sp = Helper.GetStoredProcedures(context.ProductId).FirstOrDefault(p => p.Type == StoredProcedure.SpType.SELECT);
            var model = _service.GetProductStatus(sp, context);

            return View("~/Areas/CustomerSetting/Views/EnableStatus/Index.cshtml", model);
        }

        /// <summary>
        /// Gets index page of enable status form.
        /// </summary>
        /// <param name="context">Request context to get enable product status form.</param>
        /// <returns>ActionResult object.</returns>
        [HttpGet]
        public ActionResult GetProductStatusNotAllowDisabledDownline(RequestContext context)
        {
            var controlName = typeof(ProductStatusController).Name;
            var mainAction = Helper.Products.FirstOrDefault(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).Function;
            var actionPath = controlName + "/" + mainAction;
            var authenticationType = AuthenticateAction.AuthenticationType.Read;
            var authenticateFactory = AuthenticateFactory.Create(authenticationType, mainAction);

            if (!authenticateFactory.AllowRead(actionPath) || !authenticateFactory.IsBelong(context.CustId, context.Username, AppSession.CurrentSession.CurrencyGroup))
            {
                return ActionDenied(Request);
            }

            var sp = Helper.GetStoredProcedures(context.ProductId).FirstOrDefault(p => p.Type == StoredProcedure.SpType.SELECT);
            var model = _service.GetProductStatus(sp, context);

            return View("~/Areas/CustomerSetting/Views/EnableStatusNotAllowDisabledDownline/Index.cshtml", model);
        }

        /// <summary>        
        /// Updates enable status.
        /// </summary>
        /// <param name="model">Product status model.</param>
        /// <returns>ActionResult object.</returns>
        [AntiXSRFFilter]
        [HttpPost]
        public ActionResult Update(string model)
        {
            var modelObj = JsonConvert.DeserializeObject<EnableStatusModel>(model);
            var controlName = typeof(ProductStatusController).Name;
            var mainAction = Helper.Products.FirstOrDefault(p => p.Id.Equals(modelObj.RequestContext.ProductId, StringComparison.OrdinalIgnoreCase)).Function;
            var actionPath = controlName + "/" + mainAction;
            var authenticationType = AuthenticateAction.AuthenticationType.Host;
            var auth = AuthenticateFactory.Create(authenticationType, mainAction);

            if (!auth.AllowUpdate(actionPath) || !auth.IsBelong(modelObj.RequestContext.CustId, modelObj.RequestContext.Username, AppSession.CurrentSession.CurrencyGroup))
            {
                return ActionDenied(Request);
            }

            var sp = Helper.GetStoredProcedures(modelObj.RequestContext.ProductId).FirstOrDefault(p => p.Type == StoredProcedure.SpType.UPDATE);
            var results = _service.UpdateProductStatus(modelObj, sp);

            return JsonBase(results);
        }
    }
}