// -----------------------------------------------------------------------
// <copyright file="CasinoController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  CasinoController
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveBonus.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Module.ProgressiveBonus.Entities;
    using Fanex.CustomerSetting.Module.ProgressiveBonus.Services.Implement;
    using Fanex.CustomerSetting.Module.ProgressiveBonus.Services.Interface;

    /// <summary>
    /// Casino controller.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
	public class CasinoController : BaseController
	{
        /// <summary>
        /// Progressive Service.
        /// </summary>
		private IProgressiveService _progressiveService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CasinoController"/> class.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
		public CasinoController()
		{
			_progressiveService = new ProgressiveService();
		}

        /// <summary>
        /// Load view of progressive bonus.
        /// </summary>
        /// <param name="custId">Customer Id need to load progressive bonus.</param>
        /// <returns>Return the view.</returns>
		[HttpGet]
		public ActionResult Index(int? custId)
		{
			return this.View("~/Areas/CustomerSetting/Views/ProgressiveBonus/Casino/Index.cshtml", custId);
		}

        /// <summary>
        /// Gets a value indicating whether the item of product.
        /// </summary>
        /// <param name="CustId">Customer Id.</param>
        /// <returns>Product of customer.</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
        [HttpPost]
		public JsonResult GetProducts(int CustId)
		{
            var products = this._progressiveService.GetProducts(CustId);
			return this.Json(products);
		}

        /// <summary>
        /// Update progressive status.
        /// </summary>
        /// <param name="progressive">Progressive product.</param>
        /// <returns>Return of action update.</returns>
		[HttpPost]
		public JsonResult UpdateProgressive(Progressive progressive)
		{
			var results = this._progressiveService.UpdateProgressive(progressive);
			return this.Json(results);
		}
	}
}