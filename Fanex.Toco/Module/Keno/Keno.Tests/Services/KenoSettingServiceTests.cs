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
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.CustomerSetting.Module.Keno.Services;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.NAP.Framework.Data;
    using NUnit.Framework;
    using Rhino.Mocks;
    using Fanex.CustomerSetting.Module.Core.Criteria;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingServiceTests
    {
        private IKenoSettingRepository _mockRepository;
        private IKenoSettingRepository _stubRepository;
        private ICriteriaGeneratorService _stubCriteriaGenerator;
        private SuperSettingViewModel _customerSetting;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _customerSetting = GenerateFakeCustomerSettingViewModel(100);
        }

        [SetUp]
        public void SetUp()
        {
            _mockRepository = MockRepository.GenerateMock<IKenoSettingRepository>();
            _stubRepository = MockRepository.GenerateStub<IKenoSettingRepository>();
            _stubCriteriaGenerator = MockRepository.GenerateStub<ICriteriaGeneratorService>();
            var stubKenoSession = MockRepository.GenerateStub<IKenoSession>();
            _stubCriteriaGenerator.Stub(r => r.KenoSession).Return(stubKenoSession);
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository = null;
            _stubRepository = null;
            _stubCriteriaGenerator = null;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _customerSetting = null;
        }

        [Test]
        public void GetSuperSetting_CustIdIsValid_ShouldReturnValidSuperSetting()
        {
            // Arrange
            _mockRepository.Stub(s => s.GetSuperSetting(null))
                .IgnoreArguments()
                .Repeat.Any()
                .Do((Func<ICriteria, SuperSettingViewModel>)(c => GenerateFakeCustomerSettingViewModel(c)));
            var kenoSettingService = new KenoSettingService(_mockRepository, null);

            // Act
            var superSetting = kenoSettingService.GetSuperSetting(200);

            // Assert
            Assert.IsInstanceOf<SuperSettingViewModel>(superSetting);
            Assert.AreEqual(200, superSetting.CustomerInformation.CustId);
            Assert.AreEqual("Paul", superSetting.CustomerInformation.FirstName);
            Assert.AreEqual(3, superSetting.BetSettings.Count);
            Assert.AreEqual(3, superSetting.PositionTakings.Count);
        }

        [Test]
        public void UpdateBetSetting_ParamsAreValid_ShouldReturnInstanceOfResultList()
        {
            // Arrange
            _stubRepository.Stub(s => s.UpdateBetSetting(Arg<ICriteria>.Is.Anything))
                .IgnoreArguments()
                .Return(new List<Result>() { });
            var kenoSettingService = new KenoSettingService(_stubRepository, _stubCriteriaGenerator);

            // Act
            var result = kenoSettingService.UpdateBetSetting(_customerSetting.CustomerInformation, _customerSetting.BetSettings[0]);

            // Assert
            Assert.IsInstanceOf<List<Result>>(result);
        }

        [Test]
        public void UpdateBetSetting_IsInvalidCriteria_ShouldReturnEmptyList()
        {
            // Arrange
            var kenoSettingService = new KenoSettingService(_stubRepository, _stubCriteriaGenerator);
            _stubCriteriaGenerator.Stub(c => c.CreateUpdateBetSettingCriteria(Arg<CustomerInformation>.Is.Anything, Arg<BetSetting>.Is.Anything))
                .IgnoreArguments()
                .Return(new InvalidCriteria());

            // Act
            var result = kenoSettingService.UpdateBetSetting(_customerSetting.CustomerInformation, _customerSetting.BetSettings[0]);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Exception from DB...")]
        public void UpdateBetSetting_RepositoryOccursException_ShouldThrowsException()
        {
            // Arrange
            _mockRepository.Stub(s => s.UpdateBetSetting(Arg<ICriteria>.Is.Anything))
                .IgnoreArguments()
                .Throw(new Exception("Exception from DB..."));
            var kenoSettingService = new KenoSettingService(_mockRepository, _stubCriteriaGenerator);

            // Act
            var result = kenoSettingService.UpdateBetSetting(_customerSetting.CustomerInformation, _customerSetting.BetSettings[0]);
        }

        [Test]
        public void UpdateBetSettings_ParamsAreValid_ShouldReturnInstanceOfResultList()
        {
            // Arrange
            _stubRepository.Stub(s => s.UpdateBetSetting(Arg<ICriteria>.Is.Anything))
                .IgnoreArguments()
                .Return(new List<Result>() { });
            var kenoSettingService = new KenoSettingService(_stubRepository, _stubCriteriaGenerator);

            // Act
            var result = kenoSettingService.UpdateBetSettings(_customerSetting);

            // Assert
            Assert.IsInstanceOf<List<Result>>(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void UpdateSuperCustomerSetting_ValidCustomerSetting_ShouldReturnAListOfResults()
        {
            // Arrange
            _stubRepository.Stub(r => r.UpdateBetSetting(Arg<ICriteria>.Is.Anything)).Return(new List<Result>() { new Result() });
            _stubRepository.Stub(r => r.UpdatePositionTaking(Arg<ICriteria>.Is.Anything)).Return(new List<Result>() { new Result() });
            var kenoSettingService = new KenoSettingService(_stubRepository, _stubCriteriaGenerator);

            // Act
            var results = kenoSettingService.UpdateSuperCustomerSetting(_customerSetting);

            // Assert
            Assert.IsInstanceOf<IList<Result>>(results);
            Assert.AreEqual(6, results.Count);
        }

        private SuperSettingViewModel GenerateFakeCustomerSettingViewModel(ICriteria criteria)
        {
            return GenerateFakeCustomerSettingViewModel(((GetSuperSettingCriteria)criteria).CustId);
        }

        private SuperSettingViewModel GenerateFakeCustomerSettingViewModel(int custId)
        {
            var superSetting = new SuperSettingViewModel();

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
