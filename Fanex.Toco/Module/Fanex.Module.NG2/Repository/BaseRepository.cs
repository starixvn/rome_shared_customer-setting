using System;
using System.Collections;
using Fanex.Data;
using Fanex.Logging.Extensions.Web;

namespace Fanex.Module.NG2.Repository
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseRepository
    {
        public static bool CheckAllowedCurrencyAll(int custId)
        {
            var isAllowed = false;

            try
            {
                var param = new Hashtable();
                param.Add("custid", custId);

                using (IObjectDb db = new ObjectDb("CheckAllowedShow", 600))
                {
                    db.AssignParamValues(param);
                    db.ExecuteDataSet();
                    var output = db.GetOuputParams();

                    isAllowed = Convert.ToBoolean(output["allowedng2"]);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return isAllowed;
        }
    }
}