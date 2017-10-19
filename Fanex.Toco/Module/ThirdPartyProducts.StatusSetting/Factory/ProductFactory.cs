using System;
using ThirdPartyProducts.StatusSetting.Factory.Product.Interface;

namespace ThirdPartyProducts.StatusSetting.Factory
{
    internal static class ProductFactory
    {
        private const string productNamespace = "ThirdPartyProducts.StatusSetting.Factory.Product.Implement";

        private static Func<int, IProductStatusSettingService> createProductServiceFunc;
        
        internal static Func<int, IProductStatusSettingService> CreateProductServiceFunc
        {
            get
            {
                return createProductServiceFunc ?? CreateInstance;
            }

            set
            {
                createProductServiceFunc = value;
            }
        }

        private static IProductStatusSettingService CreateInstance(int productId)
        {
            var productName = ((Enums.Product)productId).ToString();
            return (IProductStatusSettingService)Activator.CreateInstance(
                Type.GetType(
                    string.Format(
                        "{0}.{1}StatusSettingService",
                        productNamespace,
                        productName)));
        }
    }
}
