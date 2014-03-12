// --------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    /// <summary>
    /// The Constants class.
    /// </summary>
    public static class Constants
    {
        public const string EditSuperCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Super/EditSuper.cshtml";
        public const string EditMasterCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Master/EditMaster.cshtml";
        public const string EditAgentCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Agent/EditAgent.cshtml";
        public const string SuperEditMemberCshtml = "~/Areas/CustomerSetting/Views/Keno/KenoSetting/SuperEditMember.cshtml";
        public const string AgentEditMemberCshtml = "~/Areas/CustomerSetting/Views/Keno/KenoSetting/AgentEditMember.cshtml";
        public const string MasterEditMemberCshtml = "~/Areas/CustomerSetting/Views/Keno/KenoSetting/MasterEditMember.cshtml";

        public const string GetSuperSettingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string UpdateSuperBetSettingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_BetSetting";
        public const string UpdateSuperPositionTakingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_Positiontaking";

        public const string GetMasterSettingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string UpdateMasterBetSettingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_BetSetting";
        public const string UpdateMasterPositionTakingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_Positiontaking";

        public const string GetAgentSettingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string UpdateAgentBetSettingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_BetSetting";
        public const string UpdateAgentPositionTakingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_Positiontaking";

        public const string GetMemberSettingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string AgentUpdateMemberBetSettingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string AgentUpdateMemberPositionTakingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string SuperUpdateMemberPositionTakingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string MasterUpdateMemberPositionTakingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
    }
}