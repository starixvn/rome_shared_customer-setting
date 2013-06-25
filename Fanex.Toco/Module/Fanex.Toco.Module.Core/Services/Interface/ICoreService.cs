// -----------------------------------------------------------------------
// <copyright file="ICommonService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Fanex.Toco.Module.Core.Criteria;
using Fanex.Toco.Module.Core.Models;

namespace Fanex.Toco.Module.Core.Services.Interface
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICoreService
    {
        IList<Bettypes> GetBettypes(GetBettypeCriteria criteria);
        IList<Currency> GetCurrency();
        
    }
}
