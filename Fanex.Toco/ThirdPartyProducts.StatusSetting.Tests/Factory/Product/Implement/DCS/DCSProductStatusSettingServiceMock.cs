using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Implement.DCS
{
    internal class DCSProductStatusSettingServiceMock: DCSProductStatusSettingService
    {
        public ProductStatusResult ConvertXmlErrorMessageMock(ProductStatusResult productStatusResultWithXmlErrorMessage, bool showBettypes)
        {
            return base.ConvertXmlErrorMessage(productStatusResultWithXmlErrorMessage, showBettypes);
        }

        public ProductStatus MapSpDataMock(IDataRecord record)
        {
            return base.MapSpData(record);
        }
    }
}
