// -----------------------------------------------------------------------
// <copyright file="MasterEditMemberCriteriaGeneratorServiceTests.cs" company="Nexcel Solutions Vietnam">
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
    public class MasterEditMemberCriteriaGeneratorServiceTests
    {
        private IKenoSession _stubKenoSession;
        private MasterEditMemberSettingViewModel _masterEditMemberSetting;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _masterEditMemberSetting = GenerateFakeMasterEditMemberSettingViewModel(100);
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
            var criteriaGeneratorService = new MasterEditMemberCriteriaGeneratorService(_stubKenoSession);

            // Act
            var criteria = criteriaGeneratorService.CreateUpdateBetSettingCriteria(_masterEditMemberSetting.CustomerInformation, _masterEditMemberSetting.BetSettings[0]);

            // Assert
            Assert.IsInstanceOf<InvalidCriteria>(criteria);
        }

        [Test]
        public void CreateUpdateSuperPositionTakingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var criteriaGeneratorService = new MasterEditMemberCriteriaGeneratorService(_stubKenoSession);

            // Act
            var criteria = criteriaGeneratorService.CreateUpdateMemberPositionTakingCriteria(_masterEditMemberSetting.CustomerInformation, _masterEditMemberSetting.PositionTakings[0]);

            // Assert
            Assert.IsInstanceOf<MasterUpdateMemberPositionTakingCriteria>(criteria);
            Assert.AreEqual(_masterEditMemberSetting.CustomerInformation.CustId, criteria.CustId);
            Assert.AreEqual(202, criteria.SportType);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].BetTypeId, criteria.BetType);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].M1, criteria.MPositionTaking);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].UM1, criteria.Auto_MPositionTaking_Mem);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].IM1, criteria.IsAuto_MPositionTaking_Mem);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].IM2, criteria.IsAuto_MPositionTaking);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].IsUpdateM1, criteria.UpdatePositionTaking);
            Assert.AreEqual(_masterEditMemberSetting.PositionTakings[0].IsUpdateUM1, criteria.UpdateAutoPositionTaking);
        }

        private MasterEditMemberSettingViewModel GenerateFakeMasterEditMemberSettingViewModel(int custId)
        {
            var masterEditMemberSetting = new MasterEditMemberSettingViewModel();

            masterEditMemberSetting.CustomerInformation = new CustomerInformation()
            {
                CustId = custId,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Super001"
            };

            masterEditMemberSetting.BetSettings = new List<BetSetting>() 
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

            masterEditMemberSetting.PositionTakings = new List<MasterEditMemberPositionTaking>() 
            {
                new MasterEditMemberPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    M1 = 0.85,
                    UM1 = 0.6,
                    IM1 = true,
                    IM2 = true,
                    IsUpdateM1 =true,
                    IsUpdateUM1 = true
                },
                new MasterEditMemberPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    M1 = 0.85,
                    UM1 = 0.6,
                    IM1 = true,
                    IM2 = true,
                    IsUpdateM1 =true,
                    IsUpdateUM1 = true
                },
                new MasterEditMemberPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    M1 = 0.85,
                    UM1 = 0.6,
                    IM1 = true,
                    IM2 = true,
                    IsUpdateM1 =true,
                    IsUpdateUM1 = true
                }
            };

            return masterEditMemberSetting;
        }
    }
}
