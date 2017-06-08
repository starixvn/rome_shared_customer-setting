using System.Data;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement
{
    internal class NG2StatusSettingServiceMock : NG2StatusSettingService
    {
        public ProductStatus MapSpDataMock(IDataRecord record)
        {
            return MapSpData(record);
        }
    }
}
