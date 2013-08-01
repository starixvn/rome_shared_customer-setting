// -----------------------------------------------------------------------
// <copyright file="ModuleRegistrator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  Module registrator
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.App_Start;

    /// <summary>
    /// Module registration.
    /// </summary>
    public class ModuleRegistrator : AppAreaRegistrator 
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        /// <value>The controller name.</value>
        protected override string ControllerName
        {
            get
            {
                return "ProgressiveBonus";
            }
        }

        /// <summary>
        /// Controller name space.
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
        /// Register area.
        /// </summary>
        /// <param name="context">Area registration context.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterController(context);
            base.RegisterArea(context);
        }       
    }
}