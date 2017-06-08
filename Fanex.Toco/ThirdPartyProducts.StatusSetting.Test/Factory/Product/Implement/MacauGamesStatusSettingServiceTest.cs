using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement")]
    public class MacauGameStatusSettingServiceTest
    {
        [Test]
        public void Update_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest();
            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "60035", ErrorMessage = "You are not allowed to disable downline's product." });
            // Actual
            var actualValue = new MacauGamesStatusSettingService().Update(
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
                { "errcode", "102" },
                { "errmsg", "<Root MsgID=\"60035\" Msg=\"You are not allowed to disable for downlines.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>" }
            };
        }
    }
}
