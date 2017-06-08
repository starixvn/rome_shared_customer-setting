using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    [TestFixture]
    public class SelectProductStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustomerSelStatusByProductId";

            // Actual
            var actualValue = new SelectProductStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_ReturnTrue()
        {
            // Arrange
            var expectedValue = true;

            // Actual
            var actualValue = new SelectProductStatusCriteria().IsValid();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
