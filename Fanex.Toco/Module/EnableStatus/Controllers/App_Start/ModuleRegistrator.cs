// --------------------------------------------------------------------------
// <copyright file="ModuleRegistrator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created.</change>
//   <change who="Andy.Nguyen" date="2016.2.2">Document class.</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Controllers.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.App_Start;

    /// <summary>
    /// Registers module Enable Status.
    /// </summary>
    public class ModuleRegistrator : AppAreaRegistrator 
    {
        /// <summary>
        /// Gets module name.
        /// </summary>
        protected override string ModuleName
        {
            get
            {
                return "EnableStatus";
            }
        }

        /// <summary>
        /// Gets module name space.
        /// </summary>
        protected override string ModuleNameSpace
        {
            get
            {
                var controllerNameSpace = Assembly.GetExecutingAssembly().GetName().Name + ".Controllers";
                return controllerNameSpace;
            }
        }

        /// <summary>
        /// Registers enable status areas.
        /// </summary>
        /// <param name="context">Area registration context.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            base.RegisterArea(context);
        }       
    }
}