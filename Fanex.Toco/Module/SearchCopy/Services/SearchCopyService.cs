// --------------------------------------------------------------------------
// <copyright file="SearchCopyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using System.Data;
    using Fanex.CustomerSetting.Module.SearchCopy.Repositories;

    /// <summary>
    /// Class SearchCopyService.
    /// </summary>
    public class SearchCopyService : ISearchCopyService
    {
        private static ISearchCopyRepository _searchCopyRepository;

        public SearchCopyService() : this(null)
        {
        }

        public SearchCopyService(ISearchCopyRepository searchCopyRepository)
        {
            _searchCopyRepository = searchCopyRepository ?? new SearchCopyRepository();
        }

        public DataTable GetSportsbookSettings(int custId)
        {
            return _searchCopyRepository.GetSportsbookSettings(custId);
        }        
    }
}
