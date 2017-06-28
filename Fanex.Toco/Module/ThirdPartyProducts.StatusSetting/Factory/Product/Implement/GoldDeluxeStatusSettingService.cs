using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class GoldDeluxeStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            var criteria = new SelectGoldDeluxeStatusCriteria(custId);

            return ObjectDbFactory.ExecuteReaderFunc(criteria, MapSpData).ToList();
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

            var goldDeluxeStatus = productStatus[0];

            var criteria = new UpdateGoldDeluxeStatusCriteria
            {
                CustId = custId,
                Disable = goldDeluxeStatus.IsDisabled,
                EnableDownlines = !goldDeluxeStatus.IsDisabledDownline,
                SystemId = systemId,
                UserId = userId,
                SubAccId = subAccId,
                Ip = ip,
                Language = Utils.GetLanguageKeyForSql()
            };

            result.Add(ExecuteNonQuery(criteria));

            return result;
        }

        protected override ProductStatus MapSpData(IDataRecord record)
        {
            var producStatus = new ProductStatus
            {
                ProductId = Utils.ToInt32(record["ProductID"]),
                SportType = Utils.ToInt32(record["Sporttype"]),
                BetType = Utils.ToInt32(record["Bettype"]),

                IsDisabled = Utils.ToBoolean(record["IsDisabled"]),
                IsSyncedDownline = Utils.ToBoolean(record["IsSyncedDownline"]),
                HasDownline = Utils.ToBoolean(record["HasDownline"]),
                IsDisabledUpline = Utils.ToBoolean(record["IsDisabledUpline"]),
                IsSyncedUpline = Utils.ToBoolean(record["IsSyncedUpline"]),
                IsDisabledDownline = Utils.ToBoolean(record["IsDisabledDownline"]),
                IsSync = Utils.ToBoolean(record["IsSync"])
            };

            producStatus.GetNameFromRemotingRef();
            return producStatus;
        }
    }
}
