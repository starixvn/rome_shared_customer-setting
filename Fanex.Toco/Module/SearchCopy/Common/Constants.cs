using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.Module.SearchCopy.Common
{
    public static class Constants
    {
        public static class FailedCopyMessage
        {
            public const string Casino = "Copying failed due to RNG Casino product. Please try again!";
            public const string Bingo = "Copying failed due to Bingo product. Please try again!";
            public const string LiveCasino = "Copying failed due to Live Casino product. Please try again!";
        }
    }
}