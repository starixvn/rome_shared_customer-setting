namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Models
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusModel
    {
        public bool ReadOnly { get; set; }

        public string IframeId { get; set; }

        public string Username { get; set; }

        public bool HasCheckAll { get; set; }

        public bool ReloadAfterUpdate { get; set; }

        public string Warning { get; set; }

        public string ClosePopupFunction { get; set; }

        public RequestContext RequestContext { get; set; }

        public IList<EnableStatusEntity> Entities { get; set; }
    }
}