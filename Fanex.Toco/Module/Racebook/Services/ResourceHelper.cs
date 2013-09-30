using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Fanex.Common;
using RacebookResources;

namespace Fanex.CustomerSetting.Module.Racebook.Services
{
    public class ResourceHelper
    {
        public static void SetCulture()
        {
            string language = Converter.ToString2(HttpContext.Current.Session["culture"]);
            language = String.IsNullOrEmpty(language) ? "en-US" : language;
            Message.Culture = new CultureInfo(language);
        }

        public static string GetResource(string key)
        {
            var resourceValue = Message.ResourceManager.GetString(key);
            return resourceValue;
        }
    }

}