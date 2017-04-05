using Fanex.Data;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS
{
    internal class DCSProductStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            var criteria = new SelectDCSProductStatusCriteria()
            {
                CustId = custId,
                ProductId = productId,
                RoleId = roleId
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
                        ProductId = Utils.ToInt32(reader["ProductID"]),
                        BetType = Utils.ToInt32(reader["Bettype"]),
                        SportType = Utils.ToInt32(reader["Sporttype"]),
                        IsDisabled = Utils.ToBoolean(reader["IsDisabled"]),
                        IsDisabledUpline = Utils.ToBoolean(reader["IsDisabledUpline"]),
                        IsDisabledDownline = Utils.ToBoolean(reader["IsDisabledDownline"]),
                        HasDownline = Utils.ToBoolean(reader["HasDownline"]),
                        IsSync = Utils.ToBoolean(reader["IsSync"]),
                        IsSyncedUpline = Utils.ToBoolean(reader["IsSyncedUpline"]),
                        IsSyncedDownline = Utils.ToBoolean(reader["IsSyncedDownline"]),
                        IsHidden = Utils.ToBoolean(reader["IsHidden"]),
                        IsHiddenUpline = Utils.ToBoolean(reader["IsHiddenUpline"])
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
            var criteria = new UpdateDCSProductStatusCriteria()
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
