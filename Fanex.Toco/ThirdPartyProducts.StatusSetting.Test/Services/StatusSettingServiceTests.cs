using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Factory.Product.Interface;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Services.Tests
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Services")]
    public class StatusSettingServiceTests
    {
        private StatusSettingService _statusSettingService;
        private IProductStatusSettingService _substitute;

        [SetUp]
        public void Init()
        {
            _statusSettingService = new StatusSettingService();
            _substitute = Substitute.For<IProductStatusSettingService>();
            ProductFactory.CreateProductServiceFunc = id => _substitute;
        }

        [TearDown]
        public void Dispose()
        {
            ProductFactory.CreateProductServiceFunc = null;
        }

        [Test]
        public void GetProductStatus_AnyArgument_ReturnValue()
        {
            // Arrange
            var expectedValue = new List<ProductStatus>() {
                new ProductStatus() { ProductId=1 }
            };

            _substitute.Select(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(expectedValue);
            
            // Act
            var actualValue = _statusSettingService.GetProductStatus(1, 1, 1);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void UpdateProductStatus_AnyArgument_ReturnValue()
        {
            // Arrange
            var expectedValue = new List<ProductStatusResult>()
            {
                new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty }
            };

            _substitute.Update(
                Arg.Any<int>(), 
                Arg.Any<int>(),
                Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<string>(), 
                Arg.Any<List<ProductStatus>>()).Returns(expectedValue);

            // Act
            var actualValue = _statusSettingService.UpdateProductStatus(1, 1, 1, 1, 1, 1, "127.0.0.1", new List<ProductStatus>());

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}