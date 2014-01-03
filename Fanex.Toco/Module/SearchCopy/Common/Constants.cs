// --------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>
namespace Fanex.CustomerSetting.Module.SearchCopy.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Class CopyMessage.
        /// </summary>
        public static class CopyMessage
        {
            public const string Success = "Created Super Successfully !";
            public const string Failed = "Unable to copy the Super, please try again !";
        }

        /// <summary>
        /// Class FailedSyncMessage.
        /// </summary>
        public static class FailedSyncMessage
        {
            public const string Casino = "RNG Casino failed to synchronize !";
            public const string Bingo = "Bingo failed to synchronize !";
            public const string LiveCasino = "Live Casino failed to synchronize !";
        }
    }
}