using System.Web.Mvc;
using System.Collections.Generic;

namespace Fanex.CustomerSetting.Module.Core.Web.App_Start
{
    public class AppAreaRegistrator : AreaRegistration
    {
        private List<string> _controllers = new List<string>();

        public override string AreaName
        {
            get
            {
                return "CustomerSetting";
            }
        }

        protected virtual string ControllerName { get { return string.Empty; } }

        protected virtual string ControllerNameSpace { get { return string.Empty; } }


        protected void RegisterController(string controllerName)
        {
            if(string.IsNullOrEmpty(controllerName))
                return;
            _controllers.Add(controllerName);
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterController();
        }

        protected void RegisterController(AreaRegistrationContext context)
        {
            context.MapRoute(
                AreaName + "_" + ControllerName,
                AreaName + "/" + ControllerName + "/{controller}/{action}/{id}/{uplineId}",
                new { action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional },
                new[] { ControllerNameSpace });
        }
    }
}