// --------------------------------------------------------------------------
// <copyright file="UpdateAgentBetSettingCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// UpdateAgentBetSettingCriteria class.
    /// </summary>
    public class UpdateAgentBetSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetTypeId { get; set; }

        public bool UpdateStatus { get; set; }

        public bool Disable { get; set; }

        public bool DisableDownLines { get; set; }

        public bool UpdateMinMaxBet { get; set; }

        public decimal MinBet { get; set; }

        public decimal MaxBet { get; set; }

        public decimal MaxPerMatch { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.UpdateAgentBetSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetTypeId > 0;
        }
    }
}