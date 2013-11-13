// ************************************************************************************
// <copyright file="UpdateRequestTicketCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) UpdateRequestTicketCriteria.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Criteria.ReportRequestTicket
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UpdateRequestTicketCriteria : CriteriaBase
    {
        public string ReqNo { get; set; }

        public string Requestor { get; set; }

        public string DateOfRequest { get; set; }

        public string ReportName { get; set; }

        public string Remark { get; set; }

        public string Dateline { get; set; }

        public string Assignee { get; set; }

        public string Status { get; set; }

        public string RemarkByPreparer { get; set; }

        public string LastModifiedBy { get; set; }

        public override string GetSettingKey()
        {
            return "Report_RequestTicketReport_UpdateRequestTicket";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
