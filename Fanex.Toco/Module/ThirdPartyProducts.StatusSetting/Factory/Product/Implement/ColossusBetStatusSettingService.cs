using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class ColossusBetStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = new List<ProductStatusResult>();
            Validate(request);

            var isDisabled = request.ProductStatus[0].IsDisabled;
            var criteria = new UpdateColossusBetStatusCriteria
            {
                CustId = request.CustId,
                Ip = request.Ip,
                Language = Utils.GetLanguageKeyForSql(),
                SubAccId = request.SubAccId,
                UserId = request.UserId,
                SystemId = request.SystemId,
                Disable = isDisabled,
                EnableDownlines = GetEnableDownlineValue(request)
            };

            result.Add(ExecuteNonQuery(criteria));
            return result;
        }
    }
}
