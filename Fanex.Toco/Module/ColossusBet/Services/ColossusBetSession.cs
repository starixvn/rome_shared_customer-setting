namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Configurations;
    using Fanex.CustomerSetting.Module.Core.Web;

    /// <summary>
    /// Bound the app session from core.
    /// </summary>
    public class ColossusBetSession : IColossusBetSession
    {
        public int UserId
        {
            get
            {
                return AppSession.AccId;
            }
        }

        public int SubAccId
        {
            get
            {
                return AppSession.SubAccountId;
            }
        }

        public string Ip
        {
            get
            {
                return Utils.GetClientIP();
            }
        }

        public string Language
        {
            get
            {
                return AppSession.Language;
            }
        }

        public bool IsLocalTest
        {
            get
            {
                return AppSettings.IsLocalTest;
            }
        }
    }
}