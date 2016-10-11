using Fanex.HttpModule.NG2.UM.Configurations;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
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
            if (IsStaticContentRequest(context))
            {
                return;
            }

            var NG2Section = NumberGame2Section.Instance;
            
            //var url = context.Request.AppRelativeCurrentExecutionFilePath.TrimStart(new Char[] { '~', '/' });
            var url = context.Request.RawUrl;
            url = url ?? string.Empty;
            if (context.Request.HttpMethod == WebRequestMethods.Http.Post)
            {
                url += "?" + HttpUtility.HtmlDecode(context.Request.Params.ToString());
            }

            var matchingUrlList = (from NumberGame2Element ele in NG2Section.WebUrl
                                  where url.ToLower().Contains(ele.Url.ToLower())
                                  select ele).ToList();
            var UMService = new ONEbook.UM.ThirdParty.Services.Impl.UMService();
            if (!string.IsNullOrEmpty(url) && matchingUrlList.Count > 0 && UMService.GetUnderMaintenanceTime(23).IsUnderMaintenanceTime)
            {
                bool isAjaxRequest = matchingUrlList[0].IsAjax || context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjaxRequest)
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    context.Response.StatusCode = 302;
                    context.Response.RedirectLocation = context.Request.ApplicationPath + NG2Section.UMPageUrl.Replace("~", string.Empty);
                    string result = "{\"ErrorCode\":\"5003\", \"ErrorMessage\":\"Special Site UM\"}";
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

        private bool IsStaticContentRequest(HttpContext context)
        {
            return context.Session == null;
        }
    }
}
