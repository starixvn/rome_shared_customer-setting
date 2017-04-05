using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Interface
{
    internal interface IProductStatusSettingService
    {
        List<ProductStatus> Select(int custId, int productId, int roleId);

        List<ProductStatusResult> Update(int custId, int subAccId, int uplineId, int productId, int userId, int systemId, string ip, List<ProductStatus> productStatus);
    }
}
