namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;

    public static class AppSettings
    {
        public static List<string> PT3DecimalSites()
            => ConfigurationManager.AppSettings["PT3DecimalSites"] == null ?
            new List<string> { "789y" } :
            ConfigurationManager.AppSettings["PT3DecimalSites"].ToString().Split(',').Select(site => site.Trim()).ToList();
    }
}