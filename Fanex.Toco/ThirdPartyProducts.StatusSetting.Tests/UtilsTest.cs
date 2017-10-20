using ThirdPartyProducts.StatusSetting;
using NUnit.Framework;
using System;
using System.Threading;
using System.Globalization;

namespace ThirdPartyProducts.StatusSetting.Test
{
    [TestFixture]
    [Category("Utils.cs")]
    public class UtilsTest
    {
        static object[] ToInt32TestCaseInputs = {
            new object[] {"255", 255 },
            new object[] {(decimal)255, 255 },
            new object[] {(float)255, 255 },
            new object[] {(uint)255, 255 },
            new object[] {(ulong)255, 255 },
            new object[] {(long)255, 255 },
            new object[] {(double)255, 255 },
            new object[] {255, 255 },
            new object[] {null, 0 },
            new object[] {DBNull.Value, 0 },
            new object[] {string.Empty, 0 },
            new object[] {"255.00", 0 },
            new object[] {"255.01", 0 },
            new object[] {"2550.00", 0 },
            new object[] {"2,550.01", 0 }
        };

        [Test, TestCaseSource("ToInt32TestCaseInputs")]
        public void ToInt32_AnyArguments_ReturnValue(object value, int expectedValue)
        {
            Assert.AreEqual(expectedValue, Utils.ToInt32(value));
        }

        static object[] ToBooleanTestCaseInputs = {
            new object[] {"true", true },
            new object[] {"True", true },
            new object[] {"TRUE", true },
            new object[] {"1", true },
            new object[] {1, true },
            new object[] {"false", false },
            new object[] {"False", false },
            new object[] {"FALSE", false },
            new object[] {"0", false },
            new object[] {"00", false },
            new object[] {0, false },
            new object[] {null, false },
            new object[] {DBNull.Value, false },
            new object[] {string.Empty, false },
            new object[] { "-1", false },
            new object[] { "01", false },
            new object[] { "-01", false },
            new object[] {-1, false },
            new object[] {2, false },
            new object[] {-2, false }
        };

        [Test, TestCaseSource("ToBooleanTestCaseInputs")]
        public void ToBoolean_AnyArguments_ReturnValue(object value, bool expectedValue)
        {
            Assert.AreEqual(expectedValue, Utils.ToBoolean(value));
        }

        [Test]
        [TestCase("ja-JP", "J")]
        [TestCase("zh-TW", "CT")]
        [TestCase("zh-CN", "CS")]
        [TestCase("th-TH", "TH")]
        [TestCase("", "E")]
        [TestCase("Some Text", "E")]
        public void GetLanguageKeyForSql_AnyArgument_ReturnValue(string value, string expectedValue)
        {
            // Setup
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
            }
            catch (Exception)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("");
            }
            
            Assert.AreEqual(expectedValue, Utils.GetLanguageKeyForSql());
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }

        [Test]
        [TestCase("zh-TW", "ch")]
        [TestCase("zh-CN", "cs")]
        [TestCase("ja-JP", "jp")]
        [TestCase("ko-KR", "ko")]
        [TestCase("vi-VN", "vn")]
        [TestCase("th-TH", "th")]
        [TestCase("", "en")]
        [TestCase("Some Text", "en")]
        public void GetLanguageKeyForRemotingRef_AnyArgument_ReturnValue(string value, string expectedValue)
        {

            // Setup
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
            }
            catch (Exception)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("");
            }

            Assert.AreEqual(expectedValue, Utils.GetLanguageKeyForRemotingRef());
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }
    }
}

