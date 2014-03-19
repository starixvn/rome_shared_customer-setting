// --------------------------------------------------------------------------
// <copyright file="MailItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Models
{
    using System.Collections.Generic;
    using System.Net.Mail;

    /// <summary>
    /// Mail Item.
    /// </summary>
    public class MailItem
    {
        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IList<MailAttachment> Attachments { get; set; }

        public AlternateView AltView { get; set; }
    }
}