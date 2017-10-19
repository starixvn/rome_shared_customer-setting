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
        private IProductStatusSettingService _substitute;

        [SetUp]
        public void Init()
        {
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

            _substitute.Select(Arg.Any<GetProductStatusRequest>()).Returns(expectedValue);
            
            // Act
            var actualValue = StatusSettingService.GetProductStatus(new GetProductStatusRequest() {
                CustId=1,
                ProductId =1,
                RoleId = 1
            });

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void UpdateProductStatus_AnyArgument_ReturnValue()
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
                ProductStatus = new List<ProductStatus>()
            };

            var expectedValue = new List<ProductStatusResult>()
            {
                new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty }
            };

            _substitute.Update(
                Arg.Any<UpdateProductStatusRequest>()).Returns(expectedValue);

            // Act
            var actualValue = StatusSettingService.UpdateProductStatus(request);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}