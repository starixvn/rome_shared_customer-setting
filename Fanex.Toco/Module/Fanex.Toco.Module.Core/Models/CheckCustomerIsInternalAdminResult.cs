using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.CustomerSetting.Module.Core.Models
{
    public class CheckCustomerIsInternalAdminResult
    {
        public bool IsInternalAdmin { get; private set; }

        public bool IsAllowAccess { get; private set; }

        public CheckCustomerIsInternalAdminResult(bool isInternalAdmin, bool isAllowAccess)
        {
            IsInternalAdmin = isInternalAdmin;
            IsAllowAccess = isAllowAccess;
        }
    }
}
