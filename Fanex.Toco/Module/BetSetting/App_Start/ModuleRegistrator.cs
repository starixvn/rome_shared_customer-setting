// --------------------------------------------------------------------------
// <copyright file="ModuleRegistrator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.9.12">StyleCop</change>
// </history>

namespace Fanex.CustomerSetting.Module.LeftPanel.App_Start
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
                return "BetSetting";
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
            context.MapRoute(
                this.AreaName + "_" + this.ControllerName,
                this.AreaName + "/" + this.ControllerName + "/{controller}/{action}/{id}/{uplineId}/{userId}/{siteName}",
                new { action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional, userId = UrlParameter.Optional, siteName = UrlParameter.Optional },
                new[] { this.ControllerNameSpace });
        }       
    }
}