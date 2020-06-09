namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Data.Repository;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Helpers;

    public class AgentEditMemberCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        private readonly bool cloneSetting;

        public AgentEditMemberCriteriaGeneratorService(CustomerInformation customerInformation, bool cloneSetting)
            : base(null, customerInformation)
        {
            this.cloneSetting = cloneSetting;
        }

        public AgentEditMemberCriteriaGeneratorService(IKenoSession kenoSession, CustomerInformation customerInformation)
            : base(kenoSession, customerInformation)
        {
        }

        /// <summary>
        /// Creates the update bet setting criteria.
        /// </summary>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting, byte? systemId)
        {
            var status = betSetting.IsStatusUpdated;

            if(KenoSession.IsBelongToSpecialGroups && !this.cloneSetting)
            {
                status = true;
            }

            return new AgentUpdateMemberBetSettingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = betSetting.BetTypeId,
                UpdateStatus = status,
                Disable = !betSetting.IsEnabled,
                UpdateMinMaxBet = betSetting.IsBetSettingUpdated,
                MinBet = betSetting.MinBet,
                MaxBet = betSetting.MaxBet,
                MaxPerMatch = betSetting.MaxPerMatch,
                UserId = CustomerInformation.UserId,
                SubAccId = CustomerInformation.SubAccId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language,
                SystemId = systemId ?? SystemIdHelper.GetFromConfigurationFile()
            };
        }

        /// <summary>
        /// Creates the update position taking criteria.
        /// </summary>
        /// <param name="positionTaking">The position taking.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdatePositionTakingCriteria(dynamic positionTaking)
        {
            return CreateUpdateMemberPositionTakingCriteria(positionTaking);
        }

        /// <summary>
        /// Creates the update member position taking criteria.
        /// </summary>
        /// <param name="memberPositionTaking">The member position taking.</param>
        /// <returns>
        /// The AgentUpdateMemberPositionTakingCriteria object.
        /// </returns>
        internal AgentUpdateMemberPositionTakingCriteria CreateUpdateMemberPositionTakingCriteria(AgentEditMemberPositionTaking memberPositionTaking)
        {
            return new AgentUpdateMemberPositionTakingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = memberPositionTaking.BetTypeId,
                UpdatePT = memberPositionTaking.IsUpdateA1,
                A1 = memberPositionTaking.A1,
                UpdateComm = false,
                Discount = 0,
                SubAccId = CustomerInformation.SubAccId,
                UserId = CustomerInformation.UserId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}