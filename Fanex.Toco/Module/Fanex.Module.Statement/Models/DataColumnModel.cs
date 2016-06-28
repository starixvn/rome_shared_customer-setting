// --------------------------------------------------------------------------
// <copyright file="DataColumnModel.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Models
{
    using System;

    /// <summary>
    /// Data Column Model.
    /// </summary>
    [Serializable]
    public class DataColumnModel
    {
        public bool HasData { get; set; }

        public object Value { get; set; }
    }
}
