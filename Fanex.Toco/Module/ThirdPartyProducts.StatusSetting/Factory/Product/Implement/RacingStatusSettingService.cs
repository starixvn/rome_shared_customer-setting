using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class RacingStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            return base.Select(custId, productId, roleId);
        }

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
            var criteria = new UpdateRacingStatusCriteria()
            {
                CustId = custId,
                IP = ip,
                Language = Utils.GetLanguageKeyForSQL(),
                SubAccId = subAccId,
                UserId = userId,
                SystemId = systemId
            };

            criteria.MapSPParam(productStatus);
            result.Add(ExecuteNonQuery(criteria));
            return result;
        }
    }
}
