// --------------------------------------------------------------------------
// <copyright file="UpdateMasterPositionTakingCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// UpdateMasterPositionTakingCriteria class.
    /// </summary>
    public class UpdateMasterPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        /// <summary>
        /// Gets or sets the value of S3.
        /// </summary>
        /// <value>
        /// The value of S3.
        /// </value>
        public double SPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of US3.
        /// </summary>
        /// <value>
        /// The value of US3.
        /// </value>
        public double Auto_SPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IS3 is true or not.
        /// </summary>
        /// <value>
        /// The value of IS3.
        /// </value>
        public bool IsAuto_SPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of M3.
        /// </summary>
        /// <value>
        /// The value of M3.
        /// </value>
        public double MPositionTaking { get; set; }

        /// <summary>
        /// Gets or sets the value of N3.
        /// </summary>
        /// <value>
        /// The value of N3.
        /// </value>
        public double MPositionTaking_Min { get; set; }

        public bool UpdateAllMembers { get; set; }

        public double NewSPositionTaking { get; set; }

        public double Discount { get; set; }

        public bool UpdatePositiontaking { get; set; }

        public bool UpdateMinPositiontaking { get; set; }

        public bool UpdateAutoPositionTaking { get; set; }

        public bool UpdateCommission { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.UpdateMasterPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}