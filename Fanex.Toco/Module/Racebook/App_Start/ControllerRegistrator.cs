using System.Reflection;
using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Web.App_Start;

namespace Fanex.CustomerSetting.Module.Racebook.App_Start
{
    public class ControllerRegistrator : AppAreaRegistrator 
    {

        protected override string ControllerName
        {
            get
            {
                return "Racebook";
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
            RegisterController(context);
            base.RegisterArea(context);
        }
       
    }
}