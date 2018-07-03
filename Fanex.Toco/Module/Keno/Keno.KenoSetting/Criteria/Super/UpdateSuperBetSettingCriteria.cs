// --------------------------------------------------------------------------
// <copyright file="UpdateSuperBetSettingCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria
{
    using System;
    using System.Configuration;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// UpdateSuperBetSettingCriteria class.
    /// </summary>
    public class UpdateSuperBetSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool UpdateStatus { get; set; }

        public bool Disable { get; set; }

        public bool UpdateMinMaxBet { get; set; }

        public double MinBet { get; set; }

        public double MaxBet { get; set; }

        public double MaxPerMatch { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public int SystemId
        {
            get
            {
                int systemId;
                string systemIdInConfig = ConfigurationManager.AppSettings["SystemId"];

                if (string.IsNullOrWhiteSpace(systemIdInConfig) || !int.TryParse(systemIdInConfig, out systemId))
                {
                    throw new ArgumentException("SystemId is null, empty or not a integer number.", "SystemId");
                }

                return systemId;
            }
        }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return Constants.UpdateSuperBetSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}