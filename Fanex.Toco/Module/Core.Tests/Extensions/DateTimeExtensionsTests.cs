// --------------------------------------------------------------------------
// <copyright file="HandleActionErrorAttributeTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Tests.Extensions
{
    using System;
    using Fanex.CustomerSetting.Module.Core.Extensions;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void FirstDayOfMonth_ShouldReturnExactValue()
        {
            // Arrange
            var certainDay = new DateTime(2014, 2, 25);

            // Act
            var firstDay = certainDay.FirstDayOfMonth();

            // Assert
            Assert.AreEqual(2014, firstDay.Year);
            Assert.AreEqual(2, firstDay.Month);
            Assert.AreEqual(1, firstDay.Day);
        }

        [Test]
        public void LastDayOfMonth_FirstMonth_ShouldReturnExactValue()
        {
            // Arrange
            var firstMonth = new DateTime(2014, 1, 15);

            // Act
            var lastDayOfFirstMonth = firstMonth.LastDayOfMonth();

            // Assert
            Assert.AreEqual(2014, lastDayOfFirstMonth.Year);
            Assert.AreEqual(1, lastDayOfFirstMonth.Month);
            Assert.AreEqual(31, lastDayOfFirstMonth.Day);
        }

        [Test]
        public void LastDayOfMonth_SecondMonth_ShouldReturnExactValue()
        {
            // Arrange
            var secondMonth = new DateTime(2014, 2, 25);

            // Act
            var lastDayOfSecondMonth = secondMonth.LastDayOfMonth();

            // Assert
            Assert.AreEqual(2014, lastDayOfSecondMonth.Year);
            Assert.AreEqual(2, lastDayOfSecondMonth.Month);
            Assert.AreEqual(28, lastDayOfSecondMonth.Day);
        }

        [Test]
        public void LastDayOfMonth_FourthMonth_ShouldReturnExactValue()
        {
            // Arrange
            var fourthMonth = new DateTime(2014, 4, 12);

            // Act
            var lastDayOfFourthMonth = fourthMonth.LastDayOfMonth();

            // Assert
            Assert.AreEqual(2014, lastDayOfFourthMonth.Year);
            Assert.AreEqual(4, lastDayOfFourthMonth.Month);
            Assert.AreEqual(30, lastDayOfFourthMonth.Day);
        }

        [Test]
        public void ToShortFormatedDate_ObjectHasNoValue_ShouldReturnEmpty()
        {
            // Arrange
            var datetime = new Nullable<DateTime>();

            // Act
            var result = datetime.ToShortFormatedDate();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ToShortFormatedDate_ObjectIsValid_ShouldReturnString()
        {
            // Arrange
            var datetime = new DateTime(2014, 1, 1);

            // Act
            var result = datetime.ToShortFormatedDate();

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void ToShortFormatedDate_IntegrationTestWithObjectIsValid_ShouldReturnValidString()
        {
            // Arrange
            var datetime = new DateTime(2014, 1, 10);

            // Act
            var result = datetime.ToShortFormatedDate();

            // Assert
            Assert.AreEqual("Jan 10, 2014", result);
        }
    }
}
