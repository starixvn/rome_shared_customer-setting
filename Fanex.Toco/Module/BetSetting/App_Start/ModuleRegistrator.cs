using System.Reflection;
using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Web.App_Start;


namespace Fanex.CustomerSetting.Module.LeftPanel.App_Start
{
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
                AreaName + "_" + ControllerName,
                AreaName + "/" + ControllerName + "/{controller}/{action}/{id}/{uplineId}/{userId}/{siteName}",
                new { action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional, userId = UrlParameter.Optional, siteName = UrlParameter.Optional },
                new[] { ControllerNameSpace });
        }       
    }
}