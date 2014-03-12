// -----------------------------------------------------------------------
// <copyright file="KenoSettingRepositoryTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Tests.Repositories
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingRepositoryTests
    {
        [Test]
        public void ReadAgentInformationData_ShouldMappingCorrectly()
        {
            // Arrange
            var mockDataReader = MockRepository.GenerateMock<IDataReader>();
            mockDataReader.Stub(d => d["firstname"]).Return("paul");
            mockDataReader.Stub(d => d["lastname"]).Return("nguyen");
            mockDataReader.Stub(d => d["phone"]).Return("0908201560");
            mockDataReader.Stub(d => d["mobilephone"]).Return("0908201560");
            mockDataReader.Stub(d => d["fax"]).Return("fax_test");
            mockDataReader.Stub(d => d.Read()).Return(true).Repeat.Times(1);

            // Act
            var customerInformation = KenoSettingRepository.ReadAgentInformationData(mockDataReader);

            // Assert
            Assert.NotNull(customerInformation);
            Assert.AreEqual("paul", customerInformation.FirstName);
            Assert.AreEqual("nguyen", customerInformation.LastName);
            Assert.AreEqual("0908201560", customerInformation.Phone);
            Assert.AreEqual("0908201560", customerInformation.MobilePhone);
            Assert.AreEqual("fax_test", customerInformation.Fax);
        }

        [Test]
        public void ReadBetLimitData_ShouldMappingCorrectly()
        {
            // Arrange
            var mockDataReader = MockRepository.GenerateMock<IDataReader>();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(1).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(2).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(3).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(4).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(5).Repeat.Twice();
            mockDataReader.Stub(d => d["MinBet"]).Return(10);
            mockDataReader.Stub(d => d["MaxBet"]).Return(100);
            mockDataReader.Stub(d => d["MaxPerGameType"]).Return(1000);
            mockDataReader.Stub(d => d["IsDisabled"]).Return(false);
            mockDataReader.Stub(d => d["DisableDownlinesKeno"]).Return(true);
            mockDataReader.Stub(d => d["MaxMinBet"]).Return(0);
            mockDataReader.Stub(d => d["MinMaxBet"]).Return(120);
            mockDataReader.Stub(d => d["MinMaxPerMatch"]).Return(1500);
            mockDataReader.Stub(d => d.Read()).Return(true).Repeat.Times(5);

            // Act
            var betLimits = KenoSettingRepository.ReadBetLimitData(mockDataReader);

            // Assert
            Assert.NotNull(betLimits);
            Assert.AreEqual(5, betLimits.Count);
            Assert.AreEqual(1, betLimits[0].BetTypeId);
            Assert.AreEqual(10, betLimits[0].MinBet);
            Assert.AreEqual(100, betLimits[0].MaxBet);
            Assert.AreEqual(1000, betLimits[0].MaxPerMatch);
            Assert.AreEqual(true, betLimits[0].IsEnabled);
            Assert.AreEqual(false, betLimits[0].IsDownlinesEnabled);
            Assert.AreEqual(0, betLimits[0].Upline.MinBet);
            Assert.AreEqual(120, betLimits[0].Upline.MaxBet);
            Assert.AreEqual(1500, betLimits[0].Upline.MaxPerMatch);

            Assert.AreEqual(2, betLimits[1].BetTypeId);
            Assert.AreEqual(3, betLimits[2].BetTypeId);
            Assert.AreEqual(4, betLimits[3].BetTypeId);
            Assert.AreEqual(5, betLimits[4].BetTypeId);
        }

        [Test]
        public void ReadSuperPositionTakingData_ShouldMappingCorrectly()
        {
            // Arrange
            var mockDataReader = MockRepository.GenerateMock<IDataReader>();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(1).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(2).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(3).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(4).Repeat.Twice();
            mockDataReader.Stub(d => d["IBCBetTypeID"]).Return(5).Repeat.Twice();
            mockDataReader.Stub(d => d["minpositiontaking"]).Return(10);
            mockDataReader.Stub(d => d["spositiontaking"]).Return(100);
            mockDataReader.Stub(d => d["sdiscount"]).Return(10);
            mockDataReader.Stub(d => d["smaxdiscount"]).Return(20);
            mockDataReader.Stub(d => d.Read()).Return(true).Repeat.Times(5);

            // Act
            var betLimits = KenoSettingRepository.ReadSuperPositionTakingData(mockDataReader);

            // Assert
            Assert.NotNull(betLimits);
            Assert.AreEqual(5, betLimits.Count);
            Assert.AreEqual(1, betLimits[0].BetTypeId);
            Assert.AreEqual(10, betLimits[0].N4);
            Assert.AreEqual(0.85, betLimits[0].L4);
            Assert.AreEqual(100, betLimits[0].S4);
            Assert.AreEqual(10, betLimits[0].SComm);
            Assert.AreEqual(20, betLimits[0].SCommMax);

            Assert.AreEqual(2, betLimits[1].BetTypeId);
            Assert.AreEqual(3, betLimits[2].BetTypeId);
            Assert.AreEqual(4, betLimits[3].BetTypeId);
            Assert.AreEqual(5, betLimits[4].BetTypeId);
        }
    }
}
