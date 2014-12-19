// --------------------------------------------------------------------------
// <copyright file="StatementReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services.StatementReport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Fanex.CustomerSetting.Module.Core.Services.Impl;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models.StatementReport;
    using Fanex.Module.Statement.Repositories.StatementReport;

    /// <summary>
    /// Statement Report Service.
    /// </summary>
    public class StatementReportService : IStatementReportService
    {
        private readonly IStatementReportRepository _statementReportRepository;

        public StatementReportService() :
            this(null)
        {
        }

        public StatementReportService(IStatementReportRepository statementReportRepository)
        {
            _statementReportRepository = statementReportRepository ?? new StatementReportRepository();
        }

        /// <summary>
        /// Gets the SMA statement.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The SMA statement.</returns>
        public StatementReportModel SMAStatement(SMAStatementCriteria criteria)
        {
            StatementReportModel result = new StatementReportModel
            {
                StatementList = new List<StatementModel>()
            };

            try
            {
                result = _statementReportRepository.SMAStatement(criteria);
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the member statement.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The member statement.</returns>
        public StatementReportModel MemberStatement(MemberStatementCriteria criteria)
        {
            StatementReportModel result = new StatementReportModel
            {
                StatementList = new List<StatementModel>()
            };

            try
            {
                result = _statementReportRepository.MemberStatement(criteria);
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the transfer details.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The transfer details.</returns>
        public StatementReportModel TransferDetails(TransferDetailsCriteria criteria)
        {
            StatementReportModel result = new StatementReportModel
            {
                StatementList = new List<StatementModel>(),
                Total = new StatementModel()
            };

            try
            {
                result = _statementReportRepository.TransferDetails(criteria);
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }

            return result;
        }

        public MemoryStream ExportMemberStatement(MemberStatementCriteria criteria, StatementReportModel data)
        {
            if (data == null)
            {
                data = MemberStatement(criteria);
            }

            StatementReportExportExcelService _exportExcelService = new StatementReportExportExcelService();
            return _exportExcelService.ExportMemberStatement(criteria, data);
        }

        public MemoryStream ExportSMAStatement(SMAStatementCriteria criteria, StatementReportModel data)
        {
            if (data == null)
            {
                data = SMAStatement(criteria);
            }

            StatementReportExportExcelService _exportExcelService = new StatementReportExportExcelService();
            return _exportExcelService.ExportSMAStatement(criteria, data);
        }

        public MemoryStream ExportTransferDetails(TransferDetailsCriteria criteria, StatementReportModel data)
        {
            if (data == null)
            {
                data = TransferDetails(criteria);
            }

            StatementReportExportExcelService _exportExcelService = new StatementReportExportExcelService();
            return _exportExcelService.ExportTransferDetails(criteria, data);
        }
    }
}
