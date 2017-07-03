using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Interface
{
    internal interface IProductStatusSettingService
    {
        List<ProductStatus> Select(GetProductStatusRequest request);

        List<ProductStatusResult> Update(UpdateProductStatusRequest request);
    }
}
