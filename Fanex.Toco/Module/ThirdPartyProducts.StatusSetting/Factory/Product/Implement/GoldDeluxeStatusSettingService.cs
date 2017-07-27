using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class GoldDeluxeStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatus> Select(GetProductStatusRequest request)
        {
            var criteria = new SelectGoldDeluxeStatusCriteria(request.CustId);

            return ObjectDbFactory.ExecuteReaderFunc(criteria, MapSpData).ToList();
        }

        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = new List<ProductStatusResult>();

            Validate(request);

            var goldDeluxeStatus = request.ProductStatus[0];

            var criteria = new UpdateGoldDeluxeStatusCriteria
            {
                CustId = request.CustId,
                Disable = goldDeluxeStatus.IsDisabled,
                EnableDownlines = !goldDeluxeStatus.IsDisabledDownline,
                SystemId = request.SystemId,
                UserId = request.UserId,
                SubAccId = request.SubAccId,
                Ip = request.Ip,
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
