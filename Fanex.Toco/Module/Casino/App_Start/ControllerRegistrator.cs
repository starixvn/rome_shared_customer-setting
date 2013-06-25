using System.Reflection;
using System.Web.Mvc;
using Fanex.Toco.Module.Core.Web.App_Start;

namespace Fanex.Toco.Module.Casino.App_Start
{
    public class ControllerRegistrator : AppAreaRegistrator 
    {

        protected override string ControllerName
        {
            get
            {
                return "Casino";
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