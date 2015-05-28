// --------------------------------------------------------------------------
// <copyright file="Helper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Helper
    {
        public static ProductCollectionConfiguration Products
        {
            get
            {
                var products = EnableStatusConfiguration.Instance.ProductCollection;

                return products;
            }
        }

        public static IList<string> ProductIds
        {
            get
            {
                var products = Products;
                var ids = new List<string>();

                foreach (ProductElementConfiguration product in products)
                {
                    ids.Add(product.Id);
                }

                return ids;
            }
        }

        public static SpCollectionConfiguration GetStoredProcedures(string productId)
        {
            var products = Products;
            var product = products.Where(p => p.Id.Equals(productId, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            var sps = product.SpCollection;

            return sps;
        }

        public static string GetResource(string key)
        {
            var resource = new System.Resources.ResourceManager(Type.GetType("Fanex.CustomerSetting.EnableStatus.AllInOne.Resources.EnableStatusResources"));
            return resource.GetObject(key) as string;
        }
    }
}
