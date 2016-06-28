// ************************************************************************************
// <copyright file="CloneAccountCriteria.cs" company="">
//     Copyright (c) CloneAccountCriteria.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Andy.Nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.CloneSetting.Criteria
{
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloneAccountCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "fromusername")]
        public string TargetUsername { get; set; }

        [SpParam(SqlParamName = "tousername")]
        public string DestinationUsername { get; set; }

        public int UserId
        {
            get
            {
                return AppSession.AccId;
            }
        }

        public string Ip
        {
            get
            {
                return Utils.GetClientIP();
            }
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
