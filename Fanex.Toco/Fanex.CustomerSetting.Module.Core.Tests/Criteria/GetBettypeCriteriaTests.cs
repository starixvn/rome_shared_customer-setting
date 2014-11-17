// --------------------------------------------------------------------------
// <copyright file="GetBettypeCriteriaTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Tests.Criteria
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Core.Criteria;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class GetBettypeCriteriaTests
    {
        [Test]
        public void GetSettingKey_AlwaysReturnExactSpKeyName()
        {
            // Arrange
            var criteria = new GetBettypeCriteria();

            // Act
            var keyName = criteria.GetSettingKey();

            // Assert
            Assert.That(keyName, Is.EqualTo("Core_SelBettype"));
        }

        [Test]
        public void IsValid_AlwaysReturnTrue()
        {
            // Arrange
            var criteria = new GetBettypeCriteria();

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.That(isValid, Is.EqualTo(true));
        }
    }
}
