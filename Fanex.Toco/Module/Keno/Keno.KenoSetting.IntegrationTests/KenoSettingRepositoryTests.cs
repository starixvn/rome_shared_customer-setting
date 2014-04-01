// --------------------------------------------------------------------------
// <copyright file="KenoSettingRepositoryTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.IntegrationTests
{
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories;
    using Fanex.Data.Configuration;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingRepositoryTests
    {
        private KenoSettingRepository _kenoSettingRepository;
        private const int SuperId = 14195486;
        private const int MasterId = 14195487;
        private const int AgentId = 14195488;
        private const int MemberId = 14195489;

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
            var criteria = new GetSuperSettingCriteria() { CustId = SuperId };

            // Act
            var result = _kenoSettingRepository.GetSuperSetting(criteria);
            result.AdjustInformationWhenLoadSetting(SuperId);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.Greater(result.BetSettings.Count, 0);
            Assert.IsNotNull(result.PositionTakings);
            Assert.Greater(result.PositionTakings.Count, 0);
        }

        [Test]
        public void UpdateSuperBetSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new UpdateSuperBetSettingCriteria()
            {
                CustId = SuperId,
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

            // Act
            var result = _kenoSettingRepository.UpdateBetSetting(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void UpdateSuperPositionTaking_ShouldRunNormally()
        {
            // Arrange
            var criteria = new UpdateSuperPositionTakingCriteria()
            {
                CustId = SuperId,
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

            // Act
            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetMasterSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new GetMasterSettingCriteria() { CustId = MasterId };

            // Act
            var result = _kenoSettingRepository.GetMasterSetting(criteria);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.Greater(result.BetSettings.Count, 0);
            Assert.IsNotNull(result.PositionTakings);
            Assert.Greater(result.PositionTakings.Count, 0);
        }

        [Test]
        public void UpdateMasterBetSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new UpdateMasterBetSettingCriteria()
            {
                CustId = MasterId,
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

            // Act
            var result = _kenoSettingRepository.UpdateBetSetting(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void UpdateMasterPositionTaking_ShouldRunNormally()
        {
            // Arrange
            var criteria = new UpdateMasterPositionTakingCriteria()
            {
                CustId = MasterId,
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

            // Act
            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAgentSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new GetAgentSettingCriteria() { CustId = AgentId };

            // Act
            var result = _kenoSettingRepository.GetAgentSetting(criteria);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.Greater(result.BetSettings.Count, 0);
            Assert.IsNotNull(result.PositionTakings);
            Assert.Greater(result.PositionTakings.Count, 0);
        }

        [Test]
        public void UpdateAgentBetSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new UpdateAgentBetSettingCriteria()
            {
                CustId = AgentId,
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

            // Act
            var result = _kenoSettingRepository.UpdateBetSetting(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void UpdateAgentPositionTaking_ShouldRunNormally()
        {
            // Arrange
            var criteria = new UpdateAgentPositionTakingCriteria()
            {
                CustId = AgentId,
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

            // Act
            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetSettingInCaseOfAgentEditMember_ShouldRunNormally()
        {
            // Arrange
            var criteria = new AgentGetMemberSettingCriteria() { CustId = MemberId };

            // Act
            var result = _kenoSettingRepository.GetSettingInCaseOfAgentEditMember(criteria);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.Greater(result.BetSettings.Count, 0);
            Assert.IsNotNull(result.PositionTakings);
            Assert.Greater(result.PositionTakings.Count, 0);
        }

        [Test]
        public void AgentUpdateMemberBetSetting_ShouldRunNormally()
        {
            // Arrange
            var criteria = new AgentUpdateMemberBetSettingCriteria()
            {
                CustId = MemberId,
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

            // Act
            var result = _kenoSettingRepository.UpdateBetSetting(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void AgentUpdateMemberPositionTaking_ShouldRunNormally()
        {
            // Arrange
            var criteria = new AgentUpdateMemberPositionTakingCriteria()
            {
                CustId = MemberId,
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

            // Act
            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetSettingInCaseOfSuperEditMember_ShouldRunNormally()
        {
            // Arrange
            var criteria = new SuperGetMemberSettingCriteria() { CustId = MemberId };

            // Act
            var result = _kenoSettingRepository.GetSettingInCaseOfSuperEditMember(criteria);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.Greater(result.BetSettings.Count, 0);
            Assert.IsNotNull(result.PositionTakings);
            Assert.Greater(result.PositionTakings.Count, 0);
        }

        [Test]
        public void SuperUpdateMemberPositionTaking_ShouldRunNormally()
        {
            // Arrange
            var criteria = new SuperUpdateMemberPositionTakingCriteria()
            {
                CustId = MemberId,
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

            // Act
            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetSettingInCaseOfMasterEditMember_ShouldRunNormally()
        {
            // Arrange
            var criteria = new MasterGetMemberSettingCriteria() { CustId = MemberId };

            // Act
            var result = _kenoSettingRepository.GetSettingInCaseOfMasterEditMember(criteria);

            // Assert
            Assert.IsNotNull(result.CustomerInformation);
            Assert.IsNotNull(result.BetSettings);
            Assert.Greater(result.BetSettings.Count, 0);
            Assert.IsNotNull(result.PositionTakings);
            Assert.Greater(result.PositionTakings.Count, 0);
        }

        [Test]
        public void MasterUpdateMemberPositionTaking_ShouldRunNormally()
        {
            // Arrange
            var criteria = new MasterUpdateMemberPositionTakingCriteria()
            {
                CustId = MemberId,
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

            // Act
            var result = _kenoSettingRepository.UpdatePositionTaking(criteria);

            // Assert
            Assert.AreEqual(1, result.Count);
        }
    }
}
