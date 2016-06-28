// --------------------------------------------------------------------------
// <copyright file="InvalidCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/14">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InvalidCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return string.Empty;
        }

        public override bool IsValid()
        {
            return false;
        }
    }
}