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
        public int CustId { get; set; }

        public int UplineId { get; set; }

        public int UserId { get; set; }

        public EnableStatusEnum.Level Level { get; set; }

        public IList<EnableStatusEntity> Entities { get; set; }        
    }
}
