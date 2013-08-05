// -----------------------------------------------------------------------
// <copyright file="ProgressiveStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  ProgressiveStatus
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveBonus.Entities
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Progressive status.
    /// </summary>
    public class ProgressiveStatus
    {
        /// <summary>
        /// Gets or sets the name of the customer Id. 
        /// </summary>
        /// <value>The id of the customer.</value>
        public int CustId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer username.
        /// </summary>   
        /// <value>The username of the customer.</value>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
		public string Username { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer role Id.
        /// </summary>
        /// <value>The role id of the customer.</value>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
		public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino Bonus status.
        /// </summary>
        /// <value>The status of the casino bonus.</value>
        public bool DisableCasinoBonus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino Bonus jackpot.
        /// </summary>
        /// <value>The status of the casino jackpot.</value>
        public bool DisableCasinoJackpot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino Bonus downline status.
        /// </summary>
        /// <value>The status of the downlines casino bonus.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public bool DisableDownlinesCasinoBonus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Casino jackpot status.
        /// </summary>
        /// <value>The status of the up-line casino jackpot.</value>
        public bool DisableUplinesCasinoBonus { get; set; }
    }
}