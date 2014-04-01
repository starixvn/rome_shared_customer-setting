// --------------------------------------------------------------------------
// <copyright file="ModuleRegistrator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.App_Start
{
    using System.Reflection;
    using Fanex.CustomerSetting.Module.Core.Web.App_Start;

    /// <summary>
    /// Register racing module.
    /// </summary>
    public class ModuleRegistrator : AppAreaRegistrator
    {
        public const string Module = "KenoSetting";

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <value>The name of the controller.</value>
        protected override string ModuleName
        {
            get
            {
                return Module;
            }
        }

        /// <summary>
        /// Gets the controller name space.
        /// </summary>
        /// <value>The controller name space.</value>
        protected override string ModuleNameSpace
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
        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            base.RegisterArea(context);
            BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
        }
    }
}