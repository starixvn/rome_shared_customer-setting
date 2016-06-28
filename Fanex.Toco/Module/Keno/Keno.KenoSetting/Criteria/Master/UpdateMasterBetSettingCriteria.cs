// --------------------------------------------------------------------------
// <copyright file="UpdateMasterBetSettingCriteria.cs" company="">
// Copyright (c). All rights reserved.
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
    /// UpdateMasterBetSettingCriteria class.
    /// </summary>
    public class UpdateMasterBetSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool UpdateStatus { get; set; }

        public bool Disable { get; set; }

        public bool DisableDownLines { get; set; }

        public bool UpdateMinMaxBet { get; set; }

        public double MinBet { get; set; }

        public double MaxBet { get; set; }

        public double MaxPerMatch { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.UpdateMasterBetSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}