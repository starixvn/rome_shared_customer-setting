// ************************************************************************************
// <copyright file="Bettype.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Entities
{
    /// <summary>
    /// Class bet type.
    /// </summary>
    public class Bettype
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public Data PositionTaking { get; set; }
    }
}