// --------------------------------------------------------------------------
// <copyright file="MasterUpdateMemberPositionTakingCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// MasterUpdateMemberPositionTakingCriteria class.
    /// </summary>
    public class MasterUpdateMemberPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        /// <summary>
        /// Gets or sets the position taking of M1.
        /// </summary>
        /// <value>
        /// The position taking of M1.
        /// </value>
        public double MPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of UM1.
        /// </summary>
        /// <value>
        /// The value of UM1.
        /// </value>
        public double Auto_MPositionTaking_Mem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IM1 is true or not.
        /// </summary>
        /// <value>
        /// The value of IM1.
        /// </value>
        public bool IsAuto_MPositionTaking_Mem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IM2 is true or not.
        /// </summary>
        /// <value>
        /// The value of IM2.
        /// </value>
        public bool IsAuto_MPositionTaking { get; set; }

        public bool UpdatePositionTaking { get; set; }

        public bool UpdateAutoPositionTaking { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.MasterUpdateMemberPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}