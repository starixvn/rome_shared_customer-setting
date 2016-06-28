// --------------------------------------------------------------------------
// <copyright file="CustomerInformationModel.cs" company="">
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
    public class CustomerInformationModel
    {
        public int RoleId { get; set; }

        public int CustomerId { get; set; }
    }
}
