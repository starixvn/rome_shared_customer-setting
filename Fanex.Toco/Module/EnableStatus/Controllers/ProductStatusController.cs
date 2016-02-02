// --------------------------------------------------------------------------
// <copyright file="ProductStatusController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created.</change>
//   <change who="Andy.Nguyen" date="2016.2.2">Documented.</change>
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
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Newtonsoft.Json;

    /// <summary>
    /// Product status controller to use for NAP, ALPHA, EXTNAP.
    /// </summary>
    public class ProductStatusController : BaseController
    {
        /// <summary>
        /// Enable status service.
        /// </summary>
        private IEnableStatusService _service = new EnableStatusService();

        /// <summary>
        /// Gets index page of enable status form.
        /// </summary>
        /// <param name="context">Request context to get enable product status form.</param>
        /// <returns>ActionResult object.</returns>
        [HttpGet]
        public ActionResult Index(RequestContext context)
        {
            var controlName = typeof(ProductStatusController).Name;
            var mainAction = Helper.Products.Where(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Function;
            var actionPath = controlName + "/" + mainAction;
            var authenticationType = AuthenticateAction.AuthenticationType.Read;
            var authenticateFactory = AuthenticateFactory.Create(authenticationType, mainAction);
            
            if (!authenticateFactory.AllowRead(actionPath) || !authenticateFactory.IsBelong(context.CustId, context.Username, AppSession.CurrentSession.CurrencyGroup))
            {
                return this.ActionDenied(this.Request);
            }

            var sp = Helper.GetStoredProcedures(context.ProductId).Where(p => p.Type == StoredProcedure.SpType.SELECT).FirstOrDefault();            
            var model = this._service.GetProductStatus(sp, context);            

            return this.View("~/Areas/CustomerSetting/Views/EnableStatus/Index.cshtml", model);
        }

        /// <summary>
        /// Updates enable status.
        /// </summary>
        /// <param name="model">Product status model.</param>
        /// <returns>ActionResult object.</returns>
        [HttpPost]
        public ActionResult Update(string model)
        {
            var modelObj = JsonConvert.DeserializeObject<EnableStatusModel>(model);
            var controlName = typeof(ProductStatusController).Name;
            var mainAction = Helper.Products.Where(p => p.Id.Equals(modelObj.RequestContext.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Function;
            var actionPath = controlName + "/" + mainAction;
            var authenticationType = AuthenticateAction.AuthenticationType.Host;
            var auth = AuthenticateFactory.Create(authenticationType, mainAction);

            if (!auth.AllowUpdate(actionPath) || !auth.IsBelong(modelObj.RequestContext.CustId, modelObj.RequestContext.Username, AppSession.CurrentSession.CurrencyGroup))
            {
                return this.ActionDenied(this.Request);
            }
                        
            var sp = Helper.GetStoredProcedures(modelObj.RequestContext.ProductId).Where(p => p.Type == StoredProcedure.SpType.UPDATE).FirstOrDefault();
            var results = this._service.UpdateProductStatus(modelObj, sp);

            return this.JsonBase(results);
        }
    }
}