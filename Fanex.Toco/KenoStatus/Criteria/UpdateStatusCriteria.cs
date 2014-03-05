// ************************************************************************************
// <copyright file="SelectAgentCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="ethan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.KenoStatus.Criteria
{
    using Fanex.NAP.Framework.Data;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Update Enable Status for Keno.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
    public class UpdateStatusCriteria : CriteriaBase
    {
        /// <summary>
        /// Gets or sets the Id of the customer. 
        /// </summary>
        /// <value>The id of the customer.</value>
        public int CustId { get; set; }

        /// <summary>
        /// Gets or sets the Bet Type 
        /// </summary>
        /// <value>The Bet Type</value>
        public int Bettype { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether disable status.
        /// </summary>
        /// <value>The id of the customer.</value>
        public bool Disable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether disable status.
        /// </summary>
        /// <value>The true of false status of the customer.</value>
        public bool DisableDownLines { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Sub account. 
        /// </summary>
        /// <value>The sub account id of a customer.</value>
        public int SubAccID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether User Id.
        /// </summary>
        /// <value>The account id of an admin.</value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether language.
        /// </summary>
        /// <value>The language of system is using.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ip address.
        /// </summary>
        /// <value>The ip of client is using.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public string IP { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdDisableKeno";
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