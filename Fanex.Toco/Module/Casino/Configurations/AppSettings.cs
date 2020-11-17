using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Fanex.CustomerSetting.Module.Casino.Configurations
{
    public static class AppSettings
    {
        static AppSettings()
        {
            LocalTest = ConfigurationManager.AppSettings["LocalTest"] == "1";
        }

        public static bool LocalTest { get; private set; }

        public static List<string> PT3DecimalSites()
            => ConfigurationManager.AppSettings["PT3DecimalSites"] == null ?
            new List<string> { "789y" } :
            ConfigurationManager.AppSettings["PT3DecimalSites"].ToString().Split(',').Select(site => site.Trim()).ToList();
    }
}
