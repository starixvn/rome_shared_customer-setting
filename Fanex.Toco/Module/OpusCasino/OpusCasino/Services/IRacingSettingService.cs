using Fanex.PTRacing.BusinessObjects;
using System.Collections;

namespace Fanex.CustomerSetting.OpusCasino.Services
{
    public interface IRacingSettingService
    {
        ICustomer GetCustomerInfo(IRequestContext requestContext);
        Result SaveCustomerSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, RequestContext currentContext, Hashtable otherSettings);
    }
}
