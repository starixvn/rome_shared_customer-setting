// -----------------------------------------------------------------------
// <copyright file="Authenticate.cs" company="Nexcel">
// @Steven.Nguyen
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Security.Authenticate
{
    /// <summary>
    /// Default Authenticate
    /// </summary>
    public class DefaultAuthenticate: IAuthenticate
    {
        public bool AllowRead(string action)
        {
            return true;
        }

        public bool AllowUpdate(string action)
        {
            return true;
        }

        public AuthenticateAction.AuthenticationType Policy
        {
            get;
            set;
        }


        public string Function
        {
            get;
            set;
        }
    }
}