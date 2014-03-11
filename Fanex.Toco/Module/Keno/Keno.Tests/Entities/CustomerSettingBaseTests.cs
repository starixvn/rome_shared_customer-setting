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
            // Arrange
            var customerSettingBase = new CustomerSettingBase();

            // Act
            var result = customerSettingBase.IsCopyToItself(10, 10);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsCopyToItself_CurrentBetTypeIsDifferentCopiedBetType_ShouldReturnFalse()
        {
            // Arrange
            var customerSettingBase = new CustomerSettingBase();

            // Act
            var result = customerSettingBase.IsCopyToItself(1, 10);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
