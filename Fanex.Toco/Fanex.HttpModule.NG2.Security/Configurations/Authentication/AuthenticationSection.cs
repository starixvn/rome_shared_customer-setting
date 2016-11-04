namespace Fanex.HttpModule.NG2.Security.Configurations.Authentication
{
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AuthenticationSection : ConfigurationSection
    {
        public static AuthenticationSection Instance
        {
            get
            {
                var authenticationSection =
                 ConfigurationManager.GetSection("AuthenticationSectionNG2") as AuthenticationSection;
                return authenticationSection;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
        public GroupLevelCollection GroupLevel
        {
            get
            {
                return (GroupLevelCollection)base[string.Empty];
            }
        }

        [ConfigurationProperty("fullpermission", DefaultValue = "0", IsRequired = false)]
        public string FullPermission
        {
            get { return (string)this["fullpermission"]; }
            set { this["fullpermission"] = value.ToLower(); }
        }
    }
}