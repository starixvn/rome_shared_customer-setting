using System;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class RacingStatusSettingService : ProductStatusSettingServiceBase
    {
        private const char RacingSpliter = '\a';

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
            var criteria = new UpdateRacingStatusCriteria
            {
                CustId = custId,
                Ip = ip,
                Language = Utils.GetLanguageKeyForSql(),
                SubAccId = subAccId,
                UserId = userId,
                SystemId = systemId
            };

            criteria.MapSpParams(productStatus);
            result.Add(RefineErrorMessage(ExecuteNonQuery(criteria)));
            return result;
        }

        /// <summary>
        /// Old SP return -109, -110, -113, -114, -115, -116 as successful update message for each racing sporttype bettype. 
        /// If failed, only 1 ErrorCode, ErrorMessage returned.
        /// New behavior only need 1 success message, failed one will be as it is.
        /// Solution: Truncate all success messages & leave error message untouch.
        /// </summary>
        /// <param name="productStatusResult"></param>
        private static ProductStatusResult RefineErrorMessage(ProductStatusResult productStatusResult)
        {
            productStatusResult.ErrorCode = productStatusResult.ErrorCode.Trim(RacingSpliter);
            productStatusResult.ErrorMessage = productStatusResult.ErrorMessage.Trim(RacingSpliter);
            string[] errCodeArray = productStatusResult.ErrorCode.Split(RacingSpliter);
            if (IsSuccessCode(Convert.ToInt32(errCodeArray[0])))
            {
                productStatusResult.ErrorCode = "0";
                productStatusResult.ErrorMessage = string.Empty;
            }

            return productStatusResult;
        }

        private static bool IsSuccessCode(int code)
        {
            return code <= 0;
        }
    }
}
