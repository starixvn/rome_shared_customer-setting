// --------------------------------------------------------------------------
// <copyright file="ISearchCopyRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using System.Data;

    public interface ISearchCopyRepository
    {
        int SearchExistingUser(string username);

        DataTable GetCustomerInfo(string username);
    }
}