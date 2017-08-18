using Fanex.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThirdPartyProducts.StatusSetting.Enums;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class NG2StatusSettingService : DCSProductStatusSettingService
    {
        public override List<ProductStatus> Select(GetProductStatusRequest request)
        {
            var criteria = new SelectNG2ProductStatusCriteria
            {
                CustId = request.CustId,
                ProductId = request.ProductId
            };

            return ObjectDbFactory.ExecuteReaderFunc(criteria, MapSpData).ToList();
        }

        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = new List<ProductStatusResult>();
            Validate(request);
            var criteria = new UpdateNG2ProductStatusCriteria
            {
                CustId = request.CustId,
                SubAccId = request.SubAccId,
                UserId = request.UserId,
                Ip = request.Ip,
                SystemId = request.SystemId
            };

            criteria.MapSpParams(request.ProductStatus);
            result.Add(ConvertXmlErrorMessage(ExecuteNonQuery(criteria), false));
            return result;
        }

        protected override ProductStatus MapSpData(IDataRecord record)
        {
            var producStatus = new ProductStatus
            {
                ProductId = (int)Enums.Product.NG2,
                BetType = Utils.ToInt32(record["Bettype"]),
                SportType = Utils.ToInt32(record["Sporttype"]),
                IsDisabled = Utils.ToBoolean(record["IsDisabled"]),
                IsDisabledUpline = Utils.ToBoolean(record["IsDisabledUpline"]),
                IsDisabledDownline = Utils.ToBoolean(record["IsDisabledDownline"]),
                HasDownline = Utils.ToBoolean(record["HasDownline"]),
                IsSync = Utils.ToBoolean(record["IsSync"]),
                IsHidden = Utils.ToBoolean(record["IsHidden"])
            };

            producStatus.GetNameFromRemotingRef();
            return producStatus;
        }
    }
}
