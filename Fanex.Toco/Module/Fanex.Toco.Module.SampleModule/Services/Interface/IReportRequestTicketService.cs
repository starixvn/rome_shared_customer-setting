// --------------------------------------------------------------------------
// <copyright file="IReportRequestTicketService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

using System.Diagnostics.CodeAnalysis;
using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria.ReportRequestTicket;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities.ReportRequestTicket;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface
{
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public interface IReportRequestTicketService
    {
        RequestTicketInfo GetRequestTicketReport(GetRequestTicketReportCriteria criteria);

        string GetMaxRequestNo(GetMaxRequestNoCriteria criteria);

        Result AddNewRequestTicket(AddNewRequestTicketCriteria criteria);

        Result DeleteRequestTicket(DeleteRequestTicketCriteria criteria);

        Result UpdateRequestTicket(UpdateRequestTicketCriteria criteria);
    }
}
