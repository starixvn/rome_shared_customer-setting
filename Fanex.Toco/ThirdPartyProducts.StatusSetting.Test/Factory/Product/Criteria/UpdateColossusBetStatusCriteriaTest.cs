using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria")]
    public class UpdateColossusBetStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustomerUpdDisableColossusBet";

            // Actual
            var actualValue = new UpdateColossusBetStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_ReturnTrue()
        {
            // Arrange
            var expectedValue = true;

            // Actual
            var actualValue = new UpdateColossusBetStatusCriteria().IsValid();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
