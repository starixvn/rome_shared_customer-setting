// ************************************************************************************
// <copyright file="CloneAccountCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) CloneAccountCriteria.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Andy.Nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.CloneSetting.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloneAccountCriteria : CriteriaBase
    {
        public string TargetUsername
        {
            get;
            set;
        }

        public string DestinationUsername
        {
            get;
            set;
        }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSettingsCloneFromRealAccount";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
