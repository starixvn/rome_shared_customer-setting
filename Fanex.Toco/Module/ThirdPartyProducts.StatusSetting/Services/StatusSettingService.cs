using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Services
{
    public class StatusSettingService
    {
        public List<ProductStatus> GetProductStatus(GetProductStatusRequest request)
        {
            var productService = ProductFactory.CreateProductServiceFunc(request.ProductId);
            return productService.Select(request);
        }

        public List<ProductStatusResult> UpdateProductStatus(UpdateProductStatusRequest request)
        {
            var productService = ProductFactory.CreateProductServiceFunc(request.ProductId);
            return productService.Update(request);
        }
    }
}
