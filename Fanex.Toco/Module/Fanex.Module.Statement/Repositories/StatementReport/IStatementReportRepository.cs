// --------------------------------------------------------------------------
// <copyright file="IStatementReportRepository.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
namespace Fanex.Module.Statement.Repositories.StatementReport
{
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Module.Statement.Criteria;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models;
    using Fanex.Module.Statement.Models.StatementReport;

    public interface IStatementReportRepository
    {
        StatementReportModel SMAStatement(SMAStatementCriteria criteria);

        StatementReportModel MemberStatement(MemberStatementCriteria criteria);

        List<RewardStatementModel> RewardStatement(RewardStatementCriteria criteria);

        StatementReportModel TransferDetails(TransferDetailsCriteria criteria);

        StatementReportModel RewardTransferDetails(TransferDetailsCriteria criteria);

        CustomerInformationModel GetCustomerInfo(GetCustomerInformationByUsername criteria);

        bool CheckInternalByUsername(CheckInternalByUsernameCriteria criteria);

        DataTable GetBalanceUpDay();
    }
}
