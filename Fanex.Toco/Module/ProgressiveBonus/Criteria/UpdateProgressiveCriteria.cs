// -----------------------------------------------------------------------
// <copyright file="UpdateProgressiveCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  Update Progressive Criteria
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Criteria
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update progressive status
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
	public class UpdateProgressiveCriteria : CriteriaBase, ICriteria
	{
        /// <summary>
        /// Gets or sets the Id of the customer. 
        /// </summary>
		public int CustId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether disable status.
        /// </summary>
		public bool Disable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether disable status.
        /// </summary>
		public bool DisableDownlines { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Sub account. 
        /// </summary>
		public int SubAccId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether User Id.
        /// </summary>
		public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether language.
        /// </summary>
		public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ip address.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
		public string IP { get; set; }

        /// <summary>
        /// Gets a value indicating whether store procedure.
        /// </summary>
        /// <returns>Store procedure name</returns>
		public override string GetSettingKey()
		{
            return "CustomerUpdDisableCasinoBonus";
		}

        /// <summary>
        /// Check is valid or not
        /// </summary>
        /// <returns>return true</returns>
		public override bool IsValid()
		{
			return true;
		}
	}
}