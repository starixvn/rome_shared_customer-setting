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
        public const int SportType = 202;

        public const string EditSuperCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Views/Super/EditSuper.cshtml";
        public const string EditMasterCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Views/Master/EditMaster.cshtml";
        public const string EditAgentCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Views/Agent/EditAgent.cshtml";
        public const string SuperEditMemberCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Views/SuperOnMember/SuperEditMember.cshtml";
        public const string AgentEditMemberCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Views/Member/AgentEditMember.cshtml";
        public const string MasterEditMemberCshtml = "~/Areas/CustomerSetting/Views/Keno/Setting/Views/MasterOnMember/MasterEditMember.cshtml";

        public const string GetSuperSettingCriteriaKeyName = "Acc_CustomerSelEditSuperKeno";
        public const string UpdateSuperBetSettingCriteriaKeyName = "Acc_CustomerUpdSuperKeno_BetSetting";
        public const string UpdateSuperPositionTakingCriteriaKeyName = "Acc_CustomerUpdSuperKeno";

        public const string GetMasterSettingCriteriaKeyName = "Acc_CustomerSelEditMasterKeno";
        public const string UpdateMasterBetSettingCriteriaKeyName = "Acc_CustomerUpdMasterKeno_BetSetting";
        public const string UpdateMasterPositionTakingCriteriaKeyName = "Acc_CustomerUpdMasterKeno";

        public const string GetAgentSettingCriteriaKeyName = "Acc_CustomerSelEditAgentKeno";
        public const string UpdateAgentBetSettingCriteriaKeyName = "Acc_CustomerUpdAgentKeno_BetSetting";
        public const string UpdateAgentPositionTakingCriteriaKeyName = "Acc_CustomerUpdAgentKeno";

        public const string AgentGetMemberSettingCriteriaKeyName = "Acc_CustomerSelEditMemberKeno";
        public const string AgentUpdateMemberBetSettingCriteriaKeyName = "Acc_CustomerUpdMemberKeno_BetSetting";
        public const string AgentUpdateMemberPositionTakingCriteriaKeyName = "Acc_CustomerUpdMemberKeno_Positiontaking";

        public const string SuperGetMemberSettingCriteriaKeyName = "Acc_CustomerSelSEditCustomerKeno";
        public const string SuperUpdateMemberPositionTakingCriteriaKeyName = "Acc_CustomerUpdMemberKeno_SAg";

        public const string MasterGetMemberSettingCriteriaKeyName = "Acc_CustomerSelMEditCustomerKeno";
        public const string MasterUpdateMemberPositionTakingCriteriaKeyName = "Acc_CustomerUpdMemberKeno_MAg";
    }
}