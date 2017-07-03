using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS")]
    public class SelectDCSProductStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_Credit_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustSetting_StatusSettingSelect";

            // Actual
            var actualValue = new SelectDCSProductStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void GetSettingKey_Deposit_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_Dep_CustSetting_StatusSettingSelect";
            var criteria = new SelectDCSProductStatusCriteria() { IsDeposit = true };

            // Actual
            var actualValue = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
