// -----------------------------------------------------------------------
// <copyright file="UpdateSuperBetSettingCriteriaTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.Tests.Criteria
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UpdateSuperBetSettingCriteriaTests
    {
        [Test]
        public void GetSettingKey_AlwaysReturnExactSpKeyName()
        {
            // Arrange
            var criteria = new UpdateSuperBetSettingCriteria();

            // Act
            var keyName = criteria.GetSettingKey();

            // Assert
            Assert.That(keyName, Is.EqualTo("Acc_CustomerUpdSuperKeno_BetSetting"));
        }

        [Test]
        public void IsValid_CustIdAndBetTypeIdIsValid_ShouldReturnTrue()
        {
            // Arrange
            var criteria = new UpdateSuperBetSettingCriteria() { CustId = 10, BetType = 5, SportType = 202 };

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.EqualTo(true));
            Assert.That(criteria.CustId, Is.EqualTo(10));
            Assert.That(criteria.BetType, Is.EqualTo(5));
        }

        [Test]
        public void IsValid_CustIdEqualsZero_ShouldReturnFalse()
        {
            // Arrange
            var criteria = new UpdateSuperBetSettingCriteria() { CustId = 0, BetType = 15 };

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.EqualTo(false));
            Assert.That(criteria.CustId, Is.EqualTo(0));
            Assert.That(criteria.BetType, Is.EqualTo(15));
        }
    }
}
