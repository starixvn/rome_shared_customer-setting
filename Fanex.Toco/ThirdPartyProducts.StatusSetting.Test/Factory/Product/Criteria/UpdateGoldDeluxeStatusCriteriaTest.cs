using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    public class UpdateGoldDeluxeStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustomerUpdDisableGoldDeluxe";
            var criteria = new UpdateGoldDeluxeStatusCriteria();

            // Actual
            var actualValue = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_Always_ReturnTrue()
        {
            // Arrange
            var criteria = new UpdateGoldDeluxeStatusCriteria();

            // Actual
            var actualValue = criteria.IsValid();

            // Assert
            Assert.True(actualValue);
        }
    }
}
