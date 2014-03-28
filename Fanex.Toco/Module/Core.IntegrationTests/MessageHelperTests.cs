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
        public void GetResourceFromErrorCode_CertainValue_ShouldReturnExactValue()
        {
            // Act
            var value = MessageHelper.GetResourceFromErrorCode("1000");

            // Assert
            Assert.AreEqual("Data isn't changed.", value);
        }
    }
}