// --------------------------------------------------------------------------
// <copyright file="ModuleRegistrator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.9.12">StyleCop</change>
// </history>

namespace Fanex.CustomerSetting.Security.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.App_Start;

    public class ModuleRegistrator : AppAreaRegistrator 
    {
        protected override string ControllerName
        {
            get
            {
                return "Security";
            }
        }

        protected override string ControllerNameSpace
        {
            get
            {
                var controllerNameSpace = Assembly.GetExecutingAssembly().GetName().Name + ".Controllers";
                return controllerNameSpace;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterController(context);
            this.RegisterArea(context);
        }       
    }
}