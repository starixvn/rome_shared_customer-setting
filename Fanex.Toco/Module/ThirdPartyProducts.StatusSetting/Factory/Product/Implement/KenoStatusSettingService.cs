using System;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class KenoStatusSettingService : ProductStatusSettingServiceBase
    {
        private static readonly int[] HiddenBettypes = { 1502, 1506, 1507, 1509 };

        public override List<ProductStatus> Select(GetProductStatusRequest request)
        {
            return base.Select(request).Where(o => !IsHiddenBettype(o.BetType)).ToList();
        }

        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = new List<ProductStatusResult>();
            Validate(request);
            foreach (var item in request.ProductStatus)
            {
                var criteria = new UpdateKenoStatusCriteria
                {
                    CustId = request.CustId,
                    Bettype = item.BetType,
                    UplineId = request.UplineId,
                    Ip = request.Ip,
                    Language = Utils.GetLanguageKeyForSql(),
                    SubAccId = request.SubAccId,
                    UserId = request.UserId,
                    SystemId = request.SystemId,
                    Disable = item.IsDisabled,
                    DisableDownlines = item.IsDisabledDownline
                };

                result.Add(RefineErrorMessage(item, ExecuteNonQuery(criteria)));
            }

            return result;
        }

        public static ProductStatusResult RefineErrorMessage(ProductStatus input, ProductStatusResult output)
        {
            if (Convert.ToInt32(output.ErrorCode) != 0)
            {
                output.ErrorMessage = Utils.GetBetTypeName(input.BetType.ToString(), Utils.GetLanguageKeyForRemotingRef()) + ": " + output.ErrorMessage;
            }

            return output;
        }

        public static bool IsHiddenBettype(int bettypeId)
        {
            if (Array.Find<int>(HiddenBettypes, p => p == bettypeId) != 0)
            {
                return true;
            }

            return false;
        }
    }
}
