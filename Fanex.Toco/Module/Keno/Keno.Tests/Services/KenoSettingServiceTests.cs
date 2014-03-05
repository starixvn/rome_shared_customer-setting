// -----------------------------------------------------------------------
// <copyright file="KenoSettingServiceTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/04">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.CustomerSetting.Module.Keno.Services;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.NAP.Framework.Data;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingServiceTests
    {
        private IKenoSettingRepository _mockRepository;
        private IKenoSettingRepository _stubRepository;
        private IKenoSession _stubKenoSession;
        private SuperSetting _superSetting;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _superSetting = GenerateFakeSuperSetting(100);
            _stubKenoSession = MockRepository.GenerateStub<IKenoSession>();
        }

        [SetUp]
        public void SetUp()
        {
            _mockRepository = MockRepository.GenerateMock<IKenoSettingRepository>();
            _stubRepository = MockRepository.GenerateStub<IKenoSettingRepository>();
        }

        [Test]
        public void GetSuperSetting_CustIdIsValid_ShouldReturnValidSuperSetting()
        {
            // Arrange
            _mockRepository.Stub(s => s.GetSuperSetting(null))
                .IgnoreArguments()
                .Repeat.Any()
                .Do((Func<ICriteria, SuperSetting>)(c => GenerateFakeSuperSetting(c)));
            var kenoSettingService = new KenoSettingService(_mockRepository, _stubKenoSession);

            // Act
            var superSetting = kenoSettingService.GetSuperSetting(200);

            // Assert
            Assert.IsInstanceOf<SuperSetting>(superSetting);
            Assert.AreEqual(200, superSetting.CustomerInformation.CustId);
            Assert.AreEqual("Paul", superSetting.CustomerInformation.FirstName);
            Assert.AreEqual(3, superSetting.BetSettings.Count);
            Assert.AreEqual(3, superSetting.PositionTakings.Count);
        }

        [Test]
        public void UpdateSuperBetSetting_ParamsAreValid_ShouldReturnInstanceOfResult()
        {
            // Arrange
            _stubRepository.Stub(s => s.UpdateSuperBetSetting(Arg<ICriteria>.Is.Anything))
                .IgnoreArguments()
                .Return(new Result() { });
            var kenoSettingService = new KenoSettingService(_stubRepository, _stubKenoSession);

            // Act
            var result = kenoSettingService.UpdateSuperBetSetting(_superSetting.CustomerInformation, _superSetting.BetSettings[0]);

            // Assert
            Assert.IsInstanceOf<Result>(result);
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Exception from DB...")]
        public void UpdateSuperBetSetting_RepositoryOccursException_ShouldThrowsException()
        {
            // Arrange
            _mockRepository.Stub(s => s.UpdateSuperBetSetting(Arg<ICriteria>.Is.Anything))
                .IgnoreArguments()
                .Throw(new Exception("Exception from DB..."));
            var kenoSettingService = new KenoSettingService(_mockRepository, _stubKenoSession);

            // Act
            var result = kenoSettingService.UpdateSuperBetSetting(_superSetting.CustomerInformation, _superSetting.BetSettings[0]);
        }

        [Test]
        public void UpdateSuperCustomerSetting_ValidCustomerSetting_ShouldReturnAListOfResults()
        {
            // Arrange
            _mockRepository.Expect(r => r.UpdateSuperBetSetting(Arg<ICriteria>.Is.Anything)).Return(new Result() { });
            _mockRepository.Expect(r => r.UpdateSuperPositionTaking(Arg<ICriteria>.Is.Anything)).Return(new Result() { });
            var kenoSettingService = new KenoSettingService(_stubRepository, _stubKenoSession);

            // Act
            var results = kenoSettingService.UpdateSuperCustomerSetting(_superSetting);

            // Assert
            Assert.IsInstanceOf<IList<Result>>(results);
            Assert.AreEqual(6, results.Count);
        }

        [Test]
        public void CreateUpdateSuperBetSettingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var kenoSettingService = new KenoSettingService(null, _stubKenoSession);

            // Act
            var criteria = kenoSettingService.CreateUpdateSuperBetSettingCriteria(_superSetting.CustomerInformation, _superSetting.BetSettings[0]);

            // Assert
            Assert.IsInstanceOf<UpdateSuperBetSettingCriteria>(criteria);
            Assert.AreEqual(_superSetting.CustomerInformation.CustId, criteria.CustId);
            Assert.AreEqual(_superSetting.BetSettings[0].BetTypeId, criteria.BetTypeId);
            Assert.AreEqual(!_superSetting.BetSettings[0].IsEnabled, criteria.Disable);
            Assert.AreEqual(!_superSetting.BetSettings[0].IsDownlinesEnabled, criteria.DisableDownLines);
            Assert.AreEqual(_superSetting.BetSettings[0].MinBet, criteria.MinBet);
            Assert.AreEqual(_superSetting.BetSettings[0].MaxBet, criteria.MaxBet);
            Assert.AreEqual(_superSetting.BetSettings[0].MaxPerMatch, criteria.MaxPerMatch);
        }

        [Test]
        public void CreateUpdateSuperPositionTakingCriteria_ShouldReturnExactCriteria()
        {
            // Arrange
            var kenoSettingService = new KenoSettingService(null, _stubKenoSession);

            // Act
            var criteria = kenoSettingService.CreateUpdateSuperPositionTakingCriteria(_superSetting.CustomerInformation, _superSetting.PositionTakings[0]);

            // Assert
            Assert.IsInstanceOf<UpdateSuperPositionTakingCriteria>(criteria);
            Assert.AreEqual(_superSetting.CustomerInformation.CustId, criteria.CustId);
            Assert.AreEqual(_superSetting.PositionTakings[0].SportTypeId, criteria.SportType);
            Assert.AreEqual(_superSetting.PositionTakings[0].BetTypeId, criteria.BetType);
            Assert.AreEqual(_superSetting.PositionTakings[0].L4, criteria.SPositionTaking);
            Assert.AreEqual(_superSetting.PositionTakings[0].N4, criteria.Min_PositionTaking);
            Assert.AreEqual(_superSetting.PositionTakings[0].SComm, criteria.Discount);
            Assert.AreEqual(_superSetting.PositionTakings[0].IsUpdatePT, criteria.UpdatePositionTaking);
            Assert.AreEqual(_superSetting.PositionTakings[0].IsUpdateN4, criteria.UpdateMinPositionTaking);
            Assert.AreEqual(_superSetting.PositionTakings[0].IsUpdateSCo, criteria.UpdateCommission);
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository = null;
            _stubRepository = null;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _superSetting = null;
            _stubKenoSession = null;
        }

        private SuperSetting GenerateFakeSuperSetting(ICriteria criteria)
        {
            return GenerateFakeSuperSetting(((GetSuperSettingCriteria)criteria).CustId);
        }

        private SuperSetting GenerateFakeSuperSetting(int custId)
        {
            var superSetting = new SuperSetting();

            superSetting.CustomerInformation = new CustomerInformation()
            {
                CustId = custId,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Super001"
            };

            superSetting.BetSettings = new List<BetSetting>() 
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

            superSetting.PositionTakings = new List<SuperPositionTaking>() 
            {
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SComm = 0.0095,
                    SCommMax = 0.01
                },
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SComm = 0.0095,
                    SCommMax = 0.01
                },
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SComm = 0.0095,
                    SCommMax = 0.01
                }
            };

            return superSetting;
        }
    }
}