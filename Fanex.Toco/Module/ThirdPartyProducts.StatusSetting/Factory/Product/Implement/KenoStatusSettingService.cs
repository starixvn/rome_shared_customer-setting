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

        public override List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            return base.Select(custId, productId, roleId).Where(o => !IsHiddenBettype(o.BetType)).ToList();
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
            foreach (var item in productStatus)
            {
                var criteria = new UpdateKenoStatusCriteria
                {
                    CustId = custId,
                    Bettype = item.BetType,
                    UplineId = uplineId,
                    Ip = ip,
                    Language = Utils.GetLanguageKeyForSql(),
                    SubAccId = subAccId,
                    UserId = userId,
                    SystemId = systemId,
                    Disable = item.IsDisabled,
                    DisableDownlines = item.IsDisabledDownline
                };

                result.Add(RefineErrorMessage(item, ExecuteNonQuery(criteria)));
            }

            return result;
        }

        private static ProductStatusResult RefineErrorMessage(ProductStatus input, ProductStatusResult output)
        {
            if (Convert.ToInt32(output.ErrorCode) != 0)
            {
                output.ErrorMessage = Utils.GetBetTypeName(input.BetType.ToString(), Utils.GetLanguageKeyForRemotingRef()) + ": " + output.ErrorMessage;
            }

            return output;
        }

        private static bool IsHiddenBettype(int bettypeId)
        {
            if (Array.Find<int>(HiddenBettypes, p => p == bettypeId) != 0)
            {
                return true;
            }

            return false;
        }
    }
}
