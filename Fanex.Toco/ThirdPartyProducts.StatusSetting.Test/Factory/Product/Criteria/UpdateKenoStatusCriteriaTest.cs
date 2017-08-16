using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria")]
    public class UpdateKenoStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustomerUpdDisableKeno";

            // Actual
            var actualValue = new UpdateKenoStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_ReturnTrue()
        {
            // Arrange
            var expectedValue = true;

            // Actual
            var actualValue = new UpdateKenoStatusCriteria().IsValid();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
