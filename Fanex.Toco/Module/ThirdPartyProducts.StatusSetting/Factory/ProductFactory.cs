using System;
using System.Reflection;
using ThirdPartyProducts.StatusSetting.Enums;
using ThirdPartyProducts.StatusSetting.Factory.Product;

namespace ThirdPartyProducts.StatusSetting.Factory
{
    internal static class ProductFactory
    {
        private static string productNamespace = "ThirdPartyProducts.StatusSetting.Factory.Product.Implement";

        public static ProductStatusSettingServiceBase CreateInstance(int productId)
        {
            var productName = ((Enums.Product)productId).ToString();
            return (ProductStatusSettingServiceBase)Activator.CreateInstance(
                Type.GetType(
                    string.Format(
                        "{0}.{1}StatusSettingService",
                        productNamespace,
                        productName)));
        }
    }
}
