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

        public string Site { get; private set; }

        public CheckCustomerBelongToSpecialSiteResult(bool isSpecialSite, bool isSpecialHeadline, string site)
        {
            IsSpecialSite = isSpecialSite;
            IsSpecialHeadline = isSpecialHeadline;
            Site = site;
        }
    }
}
