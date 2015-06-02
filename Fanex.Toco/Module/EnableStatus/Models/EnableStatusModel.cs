// --------------------------------------------------------------------------
// <copyright file="EnableStatusModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Models
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusModel
    {
        public string Title { get; set; }

        public bool HasCheckAll { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public bool ShowInFrame { get; set; }

        public string Warning { get; set; }

        public RequestContext RequestContext { get; set; }

        public IList<EnableStatusEntity> Entities { get; set; }        
    }
}
