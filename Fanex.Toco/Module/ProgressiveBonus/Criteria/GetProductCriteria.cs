// -----------------------------------------------------------------------
// <copyright file="GetProductCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  GetProductCriteria.
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Criteria
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get product criteria.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
    public class GetProductCriteria : CriteriaBase, ICriteria
    {
        /// <summary>
        /// Gets or sets the Id of the customer. 
        /// </summary>
        /// <value>The id of the customer.</value>
        public int CustId { get; set; }

        /// <summary>
        /// Get store procedure name by key.
        /// </summary>
        /// <returns>Store procedure name.</returns>
        public override string GetSettingKey()
        {
            return "CustomerSelCasinoBonusStatus";
        }

        /// <summary>
        /// Check is valid or not.
        /// </summary>
        /// <returns>Return true.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}