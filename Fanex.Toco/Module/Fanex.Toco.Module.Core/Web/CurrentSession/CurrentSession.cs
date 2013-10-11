// --------------------------------------------------------------------------
// <copyright file="CurrentSession.cs" company="Nexcel Solutions Vietnam">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CurrentSession : ICurrentSession
    {
        public string UserName
        {
            get;
            set;
        }

        public int CustId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public int GroupId
        {
            get;
            set;
        }

        public bool IsLogin
        {
            get;
            set;
        }

        public int OnUserId
        {
            get;
            set;
        }

        public string NetSessionId
        {
            get;
            set;
        }

        public string Captcha
        {
            get;
            set;
        }

        public string CurrencyGroup
        {
            get;
            set;
        }

        public bool IsInternalAdmin
        {
            get;
            set;
        }

        public bool IsITAdmin
        {
            get;
            set;
        }
    }
}