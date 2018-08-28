using Fanex.NAP.Framework.Data;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product")]
    public class ProductStatusSettingServiceBaseTest
    {
        static object[] ExecuteNonQueryTestCaseInputs = {
            new object[] {
                new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty },
                new ProductStatusResult() { ErrorCode = "0", ErrorMessage = string.Empty }
            },
            new object[] {
                new ProductStatusResult() { ErrorCode = "1", ErrorMessage = "Update Unsuccessfully." },
                new ProductStatusResult() { ErrorCode = "1", ErrorMessage = "Update Unsuccessfully." }
            }
        };

        [Test]
        public void Select_AnyArgument_ReturnValue()
        {
            // Arrange
            ObjectDbFactory.ExecuteReaderFunc = (criteria, mapSpData) => CreateDataTest();
            var expectedValue = new List<ProductStatus>()
            {
                new ProductStatus() {
                    ProductId = 1
                },
                new ProductStatus() {
                    ProductId = 2
                }
            };

            // Act
            var actualValue = new ProductStatusSettingServiceBaseMock().Select(new GetProductStatusRequest());

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        [Test, TestCaseSource("ExecuteNonQueryTestCaseInputs")]
        public void ExecuteNonQuery_AnyArgument_ReturnValue(ProductStatusResult expected, ProductStatusResult testcase)
        {
            // Arrange
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest(testcase.ErrorCode, testcase.ErrorMessage);

            // Act
            var actualValue = new ProductStatusSettingServiceBaseMock().ExecuteNonQuery(Arg.Any<CriteriaBase>());

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void Validate_CustIdIsLessThanOrEqualZero_ThrowArgumentOutOfRangeException(int custId)
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = custId,
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
            var expectedException = new ArgumentOutOfRangeException("updateProductStatusRequest", custId, string.Format(Message.ArgumentMustGreaterThanZero, "CustId"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase(1)]
        [TestCase(99999)]
        public void Validate_CustIdIsGreaterThanZero_NotThrowException(int custId)
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = custId,
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
            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void Validate_ProductIdIsLessThanOrEqualZero_ThrowArgumentOutOfRangeException(int productId)
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = productId,
                SystemId = 1,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };
            var expectedException = new ArgumentOutOfRangeException("updateProductStatusRequest", productId, string.Format(Message.ArgumentMustGreaterThanZero, "ProductId"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase(1)]
        [TestCase(99999)]
        public void Validate_ProductIdIsGreaterThanZero_NotThrowException(int productId)
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = productId,
                SystemId = 1,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        public void Validate_SystemIdNotInRange_ThrowArgumentOutOfRangeException(int systemId)
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = 1,
                SystemId = systemId,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };
            var expectedException = new ArgumentOutOfRangeException("updateProductStatusRequest", systemId, string.Format(Message.ArgumentSystemIdMustBeInRange, "SystemId"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void Validate_SystemIdInRange_NotThrowException(int systemId)
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = 1,
                SystemId = systemId,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Validate_IPIsNullOrEmpty_ThrowArgumentNullException(string ip)
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
                Ip = ip,
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };

            // Arrange
            var expectedException = new ArgumentNullException("updateProductStatusRequest", string.Format(Message.ArgumentCannotBeNullOrEmpty, "Ip"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentNullException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        [TestCase("abc")]
        [TestCase("127.0.0.1")]
        public void Validate_IPIsNotEmpty_NotThrowException(string ip)
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
                Ip = ip,
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus(),
                    new ProductStatus()
                }
            };

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        public void Validate_ProductStatusIsNullOrEmpty_ThrowArgumentNullException()
        {
            // Arrange
            var requestWithNullProductStatus = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = 1,
                SystemId = 1,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = null
            };

            var requestWithEmptyProductStatus = new UpdateProductStatusRequest()
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

            // Arrange
            var expectedException = new ArgumentNullException("updateProductStatusRequest", string.Format(Message.ArgumentCannotBeNullOrEmpty, "ProductStatus"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentNullException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(requestWithNullProductStatus); });

            Assert.Throws(Is.TypeOf<ArgumentNullException>()
               .And.Message.EqualTo(expectedException.Message),
               delegate { new ProductStatusSettingServiceBaseMock().Validate(requestWithEmptyProductStatus); });
        }

        [Test]
        public void Validate_ProductStatusIsNotEmpty_NotThrowException()
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

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(request); });
        }

        [Test]
        public void MapSpData_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            var substitute = Substitute.For<IDataRecord>();
            substitute["ProductID"].Returns(1);
            substitute["Bettype"].Returns(1);
            substitute["Sporttype"].Returns(1);
            substitute["IsDisabled"].Returns(1);
            substitute["IsDisabledUpline"].Returns(1);
            substitute["IsDisabledDownline"].Returns(1);
            substitute["HasDownline"].Returns(1);
            substitute["IsSync"].Returns(1);
            substitute["IsSyncedUpline"].Returns(1);
            substitute["IsHidden"].Returns(1);
            substitute["IsHiddenUpline"].Returns(1);
            Fanex.RemotingRef.RefDataTypeContainer
                .ConnectTo(new Uri("http://ha.nexdev.net:7258/main/"))
                .FromApplication("ThirdPartyProducts.StatusSetting.Tests")
                .SupportOnlyDefaultLanguage();
            var language = Utils.GetLanguageKeyForRemotingRef();
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
                IsHidden = true,
                IsHiddenUpline = true,
                ProductName = Utils.GetProductName("1", language),
                BetTypeName = Utils.GetBetTypeName("1", language),
                SportName = Utils.GetSportName("1", language)
            };

            // Actual
            var actualValue = new ProductStatusSettingServiceBaseMock().MapSpData(substitute);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        private IEnumerable<ProductStatus> CreateDataTest()
        {
            return new List<ProductStatus>()
            {
                new ProductStatus() {
                    ProductId = 1
                },
                new ProductStatus() {
                    ProductId = 2
                }
            };
        }

        private Hashtable ExecuteNonQueryTest(string errCode, string errMsg)
        {
            return new Hashtable() {
                { "errcode", errCode },
                { "errmsg", errMsg }
            };
        }
    }
}
