using Fanex.PTRacing.BusinessObjects;
using System.Collections;

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    public interface IRacingSettingService
    {
        ICustomer GetCustomerInfo(IRequestContext requestContext);
        Result SaveCustomerSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, RequestContext currentContext, Hashtable otherSettings);
    }
}
