using Fanex.PTRacing.BusinessObjects;
using System.Collections;
using RequestContext = Fanex.PTRacing.BusinessObjects.RequestContext;
namespace Fanex.CustomerSetting.Module.Racebook.Services
{
    public interface IRacingSettingService
    {
        ICustomer GetCustomerInfo(IRequestContext requestContext);
        Result SaveCustomerSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, RequestContext currentContext, Hashtable otherSettings);
    }
}
