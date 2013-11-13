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

        protected virtual string ModuleName { get { return string.Empty; } }

        protected virtual string ModuleNameSpace { get { return string.Empty; } }


        protected void RegisterController(string controllerName)
        {
            if(string.IsNullOrEmpty(controllerName))
                return;
            _controllers.Add(controllerName);
        }                

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (!string.IsNullOrWhiteSpace(this.ModuleName) && !string.IsNullOrWhiteSpace(this.ModuleNameSpace))
            {
                context.MapRoute(
                    AreaName + "_" + ModuleName,
                    AreaName + "/" + ModuleName + "/{controller}/{action}/{id}/{uplineId}/{groupName}",
                    new { action = "Index", id = UrlParameter.Optional, uplineId = UrlParameter.Optional, groupName = UrlParameter.Optional },
                    new[] { ModuleNameSpace });
            }
        }

        protected void RegisterController(AreaRegistrationContext context)
        {
        }
    }
}