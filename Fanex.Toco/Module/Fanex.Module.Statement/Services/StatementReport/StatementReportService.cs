// --------------------------------------------------------------------------
// <copyright file="StatementReportService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services.StatementReport
{
    using CustomerSetting.Module.Core.Criteria;
    using CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.Core.Services.Impl;
    using Fanex.Module.Statement.Criteria;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models;
    using Fanex.Module.Statement.Models.StatementReport;
    using Fanex.Module.Statement.Repositories.StatementReport;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

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

            var serviceBase = new ServiceBase();
            var checkSpecialSite = serviceBase.CheckSpecialSitePermission(
                new CheckSpecialSitePermissionCriteria()
                {
                    UserID = AppSession.CurrentSession.UserId,
                    CustID = criteria.CustId
                });
            if (checkSpecialSite.ErrorCode > 0)
            {
                return result;
            }

            try
            {
                List<StatementReportCriteria> dateRangeList = GetReportDateRange(criteria);
                for (var i = 0; i < dateRangeList.Count; i++)
                {
                    var dateRange = dateRangeList[i];
                    var data = _statementReportRepository.SMAStatement(new SMAStatementCriteria
                    {
                        FromDate = dateRange.FromDate,
                        ToDate = dateRange.ToDate,
                        RoleId = criteria.RoleId,
                        PageSize = criteria.PageSize,
                        PageIndex = criteria.PageIndex,
                        CustId = criteria.CustId
                    });

                    result.StatementList = Enumerable.Concat(result.StatementList, data.StatementList).ToList();
                }
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

            var serviceBase = new ServiceBase();
            var checkSpecialSite = serviceBase.CheckSpecialSitePermission(
                new CheckSpecialSitePermissionCriteria()
                {
                    UserID = AppSession.CurrentSession.UserId,
                    CustID = criteria.CustId
                });
            if (checkSpecialSite.ErrorCode > 0)
            {
                return result;
            }

            if (criteria.IsValid())
            {
                try
                {
                    List<StatementReportCriteria> dateRangeList = GetReportDateRange(criteria);
                    for (var i = 0; i < dateRangeList.Count; i++)
                    {
                        var dateRange = dateRangeList[i];
                        var data = _statementReportRepository.MemberStatement(new MemberStatementCriteria
                        {
                            FromDate = dateRange.FromDate,
                            ToDate = dateRange.ToDate,
                            RoleId = criteria.RoleId,
                            PageSize = criteria.PageSize,
                            PageIndex = criteria.PageIndex,
                            CustId = criteria.CustId
                        });

                        result.StatementList = Enumerable.Concat(result.StatementList, data.StatementList).ToList();
                    }
                }
                catch (Exception ex)
                {
                    LoggingHandler.Log(ex);
                }
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

        /// <summary>
        /// Gets the customer information.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The customer information.</returns>
        public CustomerInformationModel GetCustomerInfo(GetCustomerInformationByUsername criteria)
        {
            CustomerInformationModel result = new CustomerInformationModel();
            if (!criteria.IsValid())
            {
                return result;
            }

            try
            {
                result = _statementReportRepository.GetCustomerInfo(criteria);
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }

            return result;
        }

        /// <summary>
        /// Checks Internal By Username.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// Is Internal or Not.
        /// </returns>
        public bool CheckInternalByUsername(CheckInternalByUsernameCriteria criteria)
        {
            if (!criteria.IsValid())
            {
                return false;
            }

            bool result = false;
            try
            {
                result = _statementReportRepository.CheckInternalByUsername(criteria);
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }

            return result;
        }
        
        /// <summary>
        /// Gets the report date range.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The report date range.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private List<StatementReportCriteria> GetReportDateRange(StatementReportCriteria criteria)
        {
            criteria.FromDate = criteria.FromDate.Year <= 1753 ||
                criteria.FromDate.Year >= 9999 
                ? DateTime.Now : criteria.FromDate;
            criteria.ToDate = criteria.ToDate.Year <= 1753 ||
                criteria.ToDate.Year >= 9999
                ? DateTime.Now : criteria.ToDate;

            if (criteria.FromDate > criteria.MoveBetsDay ||
                (criteria.BalanceUpDay <= criteria.FromDate && criteria.ToDate <= criteria.MoveBetsDay) ||
                 criteria.ToDate < criteria.BalanceUpDay)
            {
                return new List<StatementReportCriteria>() 
                { 
                    new StatementReportCriteria
                    {
                        FromDate = criteria.FromDate,
                        ToDate = criteria.ToDate 
                    }
                };
            }

            if (criteria.FromDate >= criteria.BalanceUpDay && criteria.ToDate > criteria.MoveBetsDay)
            {
                return new List<StatementReportCriteria>() 
                { 
                    new StatementReportCriteria
                    {
                        FromDate = criteria.FromDate,
                        ToDate = criteria.MoveBetsDay
                    },
                    new StatementReportCriteria
                    {
                        FromDate = criteria.MoveBetsDay.AddDays(1),
                        ToDate = criteria.ToDate
                    }
                };
            }

            if (criteria.ToDate > criteria.MoveBetsDay)
            {
                return new List<StatementReportCriteria>() 
                { 
                    new StatementReportCriteria
                    {
                        FromDate = criteria.FromDate,
                        ToDate = criteria.BalanceUpDay.AddDays(-1)
                    },
                    new StatementReportCriteria
                    {
                        FromDate = criteria.BalanceUpDay,
                        ToDate = criteria.MoveBetsDay
                    },
                    new StatementReportCriteria
                    {
                        FromDate = criteria.MoveBetsDay.AddDays(1),
                        ToDate = criteria.ToDate
                    }
                };
            }

            return new List<StatementReportCriteria>() 
            { 
                    new StatementReportCriteria
                    {
                        FromDate = criteria.FromDate,
                        ToDate = criteria.BalanceUpDay.AddDays(-1)
                    },
                    new StatementReportCriteria
                    {
                        FromDate = criteria.BalanceUpDay,
                        ToDate = criteria.ToDate
                    }
                };
        }
    }
}
