using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    [TestFixture]
    public class UpdateVirtualSportStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustomerUpdDisableVS";

            // Actual
            var actualValue = new UpdateVirtualSportStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_ReturnTrue()
        {
            // Arrange
            var expectedValue = true;

            // Actual
            var actualValue = new UpdateVirtualSportStatusCriteria().IsValid();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
