// --------------------------------------------------------------------------
// <copyright file="UpdateMasterPositionTakingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// UpdateMasterPositionTakingCriteria class.
    /// </summary>
    public class UpdateMasterPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public double S3 { get; set; }

        public double US3 { get; set; }

        public bool IS3 { get; set; }

        public double M3 { get; set; }

        public double N3 { get; set; }

        public bool UpdateAllMembers { get; set; }

        public double NewSPositionTaking { get; set; }

        public double Discount { get; set; }

        public bool UpdatePositiontaking { get; set; }

        public bool UpdateMinPositiontaking { get; set; }

        public bool UpdateAutoPositionTaking { get; set; }

        public bool UpdateCommission { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

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