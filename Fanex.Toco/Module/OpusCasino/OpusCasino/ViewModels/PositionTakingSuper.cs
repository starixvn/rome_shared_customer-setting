// ************************************************************************************
// <copyright file="PositionTakingSuper.cs" company="Nexcel Solutions Vietnam">
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
    public class PositionTakingSuper : PositionTaking
    {               
        public PositionTakingSuper()
        {
            this.IsUpdateCom4 = false;
            this.IsUpdateS4 = false;
            this.IsUpdateN4 = false;
        }

        public bool IsUpdatePT { get; set; }

        public bool IsUpdateS4 { get; set; }

        public bool IsUpdateN4 { get; set; }

        public bool IsUpdateCom4 { get; set; }
    }
}