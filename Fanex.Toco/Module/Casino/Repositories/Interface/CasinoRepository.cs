namespace Fanex.CustomerSetting.Module.Casino.Repositories.Interface
{
    using System.Collections.Generic;
    using Core.Models;
    using Criteria;
    using Data.Repository;
    using Entities;

    public interface CasinoRepository
    {
        CasinoCustomer GetCustInfoIBC(int ibcCustId, int uplineId);

        IList<int> GetEnabledJackpotProducts(GetEnabledJackpotProductsCriteria criteria);

        Result ExecuteNonQuery(ICriteria criteria);
    }
}