using System.Data;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    internal class GoldDeluxeStatusSettingServiceMock : GoldDeluxeStatusSettingService
    {
        public ProductStatus MapSpDataMock(IDataRecord record)
        {
            return base.MapSpData(record);
        }
    }
}
