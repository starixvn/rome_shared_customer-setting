using Fanex.Data;
using Fanex.NAP.Framework.Data;
using System;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Interface;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Factory.Product
{
    internal abstract class ProductStatusSettingServiceBase : IProductStatusSettingService
    {
        private static readonly int[] hiddenBettypes = { 1502, 1506, 1507, 1509 };

        public virtual List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            var criteria = new SelectProductStatusCriteria()
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

                    if (this.IsHiddenBettype(producStatus.BetType))
                    {
                        continue;
                    }

                    producStatus.GetNameFromRemotingRef();
                    result.Add(producStatus);
                }

                return result;
            }
        }

        public abstract List<ProductStatusResult> Update(
            int custId,
            int subAccId,
            int uplineId,
            int productId,
            int userId,
            int systemId,
            string ip,
            List<ProductStatus> productStatus);

        protected ProductStatusResult ExecuteNonQuery(CriteriaBase criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                return new ProductStatusResult()
                {
                    ErrorCode = db.GetOuputParams()["errcode"].ToString(),
                    ErrorMessage = db.GetOuputParams()["errmsg"].ToString()
                };
            }
        }

        protected virtual void Validate(int custId, int subAccId, int productId, int userId, int systemId, string ip, List<ProductStatus> productStatus)
        {
            // Validate input custid & productId > 0, productStatus != null && Count > 0
            if (custId <= 0)
            {
                throw new ArgumentOutOfRangeException("custId", custId, string.Format(Message.ArgumentMustGreaterThanZero, "CustId"));
            }

            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException("productId", productId, string.Format(Message.ArgumentMustGreaterThanZero, "ProductId"));
            }

            if (systemId != 1 && systemId != 2 && systemId != 4)
            {
                throw new ArgumentOutOfRangeException("systemId", productId, string.Format(Message.ArgumentSystemIdMustBeInRange, "SystemId"));
            }

            if (string.IsNullOrEmpty(ip))
            {
                throw new ArgumentOutOfRangeException("IP", productId, string.Format(Message.ArgumentCannotBeNullOrEmpty, "IP"));
            }

            if (productStatus == null || productStatus.Count == 0)
            {
                throw new ArgumentNullException("ProductStatus", string.Format(Message.ArgumentCannotBeNullOrEmpty, "ProductStatus"));
            }
        }

        private bool IsHiddenBettype(int bettypeId)
        {
            if (Array.Find<int>(hiddenBettypes, p=> p==bettypeId) != 0)
            {
                return true;
            }

            return false;
        }
    }
}
