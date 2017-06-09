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
            var actualValue = new ProductStatusSettingServiceBaseMock().Select(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());

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
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Arrange
            var expectedException = new ArgumentOutOfRangeException("custId", custId, string.Format(Message.ArgumentMustGreaterThanZero, "CustId"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(custId, 1, 1, 1, 1, "127.0.0.1", listProductStatus); });
        }

        [Test]
        [TestCase(1)]
        [TestCase(99999)]
        public void Validate_CustIdIsGreaterThanZero_NotThrowException(int custId)
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(custId, 1, 1, 1, 1, "127.0.0.1", listProductStatus); });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-999)]
        public void Validate_ProductIdIsLessThanOrEqualZero_ThrowArgumentOutOfRangeException(int productId)
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Arrange
            var expectedException = new ArgumentOutOfRangeException("productId", productId, string.Format(Message.ArgumentMustGreaterThanZero, "ProductId"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, productId, 1, 1, "127.0.0.1", listProductStatus); });
        }

        [Test]
        [TestCase(1)]
        [TestCase(99999)]
        public void Validate_ProductIdIsGreaterThanZero_NotThrowException(int productId)
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, productId, 1, 1, "127.0.0.1", listProductStatus); });
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
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Arrange
            var expectedException = new ArgumentOutOfRangeException("systemId", systemId, string.Format(Message.ArgumentSystemIdMustBeInRange, "SystemId"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, systemId, "127.0.0.1", listProductStatus); });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void Validate_SystemIdInRange_NotThrowException(int systemId)
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, systemId, "127.0.0.1", listProductStatus); });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Validate_IPIsNullOrEmpty_ThrowArgumentNullException(string ip)
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Arrange
            var expectedException = new ArgumentNullException("ip", string.Format(Message.ArgumentCannotBeNullOrEmpty, "ip"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentNullException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, 1, ip, listProductStatus); });
        }

        [Test]
        [TestCase("abc")]
        [TestCase("127.0.0.1")]
        public void Validate_IPIsNotEmpty_NotThrowException(string ip)
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, 1, ip, listProductStatus); });
        }

        [Test]
        public void Validate_ProductStatusIsNullOrEmpty_ThrowArgumentNullException()
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();

            // Arrange
            var expectedException = new ArgumentNullException("productStatus", string.Format(Message.ArgumentCannotBeNullOrEmpty, "productStatus"));

            // Assert
            Assert.Throws(Is.TypeOf<ArgumentNullException>()
                .And.Message.EqualTo(expectedException.Message),
                delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, 1, "127.0.0.1", null); });

            Assert.Throws(Is.TypeOf<ArgumentNullException>()
               .And.Message.EqualTo(expectedException.Message),
               delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, 1, "127.0.0.1", listProductStatus); });
        }

        [Test]
        public void Validate_ProductStatusIsNotEmpty_NotThrowException()
        {
            // Arrange
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus());
            listProductStatus.Add(new ProductStatus());

            // Assert
            Assert.DoesNotThrow(delegate { new ProductStatusSettingServiceBaseMock().Validate(1, 1, 1, 1, 1, "127.0.0.1", listProductStatus); });
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
            substitute["IsSyncedDownline"].Returns(1);
            substitute["IsHidden"].Returns(1);
            substitute["IsHiddenUpline"].Returns(1);
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
                ProductName = string.Empty,
                BetTypeName = string.Empty,
                SportName = string.Empty
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
