using Fanex.HttpModule.NG2.UM.Configurations;
using System;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Fanex.HttpModule.NG2.UM
{
    public class NG2HttpModule : IHttpModule
    {
        public NG2HttpModule()
        {
        }

        public String ModuleName
        {
            get { return "NG2HttpModule"; }
        }

        // In the Init function, register for HttpApplication 
        // events by adding your handlers.
        public void Init(HttpApplication application)
        {
            application.AcquireRequestState +=
                (new EventHandler(this.Application_AcquireRequestState));

        }

        private void Application_AcquireRequestState(Object source,
             EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            bool isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var NG2Section = NumberGame2Section.Instance;
            var url = context.Request.AppRelativeCurrentExecutionFilePath.TrimStart(new Char[] { '~', '/' });
            url = url ?? string.Empty;
            var isNG2RequireUM = (from NumberGame2Element ele in NG2Section.WebUrl
                                      where ele.Url.ToLower().Contains(url.ToLower())
                                  select ele).ToList();
            isNG2RequireUM = isNG2RequireUM.Where(ele => ele.Url.Contains(url)).ToList();
            var UMService = new ONEbook.UM.ThirdParty.Services.Impl.UMService();
            if (!string.IsNullOrEmpty(url) && isNG2RequireUM.Count > 0 && UMService.GetUnderMaintenanceTime(23).IsUnderMaintenanceTime)
            {
                if (isAjaxRequest)
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    string result = "{\"ErrorCode\":\"123\", \"ErrorMessage\":\"Special Site UM\"}";
                    context.Response.Write(result);
                    context.Response.End();
                }
                else
                {
                    context.Response.Redirect(NG2Section.UMPageUrl);
                }
            }

        }

        public void Dispose()
        {
        }
    }
}
