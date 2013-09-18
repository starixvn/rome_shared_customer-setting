// ************************************************************************************
// <copyright file="ModuleRegistrator.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Andy.Nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

/// <summary>
/// The App_Start namespace.
/// </summary>
namespace Fanex.CustomerSetting.Module.LeftPanel.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.App_Start;

    /// <summary>
    /// Class Module Registration.
    /// </summary>
    public class ModuleRegistrator : AppAreaRegistrator 
    {
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <value>The name of the controller.</value>
        protected override string ControllerName
        {
            get
            {
                return "BetSetting";
            }
        }

        /// <summary>
        /// Gets the controller name space.
        /// </summary>
        /// <value>The controller name space.</value>
        protected override string ControllerNameSpace
        {
            get
            {
                var controllerNameSpace = Assembly.GetExecutingAssembly().GetName().Name + ".Controllers";
                return controllerNameSpace;
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_" + this.ControllerName,
                this.AreaName + "/" + this.ControllerName + "/{controller}/{action}/{id}/{uplineId}/{userId}/{siteName}",
                new 
                { 
                    action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional, 
                    userId = UrlParameter.Optional, siteName = UrlParameter.Optional 
                },
                new[] { this.ControllerNameSpace });
        }       
    }
}