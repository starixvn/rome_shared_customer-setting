// --------------------------------------------------------------------------
// <copyright file="AgentUpdateMemberPositionTakingCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria
{
    using Data;
    using Data.Repository;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;

    /// <summary>
    /// AgentUpdateMemberPositionTakingCriteria class.
    /// </summary>
    public class AgentUpdateMemberPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        [SpParam(Ignored = true)]
        public bool UpdatePT { get; set; }

        public double A1 { get; set; }

        [SpParam(Ignored = true)]
        public double Discount { get; set; }

        [SpParam(Ignored = true)]
        public bool UpdateComm { get; set; }

        public int SubAccId { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public bool IsSearchAndCopy { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.AgentUpdateMemberPositionTakingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}