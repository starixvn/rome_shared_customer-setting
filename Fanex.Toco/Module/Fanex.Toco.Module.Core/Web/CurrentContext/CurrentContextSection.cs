// --------------------------------------------------------------------------
// <copyright file="CurrentContextSection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// <summary>
// For current session info
// </summary>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.10.11">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Web
{
    using System;
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CurrentContextSection : ConfigurationSection
    {
        private static CurrentContextSection _instance;

        [ConfigurationProperty("contextFactory", IsRequired = false)]
        public string ContextFactory
        {
            get
            {
                return (string)base["contextFactory"];
            }
        }

        public static CurrentContextSection Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = (CurrentContextSection)ConfigurationManager.GetSection("CurrentContext");
                    }
                    catch (Exception e)
                    {
                        throw new ConfigurationErrorsException("Error when parsing configuration section CurrentContext.", e);
                    }
                }

                return _instance;
            }
        }
    }
}
