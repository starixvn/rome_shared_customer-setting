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
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Helper
    {
        private static string resourceType = "Fanex.CustomerSetting.EnableStatus.AllInOne.Resources.EnableStatusResources";
        private static ResourceManager resourceManager;
        private static ProductCollectionConfiguration productCollection;

        public static ProductCollectionConfiguration Products
        {
            get
            {
                productCollection = productCollection ?? EnableStatusConfiguration.Instance.ProductCollection;

                return productCollection;
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
            resourceManager = resourceManager ?? new ResourceManager(Type.GetType(resourceType));
            return resourceManager.GetObject(key, CultureInfo.CurrentCulture) as string;
        }

        public static object GetParamValue<T, Z>(string mapProperty, T entity, Z context, bool reverse = false)
        {
            var props = typeof(T).GetProperties();
            var zprops = typeof(Z).GetProperties();
            foreach (var property in props)
            {
                if (property.Name.Equals(mapProperty, StringComparison.OrdinalIgnoreCase))
                {
                    var result = false;
                    var value = property.GetValue(entity, null);
                    value = value == null ? string.Empty : value;
                    bool.TryParse(value.ToString(), out result);
                    return reverse ? !result : value;
                }
            }

            foreach (var property in zprops)
            {
                if (property.Name.Equals(mapProperty, StringComparison.OrdinalIgnoreCase))
                {
                    var result = false;
                    var value = property.GetValue(context, null);
                    value = value == null ? string.Empty : value;
                    bool.TryParse(value.ToString(), out result);
                    return reverse ? !result : value;
                }
            }

            return default(object);
        }

        public static T MapResultValue<T>(string mapProperty, object mapValue, T entity, bool reverse = false, string defaultValue = null)
        {
            var props = typeof(T).GetProperties();
            foreach (var property in props)
            {                
                if (property.Name.Equals(mapProperty, StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(defaultValue))
                {
                    property.SetValue(entity, reverse ? !(bool)mapValue : mapValue, null);
                    break;
                }
            }

            return entity;
        }
    }
}
