using NUnit.Framework;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;
using ThirdPartyProducts.StatusSetting.Model;
using System.Text.RegularExpressions;
using KellermanSoftware.CompareNetObjects;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria.DCS")]
    public class UpdateDCSProductStatusCriteriaTest
    {
        private CompareLogic compareLogic;
        [SetUp]
        public void SetUp()
        {
            compareLogic = new CompareLogic();
        }

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
            var expectedValue = "<ROOT>\r\n  ";
            expectedValue += "<ProductStatus SportType=\"1\" BetType=\"1\" IsDisabled=\"1\" IsDisabledDownline=\"0\" />\r\n  ";
            expectedValue += "<ProductStatus SportType=\"2\" BetType=\"2\" IsDisabled=\"0\" IsDisabledDownline=\"0\" />\r\n  ";
            expectedValue += "<ProductStatus SportType=\"3\" BetType=\"3\" IsDisabled=\"1\" IsDisabledDownline=\"0\" />\r\n  ";
            expectedValue += "<ProductStatus SportType=\"4\" BetType=\"4\" IsDisabled=\"0\" IsDisabledDownline=\"1\" />\r\n";
            expectedValue += "</ROOT>";

            // Actual
            var listProductStatus = new List<ProductStatus>();
            listProductStatus.Add(new ProductStatus() { SportType = 1, BetType = 1, IsDisabled = true, HasDownline = true, IsDisabledDownline = false });
            listProductStatus.Add(new ProductStatus() { SportType = 2, BetType = 2, IsDisabled = false, HasDownline = true, IsDisabledDownline = false });
            listProductStatus.Add(new ProductStatus() { SportType = 3, BetType = 3, IsDisabled = true, HasDownline = true, IsDisabledDownline = false });
            listProductStatus.Add(new ProductStatus() { SportType = 4, BetType = 4, IsDisabled = false, HasDownline = true, IsDisabledDownline = true });
            var actualValue = new UpdateDCSProductStatusCriteria();
            actualValue.MapSpParams(listProductStatus);

            // Assert
            var result = compareLogic.Compare(Regex.Replace(expectedValue, @"\t|\n|\r", ""), Regex.Replace(actualValue.IsDisabled, @"\t|\n|\r", ""));
            Assert.True(result.AreEqual);
        }
    }
}
