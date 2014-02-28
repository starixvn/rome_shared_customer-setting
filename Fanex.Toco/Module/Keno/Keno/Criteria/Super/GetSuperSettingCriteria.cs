// --------------------------------------------------------------------------
// <copyright file="GetSuperSettingCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// GetSuperSettingCriteria class.
    /// </summary>
    public class GetSuperSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelEditSuperKeno";
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}