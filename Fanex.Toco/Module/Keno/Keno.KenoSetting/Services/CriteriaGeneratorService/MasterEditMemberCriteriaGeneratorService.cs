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
        public MasterEditMemberCriteriaGeneratorService()
            : base(null)
        {
        }

        public MasterEditMemberCriteriaGeneratorService(IKenoSession kenoSession)
            : base(kenoSession)
        {
        }

        /// <summary>
        /// Creates the update bet setting criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="betSetting">The bet setting.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdateBetSettingCriteria(CustomerInformation customerInformation, BetSetting betSetting)
        {
            return new InvalidCriteria();
        }

        /// <summary>
        /// Creates the update position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="positionTaking">The position taking.</param>
        /// <returns>
        /// The instance of ICriteria.
        /// </returns>
        public ICriteria CreateUpdatePositionTakingCriteria(CustomerInformation customerInformation, dynamic positionTaking)
        {
            return CreateUpdateMemberPositionTakingCriteria(customerInformation, positionTaking);
        }

        /// <summary>
        /// Creates the update member position taking criteria.
        /// </summary>
        /// <param name="customerInformation">The customer information.</param>
        /// <param name="memberPositionTaking">The member position taking.</param>
        /// <returns>The MasterUpdateMemberPositionTakingCriteria object.</returns>
        internal MasterUpdateMemberPositionTakingCriteria CreateUpdateMemberPositionTakingCriteria(
            CustomerInformation customerInformation,
            MasterEditMemberPositionTaking memberPositionTaking)
        {
            return new MasterUpdateMemberPositionTakingCriteria()
            {
                CustId = customerInformation.CustId,
                SportType = Constants.SportType,
                BetType = memberPositionTaking.BetTypeId,
                MPositionTaking = memberPositionTaking.M1,
                Auto_MPositionTaking_Mem = memberPositionTaking.UM1,
                IsAuto_MPositionTaking_Mem = memberPositionTaking.IM1,
                IsAuto_MPositionTaking = memberPositionTaking.IM2,
                UpdatePositionTaking = memberPositionTaking.IsUpdateM1,
                UpdateAutoPositionTaking = memberPositionTaking.IsUpdateUM1,
                SubAccId = KenoSession.SubAccId,
                UserId = KenoSession.UserId,
                Ip = KenoSession.Ip,
                Language = customerInformation.Language
            };
        }
    }
}