using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class VirtualSportStatusSettingService : ProductStatusSettingServiceBase
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
            var result = new List<ProductStatusResult>();
            Validate(custId, subAccId, productId, userId, systemId, ip, productStatus);
            var criteria = new UpdateVirtualSportStatusCriteria
            {
                CustId = custId,
                Ip = ip,
                Language = Utils.GetLanguageKeyForSql(),
                SubAccId = subAccId,
                UserId = userId,
                SystemId = systemId,
                Disable = productStatus[0].IsDisabled,
                DisableDownlines = productStatus[0].IsDisabledDownline
            };

            result.Add(ExecuteNonQuery(criteria));
            return result;
        }
    }
}
