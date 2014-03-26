// --------------------------------------------------------------------------
// <copyright file="UpdateSuperPositionTakingCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// UpdateSuperPositionTakingCriteria class.
    /// </summary>
    public class UpdateSuperPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        /// <summary>
        /// Gets or sets the value of S4.
        /// </summary>
        /// <value>
        /// The value of S4.
        /// </value>
        public double SPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of N4.
        /// </summary>
        /// <value>
        /// The value of N4.
        /// </value>
        public double Min_PositionTaking { get; set; }

        public double Discount { get; set; }

        public bool UpdatePositionTaking { get; set; }

        public bool UpdateMinPositionTaking { get; set; }

        public bool UpdateCommission { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.UpdateSuperPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}