using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Test.TestData;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Services")]
    public class GoldDeluxeStatusSettingServiceTest
    {
        private GoldDeluxeStatusSettingServiceMock _settingService;
        private IEnumerable<ProductStatus> _dataTest;

        [SetUp]
        public void Init()
        {
            _settingService = new GoldDeluxeStatusSettingServiceMock();

            _dataTest = CreateDataTest();
            ObjectDbFactory.ExecuteReaderFunc = (criteria, MapSpData) => _dataTest;
        }

        [Test]
        public void Select_Always_ReturnsCorrectData()
        {
            // Actual
            var actualValue = _settingService.Select(0, 0, 0);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(_dataTest), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void Update_Always_ReturnCorrectData()
        {
            // Arrange
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExcuteSPResult.Success;

            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty });

            var productStatus = new List<ProductStatus>
            {
                new ProductStatus
                {
                    ProductId = 12
                }
            };

            // Actual
            var actualValue = new GoldDeluxeStatusSettingServiceMock().Update(1, 1, 1, 1, 1, 1, "127.0.0.1", productStatus);
            ObjectDbFactory.ExecuteNonQueryFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void MapSpData_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            var substitute = Substitute.For<IDataRecord>();

            substitute["ProductID"].Returns(1);
            substitute["Sporttype"].Returns(1);
            substitute["Bettype"].Returns(1);

            substitute["IsDisabled"].Returns(1);
            substitute["IsSyncedDownline"].Returns(1);
            substitute["HasDownline"].Returns(1);

            substitute["IsDisabledUpline"].Returns(1);
            substitute["IsSyncedUpline"].Returns(1);
            substitute["IsDisabledDownline"].Returns(1);
            substitute["IsSync"].Returns(1);

            var expectedValue = new ProductStatus()
            {
                ProductId = 1,
                SportType = 1,
                BetType = 1,

                IsDisabled = true,
                IsSyncedDownline = true,
                HasDownline = true,
                IsDisabledUpline = true,
                IsSyncedUpline = true,
                IsDisabledDownline = true,
                IsSync = true,

                ProductName = string.Empty,
                BetTypeName = string.Empty,
                SportName = string.Empty
            };

            // Actual
            var actualValue = _settingService.MapSpDataMock(substitute);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        private IEnumerable<ProductStatus> CreateDataTest()
        {
            var result = new List<ProductStatus>()
            {
                new ProductStatus()
                {
                    BetType =1700,
                    SportType =255,
                    ProductId =12
                },
            };

            return result;
        }
    }
}
