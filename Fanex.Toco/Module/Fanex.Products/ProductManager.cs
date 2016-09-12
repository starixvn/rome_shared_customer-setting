using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fanex.Products.Configurations;
using Fanex.Products.Services;
using Fanex.Products.Utilities;

namespace Fanex.Products
{
    public static class ProductManager
    {
        private static IDictionary<int, BaseProduct> _productsDictionary = null;

        public static IDictionary<int, BaseProduct> ProductsDictionary
        {
            get
            {
                if (_productsDictionary == null)
                {
                    _productsDictionary = GetProductsDictionary();
                }

                return _productsDictionary;
            }
        }

        public static void SetProductsStatusToSession(Hashtable outputParams)
        {
            var products = ProductsDictionary.Values;

            foreach (Product_23 product in products)
            {
                product.SetProductStatusToSession(outputParams);
            }
        }

        public static BaseProduct GetProduct(int productId)
        {
            var productsDictionary = ProductsDictionary;

            return productsDictionary.ContainsKey(productId) ? productsDictionary[productId] : null;
        }

        public static bool IsActive(int productId)
        {
            var productsDictionary = ProductsDictionary;

            return productsDictionary.ContainsKey(productId) && productsDictionary[productId].Active;
        }

        public static bool IsShowReport(int productId)
        {
            var productsDictionary = ProductsDictionary;

            return productsDictionary.ContainsKey(productId) && productsDictionary[productId].ShowReport;
        }

        public static bool IsShowDCS(int productId)
        {
            var productsDictionary = ProductsDictionary;

            return productsDictionary.ContainsKey(productId) && productsDictionary[productId].ShowDCS;
        }

        public static bool? IsSynced(int productId)
        {
            var productsDictionary = ProductsDictionary;

            return productsDictionary.ContainsKey(productId) ? productsDictionary[productId].Synced : new bool?();
        }

        public static bool IsDisabled(int productId)
        {
            var productsDictionary = ProductsDictionary;

            return productsDictionary.ContainsKey(productId) && productsDictionary[productId].Disabled;
        }

        public static string GetProductName(int productId)
        {
            return RefDataUtility.GetProductName(productId);
        }

        private static IDictionary<int, BaseProduct> GetProductsDictionary()
        {
            IDictionary<int, BaseProduct> products = new Dictionary<int, BaseProduct>(16);

            BaseProduct product;
            string baseProductNamespace = typeof(Product_23).Namespace;

            var productElements = ProductSection.Instance.Products
                .Cast<ProductElement>()
                .Where(p => !string.IsNullOrEmpty(p.StatusHandlerType));

            foreach (ProductElement item in productElements)
            {
                product = (BaseProduct)Activator.CreateInstance(
                    Type.GetType(string.Format("{0}.{1}", baseProductNamespace, item.StatusHandlerType)),
                    BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new object[] { item.Active },
                    null);

                if (product != null)
                {
                    product.Id = item.Id;
                    product.Name = item.Name;
                    product.RemotingRefName = RefDataUtility.GetProductName(item.Id);
                    product.SportId = item.SportId;
                    product.ReportSPParam = item.ReportSPParam;
                    product.ViewPTLink = item.ViewPTLink;
                    product.ViewCommLink = item.ViewCommLink;
                    product.SettingPTLink = item.SettingPTLink;

                    products.Add(product.Id, product);
                }
            }

            return products;
        }
    }
}
