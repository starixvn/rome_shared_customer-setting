using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapApi.Client.Configurations
{
    public class NapApiConfiguration : ConfigurationSection
    {
        private static NapApiConfiguration settings = ConfigurationManager.GetSection("NapApiConfiguration") as NapApiConfiguration;

        public static NapApiConfiguration Settings
        {
            get
            {
                return NapApiConfiguration.settings;
            }
        }

        [ConfigurationProperty("ServiceUris", IsRequired = true)]
        public string ServiceUris
        {
            get
            {
                return (string)this["ServiceUris"];
            }
            set
            {
                this["ServiceUris"] = (object)value;
            }
        }

        [ConfigurationProperty("SecretKey", IsRequired = true)]
        public string SecretKey
        {
            get
            {
                return (string)this["SecretKey"];
            }
            set
            {
                this["SecretKey"] = (object)value;
            }
        }
    }
}
