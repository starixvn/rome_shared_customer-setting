// --------------------------------------------------------------------------
// <copyright file="MasterGetMemberSettingCriteria.cs" company="">
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
    /// MasterGetMemberSettingCriteria class.
    /// </summary>
    public class MasterGetMemberSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey()
        {
            return Constants.MasterGetMemberSettingCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}