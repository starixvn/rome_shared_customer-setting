using System.Collections.Generic;
using System.Collections;
using System.Data;
using Fanex.CustomerSetting.OpusCasino.Entities;
using Fanex.Data;
using Fanex.Data.DNAide;

namespace Fanex.CustomerSetting.OpusCasino.Repositories
{
    public class RacingDAO : IRacingDAO
    {
        public DataTable GetRacingStatus(int custId)
        {
            var param = new Hashtable { { "custid", custId } };

            using (var objDb = new ObjectDb("GetRacingStatus"))
            {
                var dsResult = objDb.ExecuteDataSet(param);
                if (dsResult == null || dsResult.Tables.Count == 0)
                    return null;
                return dsResult.Tables[0];
            }

        }
        public DataTable GetRacingSetting(int custId, int roleId)
        {
            return null;

        }

        public Result UpdateRacingStatus(Hashtable settingParams)
        {
            var result = new Result();
            using (var db = new ObjectDb("UpdateStatus"))
            {
                db.ExecuteNonQuery(settingParams);
                var outputResult = db.GetOuputParams();
                result.ErrorCode = Fanex.Common.Converter.ToInt(outputResult["errcode"]);
                result.ErrorMessage = Fanex.Common.Converter.ToString2(outputResult["errmsg"]);
            }
            return (result.ErrorCode != 0) ? result : new Result();
        }

        public Result UpdateRacingSetting(Hashtable settingParams, int roleId)
        {
            var result = new Result();
            string spMaping = "UpdateMinMaxPt";

            if (!string.IsNullOrEmpty(spMaping))
            {
                using (var db = new ObjectDb(spMaping))
                {
                    db.ExecuteNonQuery(settingParams);
                    var outputResult = db.GetOuputParams();
                    result.ErrorCode = Fanex.Common.Converter.ToInt(outputResult["errcode"]);
                    result.ErrorMessage = Fanex.Common.Converter.ToString2(outputResult["errmsg"]);
                }
            }

            return (result.ErrorCode != 0)? result : new Result();
        }

        public Dictionary<int, string> GetExchange()
        {
            var exchange = new Dictionary<int, string>();
           
                //get data from database
                
                using (var objDb = new ObjectDb("ExchangeCurrencySel"))
                {
                    var dsResult = objDb.ExecuteDataSet();
                    if (dsResult == null || dsResult.Tables.Count == 0 || dsResult.Tables[0].Rows.Count == 0)
                        return null;
                    DataRow[] dr = dsResult.Tables[0].Select();
                    foreach (DataRow row in dr)
                    {
                        exchange.Add(Fanex.Common.Converter.ToInt(row["exchangeid"]), row["currency"].ToString());
                    }
                    return exchange;
                }
             
        }
    }
}
