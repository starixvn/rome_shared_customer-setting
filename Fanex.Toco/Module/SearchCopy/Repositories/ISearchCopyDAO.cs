// --------------------------------------------------------------------------
// <copyright file="ISearchCopyDAO.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

using System.Collections;
using System.Collections.Generic;
using System.Data;
using Fanex.CustomerSetting.Module.SearchCopy.Entities;

namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    public interface ISearchCopyDAO
    {
        DataTable GetSportsbookSettings(int custId);
    }
}