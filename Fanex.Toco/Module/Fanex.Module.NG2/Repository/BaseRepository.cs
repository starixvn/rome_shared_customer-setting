using System;
using System.Collections;
using Fanex.Data;
using Fanex.Data.DNAide;
using Fanex.Logging.Extensions.Web;

namespace Fanex.Module.NG2.Repository
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseRepository
    {
        public static DbResult CheckAllowedCurrencyAll(int custId, byte settingStatus = 0)
        {
            DbResult checkCurrency = new DbResult(new Hashtable(), new System.Data.DataSet());
            try
            {
                var param = new Hashtable();
                param.Add("custid", custId);
                param.Add("settingstatus", settingStatus);

                using (ObjectDb db = new ObjectDb("CheckAllowedShow", 600))
                {
                    db.AssignParamValues(param);

                    var ds = db.ExecuteDataSet();
                    var output = db.GetOuputParams();

                    checkCurrency = new DbResult(output, ds);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

            return checkCurrency;
        }
    }
}