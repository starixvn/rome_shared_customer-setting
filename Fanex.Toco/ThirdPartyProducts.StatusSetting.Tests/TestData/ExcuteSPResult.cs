using System.Collections;

namespace ThirdPartyProducts.StatusSetting.Test.TestData
{
    public static class ExcuteSPResult
    {
        private static Hashtable success = new Hashtable
        {
            ["errcode"] = "0",
            ["errmsg"] = string.Empty
        };

        public static Hashtable Success
        {
            get
            {
                return success;
            }
        }
    }
}
