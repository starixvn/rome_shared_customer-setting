// --------------------------------------------------------------------------
// <copyright file="RequestContext.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.28">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Entities
{
    using Fanex.CustomerSetting.Module.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RequestContext
    {
        public bool NewMode { get; set; } = false;
        public int Product
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public bool Synced
        {
            get;
            set;
        }

        public bool HideDownline
        {
            get;
            set;
        }

        public bool Enable
        {
            get;
            set;
        }

        public bool EnableUplines
        {
            get;
            set;
        }

        public bool EnableDownlines
        {
            get;
            set;
        }

        public string Ip
        {
            get
            {
                return Utils.GetClientIP();
            }
        }

        public int CustId
        {
            get;
            set;
        }

        public int UplineId
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string ProductId
        {
            get;
            set;
        }

        public int SubAccId
        {
            get;
            set;
        }

        public string Language { get; set; } = "en-US";
        public bool HasDownline
        {
            get;
            set;
        }

        public EnableStatusEnum.Level Level
        {
            get;
            set;
        }

        public bool IsCasino { get; set; }

        public int SettingType { get; set; }
    }
}
