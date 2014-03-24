// --------------------------------------------------------------------------
// <copyright file="UpdateAgentPositionTakingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Criteria
{
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// UpdateAgentPositionTakingCriteria class.
    /// </summary>
    public class UpdateAgentPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        /// <summary>
        /// Gets or sets the position taking of M2.
        /// </summary>
        /// <value>
        /// The position taking of M2.
        /// </value>
        public double MPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of UM2.
        /// </summary>
        /// <value>
        /// The value of UM2.
        /// </value>
        public double Auto_MPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IM2 is true or not.
        /// </summary>
        /// <value>
        /// The value of IM2.
        /// </value>
        public bool IsAuto_MPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the position taking of A2.
        /// </summary>
        /// <value>
        /// The position taking of A2.
        /// </value>
        public double APositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the position taking of N2.
        /// </summary>
        /// <value>
        /// The position taking of N2.
        /// </value>
        public double APositionTaking_Min { get; set; }

        public bool UpdateAllMembers { get; set; }

        public double NewMPositionTaking { get; set; }

        public double Discount { get; set; }

        public bool UpdatePositionTaking { get; set; }

        public bool UpdateMinPositionTaking { get; set; }

        public bool UpdateAutoPositionTaking { get; set; }

        public bool UpdateCommission { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.UpdateAgentPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}