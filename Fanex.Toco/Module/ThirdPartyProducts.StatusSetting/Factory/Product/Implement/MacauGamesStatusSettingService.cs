using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class MacauGamesStatusSettingService : DCSProductStatusSettingService
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
                result[0] = ConvertXMLErrorMessage(result[0], false);
            }

            return result;
        }
    }
}
