// --------------------------------------------------------------------------
// <copyright file="SuperGetMemberSettingCriteria.cs" company="">
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
    /// SuperGetMemberSettingCriteria class.
    /// </summary>
    public class SuperGetMemberSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return Constants.SuperGetMemberSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}