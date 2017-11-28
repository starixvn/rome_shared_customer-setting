namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Configurations
{
    using System.Configuration;

    public class ColossusBetSection : ConfigurationSection
    {
        private static ColossusBetSection _instance;

        public static ColossusBetSection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (ColossusBetSection)ConfigurationManager.GetSection("CustomerSetting/ColossusBet");
                }

                return _instance;
            }
        }

        [ConfigurationProperty("AuthorizationServiceType", DefaultValue = "", IsRequired = true)]
        public string AuthorizationServiceType
        {
            get
            {
                return (string)base["AuthorizationServiceType"];
            }
        }

        [ConfigurationProperty("UnAuthorizedPath", DefaultValue = "", IsRequired = false)]
        public string UnAuthorizedPath
        {
            get
            {
                return (string)base["UnAuthorizedPath"];
            }
        }
    }
}