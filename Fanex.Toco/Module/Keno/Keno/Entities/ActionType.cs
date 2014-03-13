// --------------------------------------------------------------------------
// <copyright file="ActionType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    public enum ActionType : int
    {
        /// <summary>
        /// None action.
        /// </summary>
        None = 0,

        /// <summary>
        /// Edit super action.
        /// </summary>
        EditSuper = 1,

        /// <summary>
        /// Edit master action.
        /// </summary>
        EditMaster = 2,

        /// <summary>
        /// Edit agent action.
        /// </summary>
        EditAgent = 3,

        /// <summary>
        /// Agent edit member action.
        /// </summary>
        AgentEditMember = 4,

        /// <summary>
        /// Super edit member action.
        /// </summary>
        SuperEditMember = 5,

        /// <summary>
        /// Master edit member action.
        /// </summary>
        MasterEditMember = 6
    }
}