namespace Fanex.CustomerSetting.Module.Casino.Repositories.Interface
{
    using System.Collections.Generic;
    using Core.Models;
    using Criteria;
    using Entities;
    using NAP.Framework.Data;

    public interface CasinoRepository
    {
        CasinoCustomer GetCustInfoIBC(int ibcCustId, int uplineId);

        bool GetCasinoStatus(int custid);

        bool UpdateCSStatus(int custid, bool syncCS, bool disabledCS, int userId, string ip, int subAccId, string lang, string username, string subUsername, byte systemId);

        bool DoUpdateCSStatusWithoutSync(int custid, bool syncCS, bool disabledCS, int userId, string ip, int subAccId, byte systemId, string lang, out string errMsg);

        IList<int> GetEnabledJackpotProducts(GetEnabledJackpotProductsCriteria criteria);

        Result ExecuteNonQuery(ICriteria criteria);
    }
}