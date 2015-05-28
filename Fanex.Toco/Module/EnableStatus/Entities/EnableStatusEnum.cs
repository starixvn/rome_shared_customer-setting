// --------------------------------------------------------------------------
// <copyright file="EnableStatusEnum.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Entities
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusEnum
    {
        public enum Level : int
        {
            Admin = 5,
            Super = 4,
            Master = 3,
            Agent = 2,
            Member = 1
        }
    }
}
