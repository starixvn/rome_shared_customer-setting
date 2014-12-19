// --------------------------------------------------------------------------
// <copyright file="IStatementReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services.StatementReport
{
    using System.IO;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models.StatementReport;

    public interface IStatementReportService
    {
        StatementReportModel SMAStatement(SMAStatementCriteria criteria);

        StatementReportModel MemberStatement(MemberStatementCriteria criteria);

        StatementReportModel TransferDetails(TransferDetailsCriteria criteria);

        MemoryStream ExportMemberStatement(MemberStatementCriteria criteria, StatementReportModel data);

        MemoryStream ExportSMAStatement(SMAStatementCriteria criteria, StatementReportModel data);

        MemoryStream ExportTransferDetails(TransferDetailsCriteria criteria, StatementReportModel data);
    }
}
