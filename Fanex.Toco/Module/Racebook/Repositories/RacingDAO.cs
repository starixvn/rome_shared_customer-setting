using System.Collections.Generic;
namespace Fanex.CustomerSetting.Module.Racebook.Repositories
{
    using System.Collections;
    using System.Data;
    using Fanex.CustomerSetting.Module.Racebook.Entities;
    using Fanex.Data;
    using Fanex.Data.DNAide;

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
            DbResult dbResult = null;
            string spMaping = null;
            var param = new Hashtable { { "custid", custId } };
            switch (roleId)
            {
                case (int)CustInfo.Roles.Super:
                    spMaping = "GetSuperSetting";
                    break;
                case (int)CustInfo.Roles.Master:
                    spMaping = "GetMasterSetting";
                    break;
                case (int)CustInfo.Roles.Agent:
                    spMaping = "GetAgentSetting";
                    break;
                case (int)CustInfo.Roles.Member:
                    spMaping = "GetMemberSetting";
                    break;
                case (int)CustInfo.Roles.SuperOnMember: 
                    spMaping = "GetSuperOnMember";
                    break;
                case (int)CustInfo.Roles.MasterOnMember: 
                    spMaping = "GetMasterOnMember";
                    break;
                case (int)CustInfo.Roles.SuperMin: 
                    spMaping = "GetSuperMin";
                    break;
                case (int)CustInfo.Roles.MasterMinMax: 
                    spMaping = "GetMasterMinMax";
                    break;
                case (int)CustInfo.Roles.AgentMinMax: 
                    spMaping = "GetAgentMinMax";
                    break;
            }
            using (var objDb = new ObjectDb(spMaping))
            {
                var dsResult = objDb.ExecuteDataSet(param);
                if (dsResult == null || dsResult.Tables.Count == 0)
                    return null;
                return dsResult.Tables[0];
            }

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
            string spMaping = null;
            switch (roleId)
            {
                case (int)CustInfo.Roles.Super:
                    spMaping = "UpdateSuperSetting";
                    break;

                case (int)CustInfo.Roles.Master:
                    spMaping = "UpdateMasterSetting";
                    break;

                case (int)CustInfo.Roles.Agent:
                    spMaping = "UpdateAgentSetting";
                    break;

                case (int)CustInfo.Roles.Member:
                    spMaping = "UpdateMemberSetting";
                    break;
                case (int)CustInfo.Roles.SuperOnMember: 
                    spMaping = "UpdateSuperOnMember";
                    break;
                case (int)CustInfo.Roles.MasterOnMember: 
                    spMaping = "UpdateMasterOnMember";
                    break;
                case (int)CustInfo.Roles.SuperMin:
                case (int)CustInfo.Roles.MasterMinMax:
                case (int)CustInfo.Roles.AgentMinMax:
                    spMaping = "UpdateMinMaxPt";
                    break;
            }

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
