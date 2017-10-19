using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Model;


namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement.DCS
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement.DCS")]
    public class DCSProductStatusSettingServiceTest
    {
        static object[] ConvertXmlErrorMessageTestCase = new object[] {
            new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="101",
                    ErrorMessage = "<Root MsgID=\"60033\" Msg=\"Unable to edit downline's Product Status because the account disabled for this product.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>"
                },
                true,
                new ProductStatusResult()
                {
                    ErrorCode="60033",
                    ErrorMessage = "Unable to edit downline's Product Status because the account disabled for this product.\r\n\tBaccarat, Roulette, Sic Bo, Dragon Tiger"
                }
            },
            new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="102",
                    ErrorMessage = "<Root MsgID=\"60035\" Msg=\"You are not allowed to disable downline's product.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>"
                },
                true,
                new ProductStatusResult()
                {
                    ErrorCode="60035",
                    ErrorMessage = "You are not allowed to disable downline's product.\r\n\tBaccarat, Roulette, Sic Bo, Dragon Tiger"
                }
            },
            new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="103",
                    ErrorMessage = "<Root MsgID=\"90012\" Msg=\"You are not allowed to perform this action.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>"
                },
                true,
                new ProductStatusResult()
                {
                    ErrorCode="90012",
                    ErrorMessage = "You are not allowed to perform this action.\r\n\tBaccarat, Roulette, Sic Bo, Dragon Tiger"
                }
            },
             new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="101",
                    ErrorMessage = "<Root MsgID=\"60033\" Msg=\"Unable to edit downline's Product Status because the account disabled for this product.\"></Root>"
                },
                false,
                new ProductStatusResult()
                {
                    ErrorCode="60033",
                    ErrorMessage = "Unable to edit downline's Product Status because the account disabled for this product."
                }
            },
            new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="102",
                    ErrorMessage = "<Root MsgID=\"60035\" Msg=\"You are not allowed to disable downline's product.\"></Root>"
                },
                false,
                new ProductStatusResult()
                {
                    ErrorCode="60035",
                    ErrorMessage = "You are not allowed to disable downline's product."
                }
            },
            new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="103",
                    ErrorMessage = "<Root MsgID=\"90012\" Msg=\"You are not allowed to perform this action.\"></Root>"
                },
                false,
                new ProductStatusResult()
                {
                    ErrorCode="90012",
                    ErrorMessage = "You are not allowed to perform this action."
                }
            },
            new object[] {
                new ProductStatusResult()
                {
                    ErrorCode="999",
                    ErrorMessage = "Any Invalid Text"
                },
                false,
                new ProductStatusResult()
                {
                    ErrorCode="999",
                    ErrorMessage = "Any Invalid Text"
                }
            }
        };


        [Test, TestCaseSource("ConvertXmlErrorMessageTestCase")]
        public void ConvertXmlErrorMessage_AnyArgument_ReturnCorrectValue(ProductStatusResult productStatusResultWithXmlErrorMessage, bool isShowBettypeName, ProductStatusResult expectedValue)
        {
            // Actual
            var actualValue = new DCSProductStatusSettingServiceMock().ConvertXmlErrorMessageMock(productStatusResultWithXmlErrorMessage, isShowBettypeName);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void MapSpData_AnyArgument_MapDataCorrect()
        {
            // Arrange
            var substitute = Substitute.For<IDataRecord>();
            substitute["ProductID"].Returns("1");
            substitute["Bettype"].Returns("1");
            substitute["Sporttype"].Returns("1");
            substitute["IsDisabled"].Returns("true");
            substitute["IsDisabledUpline"].Returns("true");
            substitute["IsDisabledDownline"].Returns("true");
            substitute["HasDownline"].Returns("true");
            substitute["IsSync"].Returns("true");
            substitute["IsSyncedUpline"].Returns("true");
            substitute["IsSyncedDownline"].Returns("true");
            substitute["IsHidden"].Returns("true");
            substitute["IsHiddenUpline"].Returns("true");
            var expectedValue = new ProductStatus()
            {
                ProductId = 1,
                BetType = 1,
                SportType = 1,
                IsDisabled = true,
                IsDisabledUpline = true,
                IsDisabledDownline = true,
                HasDownline = true,
                IsSync = true,
                IsSyncedUpline = true,
                IsSyncedDownline = true,
                IsHidden = true,
                IsHiddenUpline = true,
                SportName = string.Empty,
                BetTypeName = string.Empty,
                ProductName = string.Empty,
            };

            // Actual
            var actualvalue = new DCSProductStatusSettingServiceMock().MapSpDataMock(substitute);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualvalue));
        }

        [Test]
        public void Select_AnyArgument_ReturnCorrectValue()
        {
            // Arrange
            ObjectDbFactory.ExecuteReaderFunc = (criteria, mapSpData) => CreateDataTest();
            var expectedValue = new List<ProductStatus>() {
                new ProductStatus() { ProductId = 1 },
                new ProductStatus() { ProductId = 2 }
            };

            // Actual
            var actualValue = new DCSProductStatusSettingServiceMock().Select(new GetProductStatusRequest());
            ObjectDbFactory.ExecuteReaderFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void Update_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = 1,
                SystemId = 1,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest();
            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty });

            // Actual
            var actualValue = new DCSProductStatusSettingServiceMock().Update(request);
            ObjectDbFactory.ExecuteNonQueryFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        private List<ProductStatus> CreateDataTest()
        {
            return new List<ProductStatus>() {
                new ProductStatus() { ProductId = 1 },
                new ProductStatus() { ProductId = 2 }
            };
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
