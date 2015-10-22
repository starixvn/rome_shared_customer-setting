// --------------------------------------------------------------------------
// <copyright file="MessageElement.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.10.21">Created</change>
// </history>

namespace Fanex.Module.Logs.Mappings
{
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageElement : ConfigurationElement 
    {
        [ConfigurationProperty("text", IsRequired = true)]
        public string Text
        {
            get
            {
                return base["text"] as string;
            }
        }
    }
}
