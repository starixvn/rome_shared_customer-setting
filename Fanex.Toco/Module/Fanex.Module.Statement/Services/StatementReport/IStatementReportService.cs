// --------------------------------------------------------------------------
// <copyright file="IStatementReportService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services.StatementReport
{
    using System.Collections.Generic;
    using System.IO;
    using Fanex.Module.Statement.Criteria;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models;
    using Fanex.Module.Statement.Models.StatementReport;

    public interface IStatementReportService
    {
        StatementReportModel SMAStatement(SMAStatementCriteria criteria);

        StatementReportModel MemberStatement(MemberStatementCriteria criteria);

        List<RewardStatementModel> RewardStatement(RewardStatementCriteria criteria);

        StatementReportModel TransferDetails(TransferDetailsCriteria criteria);

        MemoryStream ExportMemberStatement(MemberStatementCriteria criteria, StatementReportModel data);

        MemoryStream ExportSMAStatement(SMAStatementCriteria criteria, StatementReportModel data);

        MemoryStream ExportRewardStatement(RewardStatementCriteria criteria, List<RewardStatementModel> data);

        MemoryStream ExportTransferDetails(TransferDetailsCriteria criteria, StatementReportModel data);

        CustomerInformationModel GetCustomerInfo(GetCustomerInformationByUsername criteria);

        bool CheckInternalByUsername(CheckInternalByUsernameCriteria criteria);
    }
}
