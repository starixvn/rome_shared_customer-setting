// ************************************************************************************
// <copyright file="PositionTakingMaster.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Thai.Tran">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.OpusCasino.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// PositionTakingSuper Class.
    /// </summary>
    public class PositionTakingMaster : PositionTaking
    {
        public PositionTakingMaster()
        {
            this.IsUpdatePT = false;
            this.IsUpdateSuper = false;
            this.IsUpdateAutoSuper = false;
            this.IsUpdateCom3 = false;
            this.IsUpdateAllMember = false;
            this.IsUpdateN3 = false;
        }

        public bool IsUpdatePT { get; set; }

        public bool IsUpdateSuper { get; set; }

        public bool IsUpdateAutoSuper { get; set; }

        public bool IsUpdateN3 { get; set; }

        public bool IsUpdateCom3 { get; set; }

        public bool IsUpdateAllMember { get; set; }

        public double NewsPT { get; set; }
    }
}