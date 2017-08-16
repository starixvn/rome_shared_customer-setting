using Newtonsoft.Json;
using NSubstitute;
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
    public class KenoStatusSettingServiceTest
    {
        static object[] RefineErrorMessageTestCaseInput = new object[]{
            new object[] {
            new ProductStatus () { BetType = 12 },
            new ProductStatusResult() { ErrorCode = "1", ErrorMessage = "Test" },
            new ProductStatusResult() { ErrorCode = "1", ErrorMessage = ": Test" }
            },
            new object[] {
            new ProductStatus () { BetType = 12 },
            new ProductStatusResult() { ErrorCode = "0", ErrorMessage = "Test" },
            new ProductStatusResult() { ErrorCode = "0", ErrorMessage = "Test" }
            }
        };

        [Test]
        [TestCase(0, false)]
        [TestCase(1502, true)]
        [TestCase(1506, true)]
        [TestCase(1507, true)]
        [TestCase(1509, true)]
        [TestCase(1500, false)]
        [TestCase(1501, false)]
        [TestCase(1503, false)]
        [TestCase(1504, false)]
        public void IsHiddenBettype_AnyArgument_ReturnCorrectData(int value, bool expectedValue)
        {
            Assert.AreEqual(expectedValue, KenoStatusSettingService.IsHiddenBettype(value));
        }

        [Test, TestCaseSource("RefineErrorMessageTestCaseInput")]
        public void RefineErrorMessage_AnyArgument_ReturnCorrectData(ProductStatus input, ProductStatusResult output, ProductStatusResult expectedOutput)
        {
            // Actual
            var actualOutput = KenoStatusSettingService.RefineErrorMessage(input, output);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(actualOutput));
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
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty });

            // Actual
            var actualValue = new KenoStatusSettingService().Update(request);
            ObjectDbFactory.ExecuteNonQueryFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void Select_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            ObjectDbFactory.ExecuteReaderFunc = (criteria, MapSpData) => CreateDataTest();
            var expectedValue = new List<ProductStatus>() {
                new ProductStatus() { BetType=1500 },
                new ProductStatus() { BetType=1501 },
                new ProductStatus() { BetType=1503 },
                new ProductStatus() { BetType=1504 },
                new ProductStatus() { BetType=1505 },
                new ProductStatus() { BetType=1508 },
                new ProductStatus() { BetType=1510 }
            };

            // Actual
            var actualValue = new KenoStatusSettingService().Select(new GetProductStatusRequest());
            ObjectDbFactory.ExecuteReaderFunc = null;

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

        private IEnumerable<ProductStatus> CreateDataTest()
        {
            return new List<ProductStatus>() {
                new ProductStatus() { BetType=1500 },
                new ProductStatus() { BetType=1501 },
                new ProductStatus() { BetType=1502 },
                new ProductStatus() { BetType=1503 },
                new ProductStatus() { BetType=1504 },
                new ProductStatus() { BetType=1505 },
                new ProductStatus() { BetType=1506 },
                new ProductStatus() { BetType=1507 },
                new ProductStatus() { BetType=1508 },
                new ProductStatus() { BetType=1509 },
                new ProductStatus() { BetType=1510 }
            };
        }
    }
}
