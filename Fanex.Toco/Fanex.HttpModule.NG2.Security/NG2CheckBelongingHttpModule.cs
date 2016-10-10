using Fanex.Common;
using Fanex.HttpModule.NG2.Security.Configurations;
using Fanex.HttpModule.NG2.Security.Configurations.Authentication;
using Fanex.HttpModule.NG2.Security.Services;
using Sunplus.Agent.Model.BL;
using Sunplus.Agent.Model.Entities;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Fanex.HttpModule.NG2.Security
{
    public class NG2CheckBelongingHttpModule : IHttpModule
    {
        private string rngArea = "CustomerSetting/Casino";

        private NG2SecurityService _ng2SecurityService;


        public NG2CheckBelongingHttpModule()
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

            var ng2SecuritySection = AuthenticationSection.Instance;
            _ng2SecurityService = new NG2SecurityService();
            int custId = GetCustomerIdFromRequest();
            long transId = GetTransIdFromRequest(context.Request);
            bool isDataWarehouse = (context.Request["iswhcheck"] == "1");
            var currentUrl = GetRequestPath();

            if (_ng2SecurityService.IsLoginAccClosed())
            {
                context.Session.Clear();
                //context.Response.Redirect(ng2SecuritySection.LoginUrl, true);
                return;
            }

            if (!_ng2SecurityService.IsBelonging(custId, transId, isDataWarehouse, currentUrl))
            {
                context.Session.Abandon();
                //context.Response.Redirect(ng2SecuritySection.LoginUrl, true);
                return;
            }
        }

        public void Dispose()
        {
        }

        private bool IsStaticContentRequest(HttpContext context)
        {
            return context.Session == null;
        }

        private string GetRequestPath()
        {
            var url = HttpContext.Current.Request.Path;
            if (IsRnGCasinoUri)
            {
                url = GetRnGCasinoUri();
            }

            url = url.Remove(0, 1).ToLower();

            return GetUrlAccessByCustomerWithoutDirectoryPath(url);
        }

        private string GetUrlAccessByCustomerWithoutDirectoryPath(string url)
        {
            var virtualDirectory = WebAppSettings.VirtualDirectory;
            if (!string.IsNullOrWhiteSpace(virtualDirectory))
            {
                var regex = new Regex(string.Format("{0}/", virtualDirectory), RegexOptions.IgnoreCase);
                var urlWithOutVirtualDirectory = regex.Replace(url, string.Empty);

                return urlWithOutVirtualDirectory;
            }

            return url;
        }

        private string GetRnGCasinoUri()
        {
            var controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];
            var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"];

            var uri = string.Format("/{0}/{1}/{2}", rngArea, controller, action);

            return uri;
        }

        private bool IsRnGCasinoUri
        {
            get
            {
                var url = HttpContext.Current.Request.Path.ToString();
                if (url.IndexOf(rngArea, StringComparison.OrdinalIgnoreCase) != -1) { return true; }

                return false;
            }
        }

        private int GetCustomerIdFromRequest()
        {
            var custId = 0;
            var trackingValue = string.Empty;
            if (HttpContext.Current.Request.Form["model"] != null)
            {
                trackingValue = HttpContext.Current.Request.Form["model"];
            }
            else
            {
                var bytes = new byte[HttpContext.Current.Request.InputStream.Length];
                HttpContext.Current.Request.InputStream.Read(bytes, 0, bytes.Length);
                HttpContext.Current.Request.InputStream.Position = 0;
                trackingValue = Encoding.ASCII.GetString(bytes);
            }

            var belongingParams = WebAppSettings.BelongingParams;
            var paramArray = belongingParams.Split(',');
            foreach (string paramRequest in paramArray)
            {
                if (string.IsNullOrWhiteSpace(paramRequest))
                {
                    continue;
                }

                var currentParamName = Regex.Replace(paramRequest, @"^\s+", string.Empty);
                if (!string.IsNullOrWhiteSpace(trackingValue))
                {
                    var pattern = "(\"" + currentParamName + "\":)([^\r\n][0-9]+)";
                    Match regexMatch = Regex.Match(trackingValue, pattern, RegexOptions.IgnoreCase);
                    if (regexMatch.Success)
                    {
                        var custidFounded = Regex.Replace(regexMatch.Groups[2].ToString(), @"[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);

                        custId = Converter.ToInt(custidFounded);
                    }

                    if (custId > 0)
                    {
                        break;
                    }
                }

                custId = Converter.ToInt(HttpContext.Current.Request[currentParamName]);
                if (custId > 0)
                {
                    break;
                }

                custId = Converter.ToInt(HttpContext.Current.Request.Form[currentParamName]);
                if (custId > 0)
                {
                    break;
                }
            }

            return custId;
        }

        private long GetTransIdFromRequest(HttpRequest request)
        {
            long transId;
            long.TryParse(request["transid"], out transId);
            if (transId == 0)
            {
                long.TryParse(request["refno"], out transId);
            }

            return transId;
        }
    }
}
