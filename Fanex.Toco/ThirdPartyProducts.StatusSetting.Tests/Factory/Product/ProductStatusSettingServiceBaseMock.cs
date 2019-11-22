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

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public ProductStatusResult ExecuteNonQuery(CriteriaBase criteria)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            return base.ExecuteNonQuery(criteria);
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public void Validate(UpdateProductStatusRequest request)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            base.Validate(request);
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public ProductStatus MapSpData(IDataRecord record)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            return base.MapSpData(record);
        }
    }
}
