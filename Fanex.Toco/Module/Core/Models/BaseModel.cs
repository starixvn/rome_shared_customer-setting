// --------------------------------------------------------------------------
// <copyright file="BaseModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Models
{
    using System;

    /// <summary>
    /// The base model.
    /// </summary>
    [Serializable]
    public class BaseModel
    {
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
