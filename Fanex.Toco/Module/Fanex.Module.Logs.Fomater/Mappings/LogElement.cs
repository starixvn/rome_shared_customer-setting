// --------------------------------------------------------------------------
// <copyright file="LogElement.cs" company="">
// Copyright (c). All rights reserved.
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
    public class LogElement : ConfigurationElement 
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public Formater.LogType Type
        {
            get
            {
                return (Formater.LogType)base["type"];
            }
        }

        [ConfigurationProperty("Messages", IsRequired = false)]
        public MessageCollection Messages
        {
            get
            {
                return base["Messages"] as MessageCollection;
            }
        }
    }
}