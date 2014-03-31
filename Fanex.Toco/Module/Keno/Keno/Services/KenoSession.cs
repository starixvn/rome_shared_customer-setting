// --------------------------------------------------------------------------
// <copyright file="KenoSession.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Services
{
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Web;

    /// <summary>
    /// Bound the app session from core.
    /// </summary>
    public class KenoSession : IKenoSession
    {
        public int UserId
        {
            get
            {
                ////return AppSession.AccId;
                return 8;
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
    }
}