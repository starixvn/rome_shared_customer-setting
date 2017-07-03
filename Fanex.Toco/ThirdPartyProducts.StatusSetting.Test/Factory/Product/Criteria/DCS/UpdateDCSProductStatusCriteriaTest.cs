using NUnit.Framework;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS")]
    public class UpdateDCSProductStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_Credit_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustSetting_DisableNewProductStatusUpd";

            // Actual
            var actualValue = new UpdateDCSProductStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void GetSettingKey_Deposit_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_Dep_CustSetting_DisableStatUpd";
            var criteria = new UpdateDCSProductStatusCriteria() { IsDeposit = true };

            // Actual
            var actualValue = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_ReturnTrue()
        {
            // Arrange
            var expectedValue = true;

            // Actual
            var actualValue = new UpdateDCSProductStatusCriteria().IsValid();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void MapSpParams_AnyArgument_MapDataCorrect()
        {
            // Arrange
            var expectedValue = "<ROOT>";
            expectedValue += "<ProductStatus SportType=\"1\" BetType=\"1\" IsDisabled=\"1\" IsDisabledDownline=\"1\" />";
            expectedValue += "<ProductStatus SportType=\"2\" BetType=\"2\" IsDisabled=\"0\" IsDisabledDownline=\"0\" />";
            expectedValue += "<ProductStatus SportType=\"3\" BetType=\"3\" IsDisabled=\"1\" IsDisabledDownline=\"0\" />";
            expectedValue += "<ProductStatus SportType=\"4\" BetType=\"4\" IsDisabled=\"0\" IsDisabledDownline=\"1\" />";
            expectedValue += "</ROOT>";

            // Actual
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus() { SportType = 1, BetType = 1, IsDisabled = true, IsDisabledDownline = true });
            listProductStatus.Add(new ProductStatus() { SportType = 2, BetType = 2, IsDisabled = false, IsDisabledDownline = false });
            listProductStatus.Add(new ProductStatus() { SportType = 3, BetType = 3, IsDisabled = true, IsDisabledDownline = false });
            listProductStatus.Add(new ProductStatus() { SportType = 4, BetType = 4, IsDisabled = false, IsDisabledDownline = true });

            var actualValue = new UpdateDCSProductStatusCriteria();
            actualValue.MapSpParams(listProductStatus);

            // Assert
            Assert.AreEqual(expectedValue, actualValue.IsDisabled);
        }
    }
}
