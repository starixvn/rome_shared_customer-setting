// --------------------------------------------------------------------------
// <copyright file="Utility.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.11.5">Created</change>
// </history>

namespace Fanex.ExternalAccount.IP
{
    using System;
    using System.Configuration;
    using System.Data.EntityClient;
    using System.Linq;
    using Fanex.Caching;
    using Fanex.Common;
    
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Gets encoded connection string.
        /// </summary>
        /// <returns>System.String object.</returns>
        public static string GetEFConnectionString()
        {
            ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
            string str = cache.Get<string>("IPEntities_ConnectionString");
            if (string.IsNullOrEmpty(str))
            {
                var cnn = ConfigurationManager.ConnectionStrings["IPEntities"].ConnectionString;
                var pwds = cnn.Split(';');
                var pwd = string.Empty;
                var pwdPhrase = string.Empty;

                for (int i = 0; i < pwds.Count(); i++)
                {
                    if (pwds[i].Contains("password"))
                    {
                        pwdPhrase = pwds[i];
                        pwd = pwds[i].Split('=').ElementAt(1);
                        break;
                    }
                }
                
                cnn = cnn.Replace(pwdPhrase, "password=" + Cryptography.CfsDeCrypt(pwd));
                str = new EntityConnectionStringBuilder(cnn).ToString();
                cache.Set<string>("IPEntities_ConnectionString", str, TimeSpan.FromHours(1.0));
            }

            return str;
        }
    }
}
