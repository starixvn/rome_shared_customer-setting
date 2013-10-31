// --------------------------------------------------------------------------
// <copyright file="ISearchCopyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

using Fanex.PTRacing.BusinessObjects;
using System.Collections;
using System.Data;

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    public interface ISearchCopyService
    {
        DataTable GetSportsbookSettings(int custId); 
    }
}
