// --------------------------------------------------------------------------
// <copyright file="Message.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Models
{
    /// <summary>
    /// The message class.
    /// </summary>
    public class Message
    {
        private string _content_ch = string.Empty;
        private string _content_cs = string.Empty;
        private string _content_en = string.Empty;
        private string _content_es = string.Empty;
        private string _content_it = string.Empty;
        private string _content_jp = string.Empty;
        private string _content_ko = string.Empty;
        private string _content_th = string.Empty;
        private string _creatorName = string.Empty;
        private string _userName = string.Empty;
     
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets the English Message Content.
        /// </summary>
        /// <value>
        /// The English Message Content.
        /// </value>
        public string Content_en
        {
            get { return _content_en; }
            set { _content_en = value; }
        }

        /// <summary>
        /// Gets or sets the Traditional Chinese Message Content.
        /// </summary>
        /// <value>
        /// The Traditional Chinese Message Content.
        /// </value>
        public string Content_ch
        {
            get { return _content_ch; }
            set { _content_ch = value; }
        }

        /// <summary>
        /// Gets or sets the Simple Chinese Message Content.
        /// </summary>
        /// <value>
        /// The Simple Chinese Message Content.
        /// </value>
        public string Content_cs
        {
            get { return _content_cs; }
            set { _content_cs = value; }
        }

        /// <summary>
        /// Gets or sets the Japanese Message Content.
        /// </summary>
        /// <value>
        /// The Japanese Message Content.
        /// </value>
        public string Content_jp
        {
            get { return _content_jp; }
            set { _content_jp = value; }
        }

        /// <summary>
        /// Gets or sets the Italian Message Content.
        /// </summary>
        /// <value>
        /// The Italian Message Content.
        /// </value>
        public string Content_it
        {
            get { return _content_it; }
            set { _content_it = value; }
        }

        /// <summary>
        /// Gets or sets the Korean Message Content.
        /// </summary>
        /// <value>
        /// The Korean Message Content.
        /// </value>
        public string Content_ko
        {
            get { return _content_ko; }
            set { _content_ko = value; }
        }

        /// <summary>
        /// Gets or sets the Thai Message Content.
        /// </summary>
        /// <value>
        /// The Thai Message Content.
        /// </value>
        public string Content_th
        {
            get { return _content_th; }
            set { _content_th = value; }
        }

        /// <summary>
        /// Gets or sets the Spanish Message Content.
        /// </summary>
        /// <value>
        /// The Spanish Message Content.
        /// </value>
        public string Content_es
        {
            get { return _content_es; }
            set { _content_es = value; }
        }

        public int MessageType { get; set; }

        public int MsgType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is special Message or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is sticky]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSticky { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName
        {
            get { return _creatorName; }
            set { _creatorName = value; }
        }
    }
}
