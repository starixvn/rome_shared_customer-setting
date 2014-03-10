// ************************************************************************************
// <copyright file="SelectAgentCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.KenoStatus.Criteria
{
    using Fanex.NAP.Framework.Data;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Select Enable Status for Keno.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
    public class SelectStatusCriteria : CriteriaBase
    {
        /// <summary>
        /// Gets or sets the Id of the customer. 
        /// </summary>
        /// <value>The id of the customer.</value>
        public int CustID { get; set; }

        /// <summary>
        /// Get store procedure name by key.
        /// </summary>
        /// <returns>Store procedure name.</returns>
        public override string GetSettingKey()
        {
            return "Acc_CustomerSelKenoStatus";
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