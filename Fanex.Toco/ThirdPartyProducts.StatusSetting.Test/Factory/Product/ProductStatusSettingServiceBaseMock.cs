using Fanex.NAP.Framework.Data;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory.Product;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product
{
    internal class ProductStatusSettingServiceBaseMock : ProductStatusSettingServiceBase
    {
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
            return new List<ProductStatusResult>();
        }

        public ProductStatusResult ExecuteNonQuery(CriteriaBase criteria)
        {
            return base.ExecuteNonQuery(criteria);
        }

        public void Validate(
            int custId,
            int subAccId,
            int productId,
            int userId,
            int systemId,
            string ip,
            List<ProductStatus> productStatus)
        {
            base.Validate(custId, subAccId, productId, userId, systemId, ip, productStatus);
        }

        public ProductStatus MapSpData(IDataRecord record)
        {
            return base.MapSpData(record);
        }
    }
}
