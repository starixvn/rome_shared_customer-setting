// ************************************************************************************
// <copyright file="Sport.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System.Collections.Generic;

namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    /// <summary>
    /// Class Sport.
    /// </summary>
    public class Sport
    {
        public string Name { get; set; }

        public bool Visible { get; set; }

        public int Id { get; set; }

        public List<Tote> Totes { get; set; }
    }
}