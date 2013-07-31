// -----------------------------------------------------------------------
// <copyright file="ProgressiveStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  ProgressiveStatus
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Entities
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Progressive status
    /// </summary>
    public class ProgressiveStatus
    {
        /// <summary>
        /// Gets or sets the name of the customer Id. 
        /// </summary>
        public int CustId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer username.
        /// </summary>   
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
		public string Username { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer role Id.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
		public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino Bonus status
        /// </summary>
        public bool DisableCasinoBonus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino Bonus jackpot
        /// </summary>
        public bool DisableCasinoJackpot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino Bonus downline status
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public bool DisableDownlinesCasinoBonus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino jackpot status
        /// </summary>
        public bool DisableUplinesCasinoBonus { get; set; }
    }
}