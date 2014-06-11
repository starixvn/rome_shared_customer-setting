// --------------------------------------------------------------------------
// <copyright file="SuperEditMemberCriteriaGeneratorService.cs" company="Nexcel Solutions Vietnam">
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
    public class SuperEditMemberCriteriaGeneratorService : CriteriaGeneratorServiceBase, ICriteriaGeneratorService
    {
        public SuperEditMemberCriteriaGeneratorService(CustomerInformation customerInformation)
            : base(null, customerInformation)
        {
        }

        public SuperEditMemberCriteriaGeneratorService(IKenoSession kenoSession, CustomerInformation customerInformation)
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
        /// The SuperUpdateMemberPositionTakingCriteria object.
        /// </returns>
        internal SuperUpdateMemberPositionTakingCriteria CreateUpdateMemberPositionTakingCriteria(SuperEditMemberPositionTaking memberPositionTaking)
        {
            return new SuperUpdateMemberPositionTakingCriteria()
            {
                CustId = CustomerInformation.CustId,
                SportType = Constants.SportType,
                BetType = memberPositionTaking.BetTypeId,
                S1 = memberPositionTaking.S1,
                US1 = memberPositionTaking.US1,
                IS1 = memberPositionTaking.IS1,
                IS3 = memberPositionTaking.IS3,
                UpdatePositionTaking = memberPositionTaking.IsUpdateS1,
                UpdateAutoPositionTaking = memberPositionTaking.IsUpdateUS1,
                SubAccId = CustomerInformation.SubAccId,
                UserId = CustomerInformation.UserId,
                Ip = KenoSession.Ip,
                Language = CustomerInformation.Language
            };
        }
    }
}