// -----------------------------------------------------------------------
// <copyright file="UpdateSuperPositionTakingCriteriaTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Tests.Criteria
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Criteria;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UpdateSuperPositionTakingCriteriaTests
    {
        [Test]
        public void GetSettingKey_AlwaysReturnExactSpKeyName()
        {
            // Arrange
            var criteria = new UpdateSuperPositionTakingCriteria();

            // Act
            var keyName = criteria.GetSettingKey();

            // Assert
            Assert.That(keyName, Is.EqualTo("Acc_CustomerUpdSuperKeno"));
        }

        [Test]
        public void IsValid_AllParamsAreValid_ShouldReturnTrue()
        {
            // Arrange
            var criteria = new UpdateSuperPositionTakingCriteria() { CustId = 10, BetType = 5, SportType = 1 };

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.EqualTo(true));
            Assert.That(criteria.CustId, Is.EqualTo(10));
            Assert.That(criteria.BetType, Is.EqualTo(5));
            Assert.That(criteria.SportType, Is.EqualTo(1));
        }

        [Test]
        public void IsValid_OneParamIsNotValid_ShouldReturnFalse()
        {
            // Arrange
            var criteria = new UpdateSuperPositionTakingCriteria() { CustId = 10, BetType = -15 };

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(criteria.CustId, Is.EqualTo(10));
            Assert.That(criteria.BetType, Is.EqualTo(-15));
            Assert.That(criteria.SportType, Is.EqualTo(0));
        }
    }
}
