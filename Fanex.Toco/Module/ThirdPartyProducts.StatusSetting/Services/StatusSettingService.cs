using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Factory;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Services
{
    public class StatusSettingService
    {
        public List<ProductStatus> GetProductStatus(int custId, int productId, int roleId)
        {
            var productService = ProductFactory.CreateProductServiceFunc(productId);
            return productService.Select(custId, productId, roleId);
        }

        public List<ProductStatusResult> UpdateProductStatus(
            int custId, 
            int subAccId, 
            int uplineId,
            int productId, 
            int userId, 
            int systemId, 
            string ip, 
            List<ProductStatus> productStatus)
        {
            var productService = ProductFactory.CreateProductServiceFunc(productId);
            return productService.Update(custId, subAccId, uplineId, productId, userId, systemId, ip, productStatus);
        }
    }
}
