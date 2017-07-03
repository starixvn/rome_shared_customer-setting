using Fanex.NAP.Framework.Data;
using System.Collections.Generic;
using System.Data;
using ThirdPartyProducts.StatusSetting.Factory.Product;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product
{
    internal class ProductStatusSettingServiceBaseMock : ProductStatusSettingServiceBase
    {
        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            return new List<ProductStatusResult>();
        }

        public ProductStatusResult ExecuteNonQuery(CriteriaBase criteria)
        {
            return base.ExecuteNonQuery(criteria);
        }

        public void Validate(UpdateProductStatusRequest request)
        {
            base.Validate(request);
        }

        public ProductStatus MapSpData(IDataRecord record)
        {
            return base.MapSpData(record);
        }
    }
}
