namespace Fanex.CustomerSetting.Module.SearchCopy.Repository.Impl
{
    using Core.Models;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Interface;

    public class DCSSettingCopyInsertMemberRepository : IDCSSettingCopyInsertMemberRepository
    {
        public Result CorrectMemberPositionTaking(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();
                int errorCode = Converter.ToInt(outputParams["errcode"]);
                string message = Converter.ToString2(outputParams["errmsg"]);

                var result = new Result()
                {
                    ErrorCode = errorCode,
                    ErrorMessage = message
                };

                return result;
            }
        }

        public bool CheckMemberPositionTakingConstrain(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();
                int errorCode = Converter.ToInt(outputParams["errcode"]);

                return errorCode > 0;
            }
        }
    }
}
