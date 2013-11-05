// --------------------------------------------------------------------------
// <copyright file="IbcIPService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.11.5">Created</change>
// </history>

namespace Fanex.ExternalAccount.IP.Geographic
{
    using System.Configuration;
    using System.IO;
    using System.Xml.Linq;
    using Fanex.ExternalAccount.IP.DAL;

    /// <summary>
    /// IP Service class.
    /// </summary>
    internal class IbcIPService : GeoIP
    {
        private string _urlService = ConfigurationManager.AppSettings["ipservice"];

        /// <summary>
        /// Get IPTable info.
        /// </summary>
        /// <param name="ip">IP address.</param>
        /// <returns>IPTable object.</returns>
        public override IPTable LocationFromIP(string ip)
        {
            string responseContent = ReadUrl(string.Format("{0}?ip={1}", _urlService, ip));
            StringReader sr = new StringReader(responseContent);
            XElement respElement = XElement.Load(sr);

            return new IPTable
            {
                CountryShort = (string)respElement.Element("CountryCode"),
                CountryLong = (string)respElement.Element("CountryName"),
                City = (string)respElement.Element("City"),
                Region = (string)respElement.Element("RegionName"),
                FlagPath = (string)respElement.Element("FlagPath")
            };
        }
    }
}
