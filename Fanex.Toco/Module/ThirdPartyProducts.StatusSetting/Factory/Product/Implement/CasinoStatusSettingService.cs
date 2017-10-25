using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class CasinoStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = new List<ProductStatusResult>();
            Validate(request);
            var isDisabled = request.ProductStatus[0].IsDisabled;

            var criteria = new UpdateCasinoStatusCriteria
            {
                CustId = request.CustId,
                LoginIP = request.Ip,
                Language = Utils.GetLanguageKeyForSql(),
                SubAccId = request.SubAccId,
                UserId = request.UserId,
                SystemId = request.SystemId,
                DisabledCS = isDisabled,
                EnableDownline = !isDisabled && !request.ProductStatus[0].IsDisabledDownline,
            };

            result.Add(ExecuteNonQuery(criteria));
            return result;
        }
    }
}
