// --------------------------------------------------------------------------
// <copyright file="StatementReportRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Repositories.StatementReport
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models;
    using Fanex.Module.Statement.Models.StatementReport;

    /// <summary>
    /// Statement Report Repository.
    /// </summary>
    public class StatementReportRepository : IStatementReportRepository
    {
        /// <summary>
        /// Gets the SMA statement.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The SMA statement.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public StatementReportModel SMAStatement(SMAStatementCriteria criteria)
        {
            StatementReportModel result = new StatementReportModel
            {
                StatementList = new List<StatementModel>(),
                PageIndex = criteria.PageIndex
            };

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    var statementModel = new StatementModel
                    {
                        StatementDate = Converter.ToDateTime(reader["matchdate"]),
                        OpeningBalance = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        Turnover = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        WinLoss = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        Commission = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        TransferAmount = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        AccumulatedTotal = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        }
                    };

                    statementModel.StatementDateString = statementModel.StatementDate.ToString("MM/dd/yyyy");
                    result.StatementList.Add(statementModel);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the member statement.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The member statement.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public StatementReportModel MemberStatement(MemberStatementCriteria criteria)
        {
            StatementReportModel result = new StatementReportModel
            {
                StatementList = new List<StatementModel>(),
                PageIndex = criteria.PageIndex
            };

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    var statementModel = new StatementModel
                    {
                        StatementDate = Converter.ToDateTime(reader["winlostdate"]),
                        OpeningBalance = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        Turnover = new DataColumnModel
                        {
                            HasData = reader["turnover"] != null,
                            Value = Converter.To<double>(reader["turnover"])
                        },
                        VoidCancelled = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        Outstanding = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        WinLoss = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        Commission = new DataColumnModel
                        {
                            HasData = reader["commission"] != null,
                            Value = Converter.To<double>(reader["commission"])
                        },
                        TransferAmount = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        },
                        AccumulatedTotal = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(reader["totalamt"])
                        }
                    };

                    statementModel.StatementDateString = statementModel.StatementDate.ToString("MM/dd/yyyy");
                    result.StatementList.Add(statementModel);
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
               Total = new StatementModel
               {
                   TransferAmount = new DataColumnModel
                   {
                       HasData = true
                   }
               }
           };

            double totalTransferAmount = 0;
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    var transferAmount = Converter.To<double>(reader["totalamt"]);
                    var statementModel = new StatementModel
                    {
                        StatementDate = Converter.ToDateTime(reader["winlostdate"]),
                        Remark = Converter.ToString2(reader["transtype"]),
                        TransferAmount = new DataColumnModel
                        {
                            HasData = reader["totalamt"] != null,
                            Value = Converter.To<double>(transferAmount)
                        }
                    };

                    statementModel.StatementDateString = statementModel.StatementDate.ToString("MM/dd/yyyy <br> HH:MM:ss tt");
                    result.StatementList.Add(statementModel);
                    totalTransferAmount += transferAmount;
                }
            }

            result.Total.TransferAmount.Value = Converter.To<double>(totalTransferAmount);
            return result;
        }
    }
}
