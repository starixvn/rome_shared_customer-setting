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
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// UpdateSuperPositionTakingCriteria class.
    /// </summary>
    public class UpdateSuperPositionTakingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public double SPositionTaking { get; set; }

        public double Min_PositionTaking { get; set; }

        public double Discount { get; set; }

        public bool UpdatePositionTaking { get; set; }

        public bool UpdateMinPositionTaking { get; set; }

        public bool UpdateCommission { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdSuperKeno_Positiontaking";
        }

        public override bool IsValid()
        {
            return CustId > 0 && SportType > 0 && BetType > 0;
        }
    }
}