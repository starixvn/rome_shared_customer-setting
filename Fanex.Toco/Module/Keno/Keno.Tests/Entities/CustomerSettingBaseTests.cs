// --------------------------------------------------------------------------
// <copyright file="CustomerSettingBaseTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/11">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Tests.Entities
{
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class CustomerSettingBaseTests
    {
        [Test]
        public void IsCopyToItself_CurrentBetTypeIsSameCopiedBetType_ShouldReturnTrue()
        {
            // Act
            var result = CustomerSettingBase<SuperPositionTaking>.IsCopyToItself(10, 10);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsCopyToItself_CurrentBetTypeIsDifferentCopiedBetType_ShouldReturnFalse()
        {
            // Act
            var result = CustomerSettingBase<SuperPositionTaking>.IsCopyToItself(1, 10);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CopyToOneBetSetting_ShouldMappingCorrectly()
        {
            // Arrange
            var source = new BetSetting()
            {
                MinBet = 100,
                MaxBet = 1000,
                MaxPerMatch = 5000,
                IsEnabled = true,
                IsDownlinesEnabled = true,
                IsBetSettingUpdated = true,
                IsStatusUpdated = true
            };
            var destination = new BetSetting();

            // Act
            CustomerSettingBase<SuperPositionTaking>.CopyToOneBetSetting(source, destination);

            // Assert
            Assert.AreEqual(100, destination.MinBet);
            Assert.AreEqual(1000, destination.MaxBet);
            Assert.AreEqual(5000, destination.MaxPerMatch);
            Assert.AreEqual(true, destination.IsEnabled);
            Assert.AreEqual(true, destination.IsDownlinesEnabled);
            Assert.AreEqual(true, destination.IsBetSettingUpdated);
            Assert.AreEqual(true, destination.IsStatusUpdated);
        }
    }
}
