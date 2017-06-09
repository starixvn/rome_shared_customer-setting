using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Test.Factory.Product.Criteria
{
    public class UpdateRacingStatusCriteriaMock
    {
        internal UpdateRacingStatusCriteria Criteria { get; set; }

        public void MapProductStatusToSpParamMock(ProductStatus productStatus)
        {
            Criteria.MapProductStatusToSpParam(productStatus);
        }
    }
}
