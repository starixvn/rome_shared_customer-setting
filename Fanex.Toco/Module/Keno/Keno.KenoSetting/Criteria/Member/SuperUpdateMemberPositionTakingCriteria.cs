// --------------------------------------------------------------------------
// <copyright file="SuperUpdateMemberPositionTakingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria
{
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// SuperUpdateMemberPositionTakingCriteria class.
    /// </summary>
    public class SuperUpdateMemberPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        /// <summary>
        /// Gets or sets the position taking of S1.
        /// </summary>
        /// <value>
        /// The position taking of S1.
        /// </value>
        public double SPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of US1.
        /// </summary>
        /// <value>
        /// The value of US1.
        /// </value>
        public double Auto_SPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IS1 is true or not.
        /// </summary>
        /// <value>
        /// The value of IS1.
        /// </value>
        public bool IsAuto_SPositionTaking { get; set; }

        public bool UpdatePositionTaking { get; set; }

        public bool UpdateAutoPositionTaking { get; set; }

        public int SubAccId { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.SuperUpdateMemberPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}