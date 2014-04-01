// -----------------------------------------------------------------------
// <copyright file="AgentCriteriaGeneratorServiceTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/04">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Tests.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Services;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class AgentCriteriaGeneratorServiceTests
    {
        private IKenoSession _stubKenoSession;
        private AgentSettingViewModel _agentSetting;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _agentSetting = GenerateFakeAgentSettingViewModel(100);
            _stubKenoSession = MockRepository.GenerateStub<IKenoSession>();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _stubKenoSession = null;
        }

        [Test]
        public void CreateUpdateBetSettingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var criteriaGeneratorService = new SuperCriteriaGeneratorService(_stubKenoSession);

            // Act
            var criteria = criteriaGeneratorService.CreateUpdateBetSettingCriteria(_agentSetting.CustomerInformation, _agentSetting.BetSettings[0]);

            // Assert
            Assert.IsInstanceOf<UpdateSuperBetSettingCriteria>(criteria);
            Assert.AreEqual(_agentSetting.CustomerInformation.CustId, ((UpdateSuperBetSettingCriteria)criteria).CustId);
            Assert.AreEqual(_agentSetting.BetSettings[0].BetTypeId, ((UpdateSuperBetSettingCriteria)criteria).BetType);
            Assert.AreEqual(!_agentSetting.BetSettings[0].IsEnabled, ((UpdateSuperBetSettingCriteria)criteria).Disable);
            Assert.AreEqual(!_agentSetting.BetSettings[0].IsDownlinesEnabled, ((UpdateSuperBetSettingCriteria)criteria).DisableDownLines);
            Assert.AreEqual(_agentSetting.BetSettings[0].MinBet, ((UpdateSuperBetSettingCriteria)criteria).MinBet);
            Assert.AreEqual(_agentSetting.BetSettings[0].MaxBet, ((UpdateSuperBetSettingCriteria)criteria).MaxBet);
            Assert.AreEqual(_agentSetting.BetSettings[0].MaxPerMatch, ((UpdateSuperBetSettingCriteria)criteria).MaxPerMatch);
        }

        [Test]
        public void CreateUpdateAgentPositionTakingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var criteriaGeneratorService = new AgentCriteriaGeneratorService(_stubKenoSession);

            // Act
            var criteria = criteriaGeneratorService.CreateUpdateAgentPositionTakingCriteria(_agentSetting.CustomerInformation, _agentSetting.PositionTakings[0]);

            // Assert
            Assert.IsInstanceOf<UpdateAgentPositionTakingCriteria>(criteria);
            Assert.AreEqual(_agentSetting.CustomerInformation.CustId, criteria.CustId);
            Assert.AreEqual(202, criteria.SportType);
            Assert.AreEqual(_agentSetting.PositionTakings[0].BetTypeId, criteria.BetType);
            Assert.AreEqual(_agentSetting.PositionTakings[0].M2, criteria.MPositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].UM2, criteria.Auto_MPositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].IM2, criteria.IsAuto_MPositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].A2, criteria.APositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].N2, criteria.APositionTaking_Min);
            Assert.AreEqual(_agentSetting.PositionTakings[0].IsUpdateAllMembers, criteria.UpdateAllMembers);
            Assert.AreEqual(_agentSetting.PositionTakings[0].NewMemberPT, criteria.NewMPositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].IsUpdateA2, criteria.UpdatePositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].IsUpdateN2, criteria.UpdateMinPositionTaking);
            Assert.AreEqual(_agentSetting.PositionTakings[0].IsUpdateUM2, criteria.UpdateAutoPositionTaking);
            Assert.AreEqual(0, criteria.Discount);
            Assert.AreEqual(false, criteria.UpdateCommission);
        }

        private AgentSettingViewModel GenerateFakeAgentSettingViewModel(int custId)
        {
            var agentSetting = new AgentSettingViewModel();

            agentSetting.CustomerInformation = new CustomerInformation()
            {
                CustId = custId,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Super001"
            };

            agentSetting.BetSettings = new List<BetSetting>() 
            {
                new BetSetting()
                {
                    BetTypeId = 1,
                    BetTypeName = "Beijing",
                    MinBet = 10,
                    MaxBet = 10,
                    MaxPerMatch = 10,
                    IsEnabled = true,
                    IsDownlinesEnabled = true,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 10,
                        MaxPerMatch = 10
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    MinBet = 10,
                    MaxBet = 10,
                    MaxPerMatch = 10,
                    IsEnabled = false,
                    IsDownlinesEnabled = true,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 400
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    MinBet = 10,
                    MaxBet = 10,
                    MaxPerMatch = 10,
                    IsEnabled = true,
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 300
                    }
                },
            };

            agentSetting.PositionTakings = new List<AgentPositionTaking>() 
            {
                new AgentPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    M2 = 0.5,
                    UM2 = 0.6,
                    IM2 = true,
                    A2 = 0.7,
                    N2 = 0.8,
                    IsUpdateAllMembers = true,
                    NewMemberPT = 0.9,
                    IsUpdateA2 = true,
                    IsUpdateN2 = true,
                    IsUpdateUM2 = true
                },
                new AgentPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    M2 = 0.5,
                    UM2 = 0.6,
                    IM2 = true,
                    A2 = 0.7,
                    N2 = 0.8,
                    IsUpdateAllMembers = true,
                    NewMemberPT = 0.9,
                    IsUpdateA2 = true,
                    IsUpdateN2 = true,
                    IsUpdateUM2 = true
                },
                new AgentPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    M2 = 0.5,
                    UM2 = 0.6,
                    IM2 = true,
                    A2 = 0.7,
                    N2 = 0.8,
                    IsUpdateAllMembers = true,
                    NewMemberPT = 0.9,
                    IsUpdateA2 = true,
                    IsUpdateN2 = true,
                    IsUpdateUM2 = true
                }
            };

            return agentSetting;
        }
    }
}
