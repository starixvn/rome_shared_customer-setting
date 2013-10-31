// --------------------------------------------------------------------------
// <copyright file="SearchCopyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

using Fanex.PTRacing.BusinessObjects;
using Fanex.PTRacing.Services;
using System.Collections;
using System.Data;

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    public class SearchCopyService: ISearchCopyService
    {
        protected static ISearchCopyService _searchCopyService;

        public SearchCopyService(): this(null)
        {

        }

        public SearchCopyService(ISearchCopyService searchCopyService)
        {
            _searchCopyService = searchCopyService ?? new SearchCopyService();
        }

        public DataTable GetSportsbookSettings(int custId)
        {
            return _searchCopyService.GetSportsbookSettings(custId);
        }
        
    }
}
