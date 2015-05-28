// --------------------------------------------------------------------------
// <copyright file="RequestContext.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
        public string IP
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

        public EnableStatusEnum.Level Level
        {
            get;
            set;
        }
    }
}
