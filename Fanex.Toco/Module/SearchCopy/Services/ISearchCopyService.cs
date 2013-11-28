// --------------------------------------------------------------------------
// <copyright file="ISearchCopyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>
namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.SearchCopy.Command;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;

    /// <summary>
    /// Class ISearchCopyService.
    /// </summary>
    public interface ISearchCopyService
    {        
        int SearchExistingUser(SearchExistingUserCriteria criteria);

        HideSports GetSportsStatusByUserName(string username);

        IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria);

        string GetSiteCreditAndCurrencyByUserId(GetSiteByUserIdCriteria criteria);

        CommandResult CopyExistingUser(CustomerInsMemberCriteria criteria);
    }
}
