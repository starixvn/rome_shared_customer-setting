namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public interface IKenoAuthorizationService
    {
        bool IsAuthorizedRead(AuthorizationContext authorizationContext);
        bool IsAuthorizedReadMultiple(AuthorizationContext authorizationContext, IEnumerable<string> usernames);
        bool IsAuthorizedHost(AuthorizationContext authorizationContext);
        bool IsAuthorizedHostMultiple(AuthorizationContext authorizationContext, IEnumerable<string> usernames);
    }
}