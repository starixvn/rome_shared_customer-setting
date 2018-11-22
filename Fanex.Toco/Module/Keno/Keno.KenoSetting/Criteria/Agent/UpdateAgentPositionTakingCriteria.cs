// --------------------------------------------------------------------------
// <copyright file="UpdateAgentPositionTakingCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria
{
    using Data.Repository;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;

    /// <summary>
    /// UpdateAgentPositionTakingCriteria class.
    /// </summary>
    public class UpdateAgentPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public double M2 { get; set; }

        public double UM2 { get; set; }

        public bool IM2 { get; set; }

        public double A2 { get; set; }

        public double N2 { get; set; }

        public bool UpdateAllMembers { get; set; }

        public double NewMPositionTaking { get; set; }

        public double Discount { get; set; }

        public bool UpdatePositionTaking { get; set; }

        public bool UpdateMinPositionTaking { get; set; }

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
            return Constants.UpdateAgentPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}