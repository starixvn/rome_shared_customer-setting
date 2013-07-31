// -----------------------------------------------------------------------
// <copyright file="CasinoController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  CasinoController
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
    using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement;
    using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;

    /// <summary>
    /// Casino controller
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
	public class CasinoController : BaseController
	{
        /// <summary>
        /// Progressive Service
        /// </summary>
		private IProgressiveService progressiveService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CasinoController"/> class.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
		public CasinoController()
		{
			progressiveService = new ProgressiveService();
		}

        /// <summary>
        /// Load view of progressive bonus
        /// </summary>
        /// <param name="custId">Customer Id need to load progressive bonus</param>
        /// <returns>return the view</returns>
		[HttpGet]
		public ActionResult Index(int? custId)
		{
			return this.View("~/Areas/CustomerSetting/Views/ProgressiveBonus/Casino/Index.cshtml", custId);
		}

        /// <summary>
        /// Gets a value indicating whether the item of product.
        /// </summary>
        /// <param name="CustId">Customer Id</param>
        /// <returns>Product of customer</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
        [HttpPost]
		public JsonResult GetProducts(int CustId)
		{
            var products = this.progressiveService.GetProducts(CustId);
			return this.Json(products);
		}

        /// <summary>
        /// Update progressive status
        /// </summary>
        /// <param name="progressive">Progressive product</param>
        /// <returns>Return of action update</returns>
		[HttpPost]
		public JsonResult UpdateProgressive(Progressive progressive)
		{
			var results = this.progressiveService.UpdateProgressive(progressive);
			return this.Json(results);
		}
	}
}