// ************************************************************************************
// <copyright file="ResourceHelper.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Services
{
    using System;
    using System.Globalization;
    using System.Web;
    using Fanex.Common;
    using RacebookResources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResourceHelper
    {
        public static void SetCulture()
        {
            string language = Converter.ToString2(HttpContext.Current.Session["culture"]);
            language = string.IsNullOrEmpty(language) ? "en-US" : language;
            Message.Culture = new CultureInfo(language);
        }

        public static string GetResource(string key)
        {
            var resourceValue = Message.ResourceManager.GetString(key);
            return resourceValue;
        }
    }
}