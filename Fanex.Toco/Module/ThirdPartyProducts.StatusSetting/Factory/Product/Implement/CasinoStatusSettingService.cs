using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class CasinoStatusSettingService : ProductStatusSettingServiceBase
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
            var criteria = new UpdateCasinoStatusCriteria()
            {
                CustId = custId,
                LoginIP = ip,
                Language = Utils.GetLanguageKeyForSQL(),
                SubAccId = subAccId,
                UserId = userId,
                SystemId = systemId,
                DisabledCS = productStatus[0].IsDisabled,
                EnableDownline = !productStatus[0].IsDisabledDownline,
            };

            result.Add(ExecuteNonQuery(criteria));
            return result;
        }
    }
}
