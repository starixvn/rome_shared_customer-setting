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
    using System.Data;

    /// <summary>
    /// Class ISearchCopyService.
    /// </summary>
    public interface ISearchCopyService
    {
        int SearchExistingUser(string username);

        DataTable GetCustomerInfo(string username);
    }
}
