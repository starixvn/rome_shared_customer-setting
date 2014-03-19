// --------------------------------------------------------------------------
// <copyright file="GetCurrencyCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetCurrencyCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Core_ExchangeCurrencySel";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
