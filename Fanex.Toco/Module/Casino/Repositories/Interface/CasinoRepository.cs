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

        IList<int> GetEnabledJackpotProducts(GetEnabledJackpotProductsCriteria criteria);

        Result ExecuteNonQuery(ICriteria criteria);
    }
}