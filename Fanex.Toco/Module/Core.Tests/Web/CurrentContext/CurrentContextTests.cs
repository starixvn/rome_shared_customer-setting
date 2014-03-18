// --------------------------------------------------------------------------
// <copyright file="CurrentContextTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/18">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Tests.Web.CurrentContext
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Reflection;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.CustomerSetting.Module.Core.Web;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class CurrentContextTests
    {
        private CurrentContext _currentContext = null;

        [SetUp]
        public void SetUp()
        {
            _currentContext = new CurrentContext(FakeHttpContext(), FakeServerVariables());
        }

        [TearDown]
        public void TearDown()
        {
            _currentContext = null;
        }

        [Test]
        public void GetPort_ShouldReturnExactPort()
        {
            // Act
            string port = _currentContext.GetPort();

            // Assert
            Assert.AreEqual("10.10.10.10", port);
        }

        [Test]
        public void GetDomain_ShouldReturnExactDomain()
        {
            // Act
            string domain = _currentContext.GetDomain();

            // Assert
            Assert.AreEqual("localhost", domain);
        }

        [Test]
        public void GetProtocol_ShouldReturnExactProtocol()
        {
            // Act
            string protocol = _currentContext.GetProtocol();

            // Assert
            Assert.AreEqual("http://", protocol);
        }

        /// <summary>
        /// Fakes the HTTP context. 
        /// </summary>
        /// <returns>The fake of HttpContext.</returns>
        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }

        /// <summary>
        /// Fakes the server variables.
        /// </summary>
        /// <returns>The server variables. </returns>
        public static NameValueCollection FakeServerVariables()
        {
            var result = new NameValueCollection();
            result.Add("SERVER_PORT", "10.10.10.10");
            result.Add("SERVER_NAME", "localhost");
            result.Add("SERVER_PORT_SECURE", "0");
            result.Add("HTTP_X_FORWARDED_FOR", "");
            result.Add("REMOTE_ADDR", "");
            return result;
        }
    }
}
