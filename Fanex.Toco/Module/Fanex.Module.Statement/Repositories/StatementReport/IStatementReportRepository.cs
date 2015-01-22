// --------------------------------------------------------------------------
// <copyright file="IStatementReportRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Repositories.StatementReport
{
    using Fanex.Module.Statement.Criteria;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models;
    using Fanex.Module.Statement.Models.StatementReport;

    public interface IStatementReportRepository
    {
        StatementReportModel SMAStatement(SMAStatementCriteria criteria);

        StatementReportModel MemberStatement(MemberStatementCriteria criteria);

        StatementReportModel TransferDetails(TransferDetailsCriteria criteria);

        CustomerInformationModel GetCustomerInfo(GetCustomerInformationByUsername criteria);

        bool CheckInternalByUsername(CheckInternalByUsernameCriteria criteria);
    }
}
