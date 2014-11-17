// --------------------------------------------------------------------------
// <copyright file="HandleActionErrorAttributeTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Tests.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Exceptions;
    using Fanex.CustomerSetting.Module.Core.Extensions;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class HandleActionErrorAttributeTests
    {
        [Test]
        public void HandleExceptionAndCustomizeResponse_ShouldReturenExpectedValues()
        {
            // Arrange
            var response = MockRepository.GenerateStub<HttpResponseBase>();
            var exceptionContext = MockRepository.GenerateMock<ExceptionContext>();
            exceptionContext.Stub(c => c.HttpContext.Response).Return(response);

            // Act
            HandleActionErrorAttribute.HandleExceptionAndCustomizeResponse(exceptionContext);

            // Assert
            Assert.AreEqual(true, exceptionContext.ExceptionHandled);
            Assert.AreEqual(500, exceptionContext.HttpContext.Response.StatusCode);
            Assert.AreEqual(true, exceptionContext.HttpContext.Response.TrySkipIisCustomErrors);
        }

        [Test]
        public void IfIsAjaxRequest_InputParamEqualsXMLHttpRequest_ShouldReturnTrue()
        {
            // Act
            var result = HandleActionErrorAttribute.IfIsAjaxRequest("XMLHttpRequest");

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LogExceptionMessage_ItShouldBeCalled()
        {
            // Arrange
            var logExtensionMock = MockRepository.GenerateMock<ILogExtension>();
            logExtensionMock.Expect(l => l.Log(Arg<Exception>.Is.NotNull));
            var handleActionErrorAttribute = new HandleActionErrorAttribute(logExtensionMock, null);
            var exceptionContext = MockRepository.GenerateMock<ExceptionContext>();
            exceptionContext.Stub(c => c.Exception).Return(new Exception());

            // Act
            handleActionErrorAttribute.LogExceptionMessage(exceptionContext);

            // Assert
            logExtensionMock.VerifyAllExpectations();
        }
    }
}
