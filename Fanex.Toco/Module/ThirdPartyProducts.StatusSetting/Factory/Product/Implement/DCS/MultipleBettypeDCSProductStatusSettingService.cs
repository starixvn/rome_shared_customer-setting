using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS
{
    internal class MultipleBettypeDCSProductStatusSettingService : DCSProductStatusSettingService
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
