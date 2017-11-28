namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IColossusBetAuthorizationService
    {
        bool IsAuthorizedRead(AuthorizationContext authorizationContext);
        bool IsAuthorizedReadMultiple(AuthorizationContext authorizationContext, IEnumerable<string> usernames);
        bool IsAuthorizedHost(AuthorizationContext authorizationContext);
        bool IsAuthorizedHostMultiple(AuthorizationContext authorizationContext, IEnumerable<string> usernames);
    }
}