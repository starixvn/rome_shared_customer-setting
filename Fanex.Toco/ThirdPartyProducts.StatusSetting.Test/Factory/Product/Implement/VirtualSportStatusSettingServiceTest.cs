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
    public class VirtualSportStatusSettingServiceTest
    {
        [Test]
        public void Update_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest();
            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "0", ErrorMessage= string.Empty });
            // Actual
            var actualValue = new VirtualSportStatusSettingService().Update(
                1,
                1,
                1,
                1,
                1,
                1,
                "127.0.0.1",
                new List<ProductStatus>(){
                    new ProductStatus()
                });
            ObjectDbFactory.ExecuteNonQueryFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        private Hashtable ExecuteNonQueryTest()
        {
            return new Hashtable()
            {
                { "errcode", "0" },
                { "errmsg", string.Empty }
            };
        }
    }
}
