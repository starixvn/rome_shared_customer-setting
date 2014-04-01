// -----------------------------------------------------------------------
// <copyright file="SuperEditMemberCriteriaGeneratorServiceTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/04">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Tests.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Criteria;
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
    public class SuperEditMemberCriteriaGeneratorServiceTests
    {
        private IKenoSession _stubKenoSession;
        private SuperEditMemberSettingViewModel _superEditMemberSetting;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _superEditMemberSetting = GenerateFakeSuperEditMemberSettingViewModel(100);
            _stubKenoSession = MockRepository.GenerateStub<IKenoSession>();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _stubKenoSession = null;
        }

        [Test]
        public void CreateUpdateSuperBetSettingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var criteriaGeneratorService = new SuperEditMemberCriteriaGeneratorService(_stubKenoSession);

            // Act
            var criteria = criteriaGeneratorService.CreateUpdateBetSettingCriteria(_superEditMemberSetting.CustomerInformation, _superEditMemberSetting.BetSettings[0]);

            // Assert
            Assert.IsInstanceOf<InvalidCriteria>(criteria);
        }

        [Test]
        public void CreateUpdateSuperPositionTakingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var criteriaGeneratorService = new SuperEditMemberCriteriaGeneratorService(_stubKenoSession);

            // Act
            var criteria = criteriaGeneratorService.CreateUpdateMemberPositionTakingCriteria(_superEditMemberSetting.CustomerInformation, _superEditMemberSetting.PositionTakings[0]);

            // Assert
            Assert.IsInstanceOf<SuperUpdateMemberPositionTakingCriteria>(criteria);
            Assert.AreEqual(_superEditMemberSetting.CustomerInformation.CustId, criteria.CustId);
            Assert.AreEqual(202, criteria.SportType);
            Assert.AreEqual(_superEditMemberSetting.PositionTakings[0].BetTypeId, criteria.BetType);
            Assert.AreEqual(_superEditMemberSetting.PositionTakings[0].S1, criteria.SPositionTaking);
            Assert.AreEqual(_superEditMemberSetting.PositionTakings[0].US1, criteria.Auto_SPositionTaking);
            Assert.AreEqual(_superEditMemberSetting.PositionTakings[0].IS1, criteria.IsAuto_SPositionTaking);
            Assert.AreEqual(_superEditMemberSetting.PositionTakings[0].IsUpdateS1, criteria.UpdatePositionTaking);
            Assert.AreEqual(_superEditMemberSetting.PositionTakings[0].IsUpdateUS1, criteria.UpdateAutoPositionTaking);
        }

        private SuperEditMemberSettingViewModel GenerateFakeSuperEditMemberSettingViewModel(int custId)
        {
            var superEditMemberSetting = new SuperEditMemberSettingViewModel();

            superEditMemberSetting.CustomerInformation = new CustomerInformation()
            {
                CustId = custId,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Super001"
            };

            superEditMemberSetting.BetSettings = new List<BetSetting>() 
            {
                new BetSetting()
                {
                    BetTypeId = 1,
                    BetTypeName = "Beijing",
                    MinBet = 0,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = true,
                    IsDownlinesEnabled = true,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 0,
                        MaxPerMatch = 0
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    MinBet = 10,
                    MaxBet = 0,
                    MaxPerMatch = 0,
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
                    MinBet = 0,
                    MaxBet = 0,
                    MaxPerMatch = 0,
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

            superEditMemberSetting.PositionTakings = new List<SuperEditMemberPositionTaking>() 
            {
                new SuperEditMemberPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    S1 = 0.85,
                    US1 = 0.6,
                    IS1 = true,
                    IsUpdateS1 = true,
                    IsUpdateUS1 = true
                },
                new SuperEditMemberPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    S1 = 0.85,
                    US1 = 0.6,
                    IS1 = true,
                    IsUpdateS1 = true,
                    IsUpdateUS1 = true
                },
                new SuperEditMemberPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    S1 = 0.85,
                    US1 = 0.6,
                    IS1 = true,
                    IsUpdateS1 = true,
                    IsUpdateUS1 = true
                }
            };

            return superEditMemberSetting;
        }
    }
}
