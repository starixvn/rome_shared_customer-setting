using System.Configuration;

namespace Fanex.CustomerSetting.Module.Casino.Configurations
{
    public static class AppSettings
    {
        static AppSettings()
        {
            LocalTest = ConfigurationManager.AppSettings["LocalTest"] == "1";
        }

        public static bool LocalTest { get; private set; }
    }
}
