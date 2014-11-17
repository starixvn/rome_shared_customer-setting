// --------------------------------------------------------------------------
// <copyright file="KenoSettingControllerTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>


namespace Fanex.CustomerSetting.Module.Core.Tests.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Controllers.Base;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class JsonBaseTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteResult_ControllerContextIsNull_ShouldThrowsException()
        {
            // Arrange
            var jsonBase = new JsonBase();

            // Act
            jsonBase.ExecuteResult(null);
        }

        [Test]
        public void ExecuteResult_DontInputContentType_ShouldSetTheDefaultContentTypeOfApplicationJson()
        {
            // Arrange
            var jsonBase = new JsonBase();

            var response = MockRepository.GenerateStub<HttpResponseBase>();
            var controllerContext = MockRepository.GenerateMock<ControllerContext>();
            controllerContext.Stub(c => c.HttpContext.Response).Return(response);

            // Act
            jsonBase.ExecuteResult(controllerContext);

            // Assert
            Assert.AreEqual("application/json", controllerContext.HttpContext.Response.ContentType);
        }

        [Test]
        public void ExecuteResult_CheckForContentTypeAndContentEncoding()
        {
            // Arrange
            var jsonBase = new JsonBase();
            jsonBase.ContentEncoding = Encoding.Unicode;
            jsonBase.ContentType = "application/text";

            var response = MockRepository.GenerateStub<HttpResponseBase>();
            var controllerContext = MockRepository.GenerateMock<ControllerContext>();
            controllerContext.Stub(c => c.HttpContext.Response).Return(response);

            // Act
            jsonBase.ExecuteResult(controllerContext);

            // Assert
            Assert.AreEqual("application/text", controllerContext.HttpContext.Response.ContentType);
            Assert.AreEqual(Encoding.Unicode, controllerContext.HttpContext.Response.ContentEncoding);
        }

        [Test]
        public void ExecuteResult_ResponseBodyHasValue_ShouldWriteExactThisValue()
        {
            // Arrange
            var jsonBase = new JsonBase();
            var responseBody = "Response Body";
            jsonBase.ContentEncoding = Encoding.Unicode;
            jsonBase.ResponseBody = responseBody;

            var sb = new StringBuilder();
            var response = MockRepository.GenerateStub<HttpResponseBase>();
            response.Stub(r => r.Write(null))
                .IgnoreArguments()
                .Do((Action<string>)(input => sb.Append(input)));
            var controllerContext = MockRepository.GenerateMock<ControllerContext>();
            controllerContext.Stub(c => c.HttpContext.Response).Return(response);

            // Act
            jsonBase.ExecuteResult(controllerContext);

            // Assert
            Assert.That(sb.ToString(), Is.StringContaining("Response Body"));
        }
    }
}
