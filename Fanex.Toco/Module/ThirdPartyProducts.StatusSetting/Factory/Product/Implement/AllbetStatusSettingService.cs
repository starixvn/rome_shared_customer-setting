using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class AllbetStatusSettingService : DCSProductStatusSettingService
    {
        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = base.Update(request);
            if (result.Count > 0)
            {
                result[0] = ConvertXmlErrorMessage(result[0], true);
            }

            return result;
        }
    }
}
