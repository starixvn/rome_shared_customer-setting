using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement")]
    public class NG2StatusSettingServiceTest
    {
        [Test]
        public void MapSpData_AnyArgument_MapDataCorrect()
        {
            // Arrange
            var substitute = Substitute.For<IDataRecord>();
            substitute["Bettype"].Returns("1");
            substitute["Sporttype"].Returns("1");
            substitute["IsDisabled"].Returns("true");
            substitute["IsDisabledUpline"].Returns("true");
            substitute["IsDisabledDownline"].Returns("true");
            substitute["HasDownline"].Returns("true");
            substitute["IsSync"].Returns("true");
            substitute["IsHidden"].Returns("true");
            var expectedValue = new ProductStatus() {
                ProductId=(int)Enums.Product.NG2,
                BetType = 1,
                SportType=1,
                IsDisabled=true,
                IsDisabledUpline=true,
                IsDisabledDownline=true,
                HasDownline=true,
                IsSync=true,
                IsHidden=true,
                SportName = string.Empty,
                BetTypeName=string.Empty,
                ProductName= string.Empty,
            };

            // Actual
            var actualvalue = new NG2StatusSettingServiceMock().MapSpDataMock(substitute);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualvalue));
        }

        [Test]
        public void Update_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest();
            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "60035", ErrorMessage = "You are not allowed to disable downline's product." });
            // Actual
            var actualValue = new NG2StatusSettingService().Update(
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
