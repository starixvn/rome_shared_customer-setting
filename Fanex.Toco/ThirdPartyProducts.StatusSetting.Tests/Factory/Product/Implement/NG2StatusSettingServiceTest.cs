using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    [TestFixture]
    [Category("ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement")]
    public class NG2StatusSettingServiceTest
    {
        private NG2StatusSettingServiceMock _settingService;
        private IEnumerable<ProductStatus> _dataTest;

        [SetUp]
        public void Init()
        {
            _settingService = new NG2StatusSettingServiceMock();

            _dataTest = CreateDataTest();
            ObjectDbFactory.ExecuteReaderFunc = (criteria, MapSpData) => _dataTest;
        }

        [Test]
        public void Select_Always_ReturnsCorrectData()
        {
            // Actual
            var request = new GetProductStatusRequest()
            {
                CustId = 0,
                ProductId = 0
            };
            var actualValue = _settingService.Select(request);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(_dataTest), JsonConvert.SerializeObject(actualValue));
        }

        [Test]
        public void MapSpData_AnyArgument_MapDataCorrect()
        {
            // Arrange
            var substitute = Substitute.For<IDataRecord>();
            substitute["Bettype"].Returns("1");
            substitute["Sporttype"].Returns("1");
            substitute["IsDisabled"].Returns("true");
            substitute["IsDisabledUpline"].Returns("true");
            substitute["IsDisabledDownline"].Returns("true");
            substitute["HasDownline"].Returns("true");
            substitute["IsSync"].Returns("true");
            substitute["IsHidden"].Returns("true");
            Fanex.RemotingRef.RefDataTypeContainer
                .ConnectTo(new Uri("http://ha.nexdev.net:7258/main/"))
                .FromApplication("ThirdPartyProducts.StatusSetting.Tests")
                .SupportOnlyDefaultLanguage();
            var language = Utils.GetLanguageKeyForRemotingRef();
            var expectedValue = new ProductStatus() {
                ProductId=(int)Enums.Product.NG2,
                BetType = 1,
                SportType=1,
                IsDisabled=true,
                IsDisabledUpline=true,
                IsDisabledDownline=true,
                HasDownline=true,
                IsSync=true,
                IsHidden=true,
                ProductName = Utils.GetProductName(((int)Enums.Product.NG2).ToString(), language),
                BetTypeName = Utils.GetBetTypeName("1", language),
                SportName = Utils.GetSportName("1", language)
            };

            // Actual
            var actualvalue = _settingService.MapSpDataMock(substitute);

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualvalue));
        }

        [Test]
        public void Update_AnyArgument_ReturnCorrectData()
        {
            // Arrange
            var request = new UpdateProductStatusRequest()
            {
                CustId = 1,
                SubAccId = 1,
                ProductId = 1,
                SystemId = 1,
                UplineId = 1,
                UserId = 1,
                Ip = "127.0.0.1",
                ProductStatus = new List<ProductStatus>() {
                    new ProductStatus()
                }
            };
            ObjectDbFactory.ExecuteNonQueryFunc = criteria => ExecuteNonQueryTest();
            var expectedValue = new List<ProductStatusResult>();
            expectedValue.Add(new ProductStatusResult() { ErrorCode = "60035", ErrorMessage = "You are not allowed to disable downline's product." });
            // Actual
            var actualValue = _settingService.Update(request);
            ObjectDbFactory.ExecuteNonQueryFunc = null;

            // Assert
            Assert.AreEqual(JsonConvert.SerializeObject(expectedValue), JsonConvert.SerializeObject(actualValue));
        }

        private Hashtable ExecuteNonQueryTest()
        {
            return new Hashtable()
            {
                { "errcode", "102" },
                { "errmsg", "<Root MsgID=\"60035\" Msg=\"You are not allowed to disable for downlines.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>" }
            };
        }

        private IEnumerable<ProductStatus> CreateDataTest()
        {
            var result = new List<ProductStatus>()
            {
                new ProductStatus()
                {
                    BetType =2401,
                    SportType =211,
                    ProductId =23
                },
            };

            return result;
        }
    }
}
