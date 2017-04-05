using Fanex.Data;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Enums;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;
using ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement
{
    internal class NG2StatusSettingService : DCSProductStatusSettingService
    {
        public override List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            var criteria = new SelectNG2ProductStatusCriteria()
            {
                CustId = custId,
                ProductId = productId
            };

            var result = new List<ProductStatus>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                var reader = db.ExecuteReader();
                while (reader.Read())
                {
                    var producStatus = new ProductStatus()
                    {
                        ProductId = (int)ProductEnum.NG2,
                        BetType = Utils.ToInt32(reader["Bettype"]),
                        SportType = Utils.ToInt32(reader["Sporttype"]),
                        IsDisabled = Utils.ToBoolean(reader["IsDisabled"]),
                        IsDisabledUpline = Utils.ToBoolean(reader["IsDisabledUpline"]),
                        IsDisabledDownline = Utils.ToBoolean(reader["IsDisabledDownline"]),
                        HasDownline = Utils.ToBoolean(reader["HasDownline"]),
                        IsSync = Utils.ToBoolean(reader["IsSync"]),
                        IsHidden = Utils.ToBoolean(reader["IsHidden"])
                    };

                    producStatus.GetNameFromRemotingRef();
                    result.Add(producStatus);
                }

                return result;
            }
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
            var criteria = new UpdateNG2ProductStatusCriteria()
            {
                CustId = custId,
                SubAccId = subAccId,
                UserId = userId,
                IP = ip,
                SystemId = systemId
            };

            criteria.MapSPParams(productStatus);
            result.Add(ExecuteNonQuery(criteria));
            return result;
        }
    }
}
