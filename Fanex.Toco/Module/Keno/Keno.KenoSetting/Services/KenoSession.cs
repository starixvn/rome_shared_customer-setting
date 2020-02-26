// --------------------------------------------------------------------------
// <copyright file="KenoSession.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Configurations;
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
                return AppSession.AccId;
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
                return AppSession.CurrentSession.Language;
            }
        }

        public bool IsLocalTest
        {
            get
            {
                return AppSettings.IsLocalTest;
            }
        }

        public bool IsBelongToSpecialGroups
            => AppSession.CurrentSession.IsBelongToSpecialGroups;
    }
}