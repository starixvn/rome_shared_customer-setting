using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement")]
    public class RacingStatusSettingServiceTest
    {
        [Test]
        [TestCase("0", "0")]
        [TestCase("100", "100")]
        [TestCase("0", "-109")]
        [TestCase("0", "-110")]
        [TestCase("0", "-113")]
        [TestCase("0", "-114")]
        [TestCase("0", "-115")]
        [TestCase("0", "-116")]
        public void Update_AnyArgument_ReturnSuccess(string expectedErrCode, string errCode)
        {
            // Arrange
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest(errCode, string.Empty);
            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = expectedErrCode, ErrorMessage = string.Empty });

            // Actual
            var actualValue = new RacingStatusSettingService().Update(
                1,
                1,
                1,
                1,
                1,
                1,
                "127.0.0.1",
                new List<ProductStatus>(){
                    new ProductStatus(),
                    new ProductStatus()
                });
            ObjectDbFactory.ExecuteNonQueryFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        private Hashtable ExecuteNonQueryTest(string errCode, string errMsg)
        {
            return new Hashtable()
            {
                { "errcode", errCode },
                { "errmsg", errMsg }
            };
        }
    }
}
