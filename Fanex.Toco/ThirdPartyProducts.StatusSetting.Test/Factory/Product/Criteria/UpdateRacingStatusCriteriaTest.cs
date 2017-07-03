using Newtonsoft.Json;
using NUnit.Framework;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria")]
    public class UpdateRacingStatusCriteriaTest
    {
        [Test]
        public void GetSettingKey_ReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "Acc_CustomerUpdRBStatus";

            // Actual
            var actualValue = new UpdateRacingStatusCriteria().GetSettingKey();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void IsValid_ReturnTrue()
        {
            // Arrange
            var expectedValue = true;

            // Actual
            var actualValue = new UpdateRacingStatusCriteria().IsValid();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        static object[] MapProductStatusToSpParamTestCase = new object[] {
            new object[] {
                new ProductStatus() {SportType = 151, BetType = 31, IsDisabled = true, IsDisabledDownline = true },
                new UpdateRacingStatusCriteriaMock() { Criteria = new UpdateRacingStatusCriteria() { Disable151_31 = true, DisableDownlines151_31 = true, UpdateDisable151_31 = true} }
            },
            new object[] {
                new ProductStatus() {SportType = 151, BetType = 41, IsDisabled = true, IsDisabledDownline = true },
                new UpdateRacingStatusCriteriaMock() { Criteria = new UpdateRacingStatusCriteria() { Disable151_41 = true, DisableDownlines151_41 = true, UpdateDisable151_41 = true} }
            },
            new object[] {
                new ProductStatus() {SportType = 151, BetType = 51, IsDisabled = true, IsDisabledDownline = true },
                new UpdateRacingStatusCriteriaMock() { Criteria = new UpdateRacingStatusCriteria() { Disable151_51 = true, DisableDownlines151_51 = true, UpdateDisable151_51 = true} }
            },
            new object[] {
                new ProductStatus() {SportType = 152, BetType = 41, IsDisabled = true, IsDisabledDownline = true },
                new UpdateRacingStatusCriteriaMock() { Criteria = new UpdateRacingStatusCriteria() { Disable152_41 = true, DisableDownlines152_41 = true, UpdateDisable152_41 = true} }
            },
            new object[] {
                new ProductStatus() {SportType = 152, BetType = 51, IsDisabled = true, IsDisabledDownline = true },
                new UpdateRacingStatusCriteriaMock() { Criteria = new UpdateRacingStatusCriteria() { Disable152_51 = true, DisableDownlines152_51 = true, UpdateDisable152_51 = true} }
            },
            new object[] {
                new ProductStatus() {SportType = 153, BetType = 51, IsDisabled = true, IsDisabledDownline = true },
                new UpdateRacingStatusCriteriaMock() { Criteria = new UpdateRacingStatusCriteria() { Disable153_51 = true, DisableDownlines153_51 = true, UpdateDisable153_51 = true} }
            },
        };

        [Test, TestCaseSource("MapProductStatusToSpParamTestCase")]
        public void MapProductStatusToSpParam_AnyArgument_MapDataCorrect(ProductStatus inputValue, UpdateRacingStatusCriteriaMock expectedValue)
        {
            // Actual
            var actualValue = new UpdateRacingStatusCriteriaMock();
            actualValue.Criteria = new UpdateRacingStatusCriteria();
            actualValue.MapProductStatusToSpParamMock(inputValue);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue.Criteria), JsonConvert.SerializeObject(actualValue.Criteria));
        }
    }
}
