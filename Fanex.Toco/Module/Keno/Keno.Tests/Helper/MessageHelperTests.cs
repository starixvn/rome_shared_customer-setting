// -----------------------------------------------------------------------
// <copyright file="MessageHelperTests.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.Tests.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.Helpers;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class MessageHelperTests
    {
        [Test]
        public void NoDataIsChanged_ErrorCodeIsNullOrEmpty_ShouldReturnTrue()
        {
            // Arrange
            var messageHelper = new MessageHelper(string.Empty, string.Empty);

            // Act
            var isNoDataChanged = messageHelper.DataIsNotChangedOrNotUpdated();

            // Assert
            Assert.AreEqual(true, isNoDataChanged);
        }

        [Test]
        public void AddErrorMessage_ErrorCodeDoNotEqualZero_ShouldAddError()
        {
            // Arrange
            var messageHelper = new MessageHelper(string.Empty, string.Empty);

            // Act
            messageHelper.AddErrorMessage("100", "Error Message for 100");

            // Assert
            Assert.AreEqual(1, messageHelper.ErrorResults.Count());
            Assert.AreEqual(100, messageHelper.ErrorResults[0].ErrorCode);
            Assert.AreEqual("Error Message for 100", messageHelper.ErrorResults[0].ErrorMessage);
        }

        [Test]
        public void AddErrorMessageWhenClientDataIsChanged_AddSomeErrors_ShouldReturnArray()
        {
            // Arrange
            var messageHelper = new MessageHelper("0\a100\a-200", "Msg0\aMsg100\aMsg-200");

            // Act
            messageHelper.AddMessageWhenDataIsUpdated();

            // Assert
            Assert.AreEqual(3, messageHelper.ErrorResults.Count());
            Assert.AreEqual(0, messageHelper.ErrorResults[0].ErrorCode);
            Assert.AreEqual("Msg0", messageHelper.ErrorResults[0].ErrorMessage);
        }

        [Test]
        public void GetErrors_MessageIsNotValid_ShouldAddAnErrorMessage()
        {
            // Arrange
            var messageHelper = new MessageHelper("0\a100", "Msg0\aMsg100\aMsg-200");

            // Act
            messageHelper.GetErrors();

            // Assert
            Assert.AreEqual(1, messageHelper.ErrorResults.Count());
            Assert.AreEqual(1001, messageHelper.ErrorResults[0].ErrorCode);
        }

        [Test]
        public void GetErrors_AddSomeErrors_ShouldReturnArray()
        {
            // Arrange
            var messageHelper = new MessageHelper("0\a100\a-200", "Msg0\aMsg100\aMsg-200");

            // Act
            messageHelper.GetErrors();

            // Assert
            Assert.AreEqual(3, messageHelper.ErrorResults.Count());
            Assert.AreEqual(0, messageHelper.ErrorResults[0].ErrorCode);
            Assert.AreEqual("Msg0", messageHelper.ErrorResults[0].ErrorMessage);
        }

        [Test]
        public void GetErrors_ErrorCodeIsNullOrEmpty_ShouldDontHaveAnyError()
        {
            // Arrange
            var messageHelper = new MessageHelper(string.Empty, string.Empty);

            // Act
            messageHelper.GetErrors();

            // Assert
            Assert.AreEqual(1, messageHelper.ErrorResults.Count());
            Assert.AreEqual(1000, messageHelper.ErrorResults[0].ErrorCode);
        }
    }
}
