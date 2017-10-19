using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS.NG2
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS.NG2")]
    public class UpdateNG2ProductStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustSetting_DisableGameStatusUpd";

            // Actual
            var actualValue = new UpdateNG2ProductStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
