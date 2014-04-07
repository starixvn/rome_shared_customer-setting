// --------------------------------------------------------------------------
// <copyright file="MasterEditMemberCriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Fanex.CustomerSetting.Module.Core.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public class MasterEditMemberCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public MasterEditMemberCriteriaGeneratorService(CustomerInformation customerInformation)
            : base(null, customerInformation)
        {
        }

        public MasterEditMemberCriteriaGeneratorService(IKenoSession kenoSession, CustomerInformation customerInformation)
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
        public ICriteria CreateUpdateBetSettingCriteria(BetSetting betSetting)
        {
            return new InvalidCriteria();
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
        /// The MasterUpdateMemberPositionTakingCriteria object.
        /// </returns>
        internal MasterUpdateMemberPositionTakingCriteria CreateUpdateMemberPositionTakingCriteria(MasterEditMemberPositionTaking memberPositionTaking)
        {
            return new MasterUpdateMemberPositionTakingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = memberPositionTaking.BetTypeId,
                MPositionTaking = memberPositionTaking.M1,
                Auto_MPositionTaking_Mem = memberPositionTaking.UM1,
                IsAuto_MPositionTaking_Mem = memberPositionTaking.IM1,
                IsAuto_MPositionTaking = memberPositionTaking.IM2,
                UpdatePositionTaking = memberPositionTaking.IsUpdateM1,
                UpdateAutoPositionTaking = memberPositionTaking.IsUpdateUM1,
                SubAccId = CustomerInformation.SubAccId,
                UserId = CustomerInformation.UserId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}