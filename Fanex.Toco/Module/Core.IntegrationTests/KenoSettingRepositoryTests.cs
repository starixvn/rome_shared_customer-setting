// --------------------------------------------------------------------------
// <copyright file="KenoSettingRepositoryTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.IntegrationTests
{
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.Data.Configuration;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingRepositoryTests
    {
        private KenoSettingRepository _kenoSettingRepository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DbSettingProviderManager.Current.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _kenoSettingRepository = new KenoSettingRepository();
        }

        [TearDown]
        public void TearDown()
        {
            _kenoSettingRepository = null;
        }

        [Test]
        public void GetSuperSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new GetSuperSettingCriteria() { CustId = 137509731 };

            // Act
            var result = _kenoSettingRepository.GetSuperSetting(criteria);
            result.UpdateRemainingInformation(137509731);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.AreEqual(7, result.BetSettings.Count);
            Assert.IsNotNull(result.PositionTakings);
            Assert.AreEqual(7, result.PositionTakings.Count);
        }

        [Test]
        public void UpdateSuperBetSetting_ShouldRunNormally()
        {
            var criteria = new UpdateSuperBetSettingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                UpdateStatus = false,
                Disable = true,
                DisableDownLines = true,
                UpdateMinMaxBet = false,
                MinBet = 10,
                MaxBet = 10,
                MaxPerMatch = 10,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdateBetSetting(criteria);
        }

        [Test]
        public void UpdateSuperPositionTaking_ShouldRunNormally()
        {
            var criteria = new UpdateSuperPositionTakingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                SPositionTaking = 10,
                Min_PositionTaking = 10,
                Discount = 0,
                UpdatePositionTaking = false,
                UpdateMinPositionTaking = false,
                UpdateCommission = false,
                UserId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        [Test]
        public void GetMasterSetting_ShouldRunNormally()
        {
            var criteria = new GetMasterSettingCriteria() { CustId = 137509798 };
            var result = _kenoSettingRepository.GetMasterSetting(criteria);
        }

        [Test]
        public void UpdateMasterBetSetting_ShouldRunNormally()
        {
            var criteria = new UpdateMasterBetSettingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                UpdateStatus = false,
                Disable = true,
                DisableDownLines = true,
                UpdateMinMaxBet = false,
                MinBet = 10,
                MaxBet = 10,
                MaxPerMatch = 10,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdateBetSetting(criteria);
        }

        [Test]
        public void UpdateMasterPositionTaking_ShouldRunNormally()
        {
            var criteria = new UpdateMasterPositionTakingCriteria()
            {
                CustId = 1,
                SportType = Constants.SportType,
                BetType = 1,
                SPositionTaking = 10,
                Auto_SPositionTaking = 10,
                IsAuto_SPositionTaking = true,
                MPositionTaking = 10,
                MPositionTaking_Min = 10,
                UpdateAllMembers = true,
                NewSPositionTaking = 10,
                Discount = 0,
                UpdatePositiontaking = false,
                UpdateMinPositiontaking = false,
                UpdateAutoPositionTaking = false,
                UpdateCommission = false,
                UserId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        [Test]
        public void GetAgentSetting_ShouldRunNormally()
        {
            var criteria = new GetAgentSettingCriteria() { CustId = 1 };
            var result = _kenoSettingRepository.GetAgentSetting(criteria);
        }

        [Test]
        public void UpdateAgentBetSetting_ShouldRunNormally()
        {
            var criteria = new UpdateAgentBetSettingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                UpdateStatus = false,
                Disable = true,
                DisableDownLines = true,
                UpdateMinMaxBet = false,
                MinBet = 10,
                MaxBet = 10,
                MaxPerMatch = 10,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdateBetSetting(criteria);
        }

        [Test]
        public void UpdateAgentPositionTaking_ShouldRunNormally()
        {
            var criteria = new UpdateAgentPositionTakingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                MPositionTaking = 10,
                Auto_MPositionTaking = 10,
                IsAuto_MPositionTaking = true,
                APositionTaking = 10,
                APositionTaking_Min = 10,
                UpdateAllMembers = false,
                NewMPositionTaking = 10,
                Discount = 0,
                UpdatePositionTaking = false,
                UpdateMinPositionTaking = false,
                UpdateAutoPositionTaking = false,
                UpdateCommission = false,
                UserId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        [Test]
        public void GetSettingInCaseOfAgentEditMember_ShouldRunNormally()
        {
            var criteria = new AgentGetMemberSettingCriteria() { CustId = 137459563 };
            var result = _kenoSettingRepository.GetSettingInCaseOfAgentEditMember(criteria);
        }

        [Test]
        public void AgentUpdateMemberBetSetting_ShouldRunNormally()
        {
            var criteria = new AgentUpdateMemberBetSettingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                UpdateStatus = false,
                Disable = true,
                UpdateMinMaxBet = false,
                MinBet = 10,
                MaxBet = 10,
                MaxPerMatch = 10,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdateBetSetting(criteria);
        }

        [Test]
        public void AgentUpdateMemberPositionTaking_ShouldRunNormally()
        {
            var criteria = new AgentUpdateMemberPositionTakingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                UpdatePT = false,
                APositionTaking = 10,
                UpdateComm = false,
                Discount = 0,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        [Test]
        public void GetSettingInCaseOfSuperEditMember_ShouldRunNormally()
        {
            var criteria = new SuperGetMemberSettingCriteria() { CustId = 137459563 };
            var result = _kenoSettingRepository.GetSettingInCaseOfSuperEditMember(criteria);
        }

        [Test]
        public void SuperUpdateMemberPositionTaking_ShouldRunNormally()
        {
            var criteria = new SuperUpdateMemberPositionTakingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                SPositionTaking = 10,
                Auto_SPositionTaking = 10,
                IsAuto_SPositionTaking = true,
                UpdatePositionTaking = false,
                UpdateAutoPositionTaking = false,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);
        }

        [Test]
        public void GetSettingInCaseOfMasterEditMember_ShouldRunNormally()
        {
            var criteria = new MasterGetMemberSettingCriteria() { CustId = 137459563 };
            var result = _kenoSettingRepository.GetSettingInCaseOfMasterEditMember(criteria);
        }

        [Test]
        public void MasterUpdateMemberPositionTaking_ShouldRunNormally()
        {
            var criteria = new MasterUpdateMemberPositionTakingCriteria()
            {
                CustId = 137509707,
                SportType = Constants.SportType,
                BetType = 1,
                MPositionTaking = 10,
                Auto_MPositionTaking_Mem = 10,
                IsAuto_MPositionTaking_Mem = true,
                IsAuto_MPositionTaking = true,
                UpdatePositionTaking = false,
                UpdateAutoPositionTaking = false,
                UserId = 1,
                SubAccId = 1,
                Ip = string.Empty,
                Language = null
            };

            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);
        }
    }
}
