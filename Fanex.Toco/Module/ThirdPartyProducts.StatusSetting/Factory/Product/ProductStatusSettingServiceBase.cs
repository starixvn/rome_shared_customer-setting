using Fanex.Data;
using Fanex.NAP.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Interface;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;
using System.Linq;

namespace ThirdPartyProducts.StatusSetting.Factory.Product
{
    internal abstract class ProductStatusSettingServiceBase : IProductStatusSettingService
    {
        public virtual List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            var criteria = new SelectProductStatusCriteria
            {
                CustId = custId,
                ProductId = productId
            };

            return ObjectDbFactory.ExecuteReaderFunc(criteria, MapSpData).ToList();
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

        protected virtual ProductStatus MapSpData(IDataRecord record)
        {
            var producStatus = new ProductStatus
            {
                ProductId = Utils.ToInt32(record["ProductID"]),
                BetType = Utils.ToInt32(record["Bettype"]),
                SportType = Utils.ToInt32(record["Sporttype"]),
                IsDisabled = Utils.ToBoolean(record["IsDisabled"]),
                IsDisabledUpline = Utils.ToBoolean(record["IsDisabledUpline"]),
                IsDisabledDownline = Utils.ToBoolean(record["IsDisabledDownline"]),
                HasDownline = Utils.ToBoolean(record["HasDownline"]),
                IsSync = Utils.ToBoolean(record["IsSync"]),
                IsSyncedUpline = Utils.ToBoolean(record["IsSyncedUpline"]),
                IsSyncedDownline = Utils.ToBoolean(record["IsSyncedDownline"]),
                IsHidden = Utils.ToBoolean(record["IsHidden"]),
                IsHiddenUpline = Utils.ToBoolean(record["IsHiddenUpline"])
            };

            producStatus.GetNameFromRemotingRef();
            return producStatus;
        }

        protected ProductStatusResult ExecuteNonQuery(CriteriaBase criteria)
        {
            var outputs = ObjectDbFactory.ExecuteNonQueryFunc(criteria);
            return new ProductStatusResult
            {
                ErrorCode = outputs["errcode"].ToString(),
                ErrorMessage = outputs["errmsg"].ToString()
            };
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
                throw new ArgumentOutOfRangeException("systemId", systemId, string.Format(Message.ArgumentSystemIdMustBeInRange, "SystemId"));
            }

            if (string.IsNullOrEmpty(ip))
            {
                throw new ArgumentNullException("ip", string.Format(Message.ArgumentCannotBeNullOrEmpty, "ip"));
            }

            if (productStatus == null || productStatus.Count == 0)
            {
                throw new ArgumentNullException("productStatus", string.Format(Message.ArgumentCannotBeNullOrEmpty, "productStatus"));
            }
        }
    }
}
