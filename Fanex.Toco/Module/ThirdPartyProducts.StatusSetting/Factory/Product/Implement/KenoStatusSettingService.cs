using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class KenoStatusSettingService : ProductStatusSettingServiceBase
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
            foreach (var item in productStatus)
            {
                var criteria = new UpdateKenoStatusCriteria()
                {
                    CustId = custId,
                    Bettype = item.BetType,
                    UplineId = uplineId,
                    IP = ip,
                    Language = Utils.GetLanguageKeyForSQL(),
                    SubAccId = subAccId,
                    UserId = userId,
                    SystemId = systemId,
                    Disable = item.IsDisabled,
                    DisableDownlines = item.IsDisabledDownline
                };

                result.Add(ExecuteNonQuery(criteria));
            }

            return result;
        }
    }
}
