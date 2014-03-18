// --------------------------------------------------------------------------
// <copyright file="CurrentContext.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/18">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Web
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Net;
    using System.Web;

    /// <summary>
    /// Current context of session.
    /// </summary>
    public class CurrentContext : ICurrentContext
    {
        private static string loginErrorMessage = string.Empty;
        private static HttpContext _current = null;
        private static NameValueCollection _serverVariables = null;

        public CurrentContext()
            : this(HttpContext.Current, null)
        {
        }

        internal CurrentContext(HttpContext httpContext, NameValueCollection serverVariables)
        {
            _current = httpContext;
            _serverVariables = serverVariables ?? _current.Request.ServerVariables;
        }

        public static string LoginErrorMessage
        {
            get { return CurrentContext.loginErrorMessage; }
            set { CurrentContext.loginErrorMessage = value; }
        }

        public static ICurrentContext Instance
        {
            get
            {
                return new CurrentContext();
            }
        }

        public HttpContext Context
        {
            get { return _current; }
        }

        public string Ip4Address
        {
            get
            {
                string ip = GetIp4AddressFromUserHostAddress();
                return !string.IsNullOrEmpty(ip) ? ip : GetIp4AddressFromDnsHostName().Trim();
            }
        }

        public string Ip6Address
        {
            get
            {
                return !string.IsNullOrEmpty(_serverVariables["HTTP_X_FORWARDED_FOR"]) ?
                    GetTrueIpFromHttpXForwardedFor(_serverVariables["HTTP_X_FORWARDED_FOR"])
                    : _serverVariables["REMOTE_ADDR"];
            }
        }

        public string CountryCode
        {
            get
            {
                try
                {
                    RegionInfo regionInfo = this.ResolveCountry(Context.Request);
                    return regionInfo == null ? null : regionInfo.GeoId.ToString();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string DomainName
        {
            get { return _serverVariables["SERVER_NAME"]; }
        }

        public string FullDomainName
        {
            get
            {
                return GetProtocol() + GetDomain() + ":" + GetPort();
            }
        }

        internal static string GetTrueIpFromHttpXForwardedFor(string httpXForwardedFor)
        {
            string[] range = httpXForwardedFor.Split(',');
            return range.Length >= 0 ? range[0].Trim() : string.Empty;
        }

        internal static bool UserLanguagesIsValid(string[] userLanguages)
        {
            return userLanguages != null && userLanguages.Length == 0;
        }

        /// <summary>
        /// Gets the IP4 address from host name or address.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns>The IP4 of current host name or address.</returns>
        internal static string GetIp4AddressFromHostNameOrAddress(string hostNameOrAddress)
        {
            string result = string.Empty;

            foreach (IPAddress ipa in Dns.GetHostAddresses(hostNameOrAddress))
            {
                if (ipa.AddressFamily.ToString().Equals("InterNetwork"))
                {
                    result = ipa.ToString();
                    break;
                }
            }

            return result;
        }

        internal string GetIp4AddressFromDnsHostName()
        {
            return GetIp4AddressFromHostNameOrAddress(Dns.GetHostName());
        }

        internal string GetIp4AddressFromUserHostAddress()
        {
            return GetIp4AddressFromHostNameOrAddress(Context.Request.UserHostAddress);
        }

        internal RegionInfo ResolveCountry(HttpRequest request)
        {
            CultureInfo culture = this.ResolveCulture(request.UserLanguages);
            return culture != null ? new RegionInfo(culture.LCID) : null;
        }

        internal CultureInfo ResolveCulture(string[] userLanguages)
        {
            return !UserLanguagesIsValid(userLanguages) ? null : CultureInfo.CreateSpecificCulture(userLanguages[0].ToLowerInvariant().Trim());
        }

        internal string GetPort()
        {
            return _serverVariables["SERVER_PORT"];
        }

        internal string GetDomain()
        {
            return _serverVariables["SERVER_NAME"];
        }

        internal string GetProtocol()
        {
            return (_serverVariables["SERVER_PORT_SECURE"] == "1") ? "https://" : "http://";
        }
    }
}