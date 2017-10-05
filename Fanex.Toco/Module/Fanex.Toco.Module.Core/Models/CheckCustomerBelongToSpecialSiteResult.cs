using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.CustomerSetting.Module.Core.Models
{
   public class CheckCustomerBelongToSpecialSiteResult
    {

        public bool IsSpecialSite { get; private set; }

        public bool IsSpecialHeadline { get; private set; }

        public int SiteCode { get; private set; }

        public CheckCustomerBelongToSpecialSiteResult(bool isSpecialSite, bool isSpecialHeadline, int siteCode)
        {
            IsSpecialSite = isSpecialSite;
            IsSpecialHeadline = isSpecialHeadline;
            SiteCode = siteCode;
        }
    }
}
