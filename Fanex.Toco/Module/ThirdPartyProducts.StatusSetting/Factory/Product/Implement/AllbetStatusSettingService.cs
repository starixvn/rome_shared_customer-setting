using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class AllbetStatusSettingService : DCSProductStatusSettingService
    {
        public override List<ProductStatusResult> Update(
            int custId,
            int subAccId,
            int uplineId,
            int productId,
            int userId,
            int systemId,
            string ip,
            List<ProductStatus> productStatus)
        {
            var result = base.Update(custId, subAccId, uplineId, productId, userId, systemId, ip, productStatus);
            if (result.Count > 0)
            {
                result[0] = ConvertXmlErrorMessage(result[0], true);
            }

            return result;
        }
    }
}
