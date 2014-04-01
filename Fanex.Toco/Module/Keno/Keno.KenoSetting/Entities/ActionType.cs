// --------------------------------------------------------------------------
// <copyright file="ActionType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
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
        EditSuper = 4,

        /// <summary>
        /// Edit master action.
        /// </summary>
        EditMaster = 3,

        /// <summary>
        /// Edit agent action.
        /// </summary>
        EditAgent = 2,

        /// <summary>
        /// Agent edit member action.
        /// </summary>
        AgentEditMember = 21,

        /// <summary>
        /// Super edit member action.
        /// </summary>
        SuperEditMember = 41,

        /// <summary>
        /// Master edit member action.
        /// </summary>
        MasterEditMember = 31
    }
}