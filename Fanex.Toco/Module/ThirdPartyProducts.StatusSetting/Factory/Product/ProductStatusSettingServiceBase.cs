using Fanex.Data;
using Fanex.NAP.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Interface;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Factory.Product
{
    internal abstract class ProductStatusSettingServiceBase : IProductStatusSettingService
    {
        public virtual List<ProductStatus> Select(GetProductStatusRequest request)
        {
            var criteria = new SelectProductStatusCriteria
            {
                CustId = request.CustId,
                ProductId = request.ProductId
            };

            return ObjectDbFactory.ExecuteReaderFunc(criteria, MapSpData).ToList();
        }

        public abstract List<ProductStatusResult> Update(UpdateProductStatusRequest request);

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

        protected virtual void Validate(UpdateProductStatusRequest updateProductStatusRequest)
        {
            // Validate input custid & productId > 0, productStatus != null && Count > 0
            if (updateProductStatusRequest.CustId <= 0)
            {
                throw new ArgumentOutOfRangeException("custId", updateProductStatusRequest.CustId, string.Format(Message.ArgumentMustGreaterThanZero, "CustId"));
            }

            if (updateProductStatusRequest.ProductId <= 0)
            {
                throw new ArgumentOutOfRangeException("productId", updateProductStatusRequest.ProductId, string.Format(Message.ArgumentMustGreaterThanZero, "ProductId"));
            }

            if (updateProductStatusRequest.SystemId != 1 && updateProductStatusRequest.SystemId != 2 && updateProductStatusRequest.SystemId != 4)
            {
                throw new ArgumentOutOfRangeException("systemId", updateProductStatusRequest.SystemId, string.Format(Message.ArgumentSystemIdMustBeInRange, "SystemId"));
            }

            if (string.IsNullOrEmpty(updateProductStatusRequest.Ip))
            {
                throw new ArgumentNullException("ip", string.Format(Message.ArgumentCannotBeNullOrEmpty, "ip"));
            }

            if (updateProductStatusRequest.ProductStatus == null || updateProductStatusRequest.ProductStatus.Count == 0)
            {
                throw new ArgumentNullException("productStatus", string.Format(Message.ArgumentCannotBeNullOrEmpty, "productStatus"));
            }
        }
    }
}
