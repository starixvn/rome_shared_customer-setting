// --------------------------------------------------------------------------
// <copyright file="StatementReportRepository.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Repositories.StatementReport
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Module.Statement.Criteria;
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
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
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
                        StatementDate = Converter.ToDateTime(reader["winlostdate"]),
                        OpeningBalance = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["OpeningBalance"]),
                            Value = Converter.To<double>(reader["OpeningBalance"])
                        },
                        Turnover = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["turnover"]),
                            Value = Converter.To<double>(reader["turnover"])
                        },
                        WinLoss = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["WinLost"]),
                            Value = Converter.To<double>(reader["WinLost"])
                        },
                        Commission = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["Comm"]),
                            Value = Converter.To<double>(reader["Comm"])
                        },
                        TransferAmount = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["TransferAmt"]),
                            Value = Converter.To<double>(reader["TransferAmt"])
                        },
                        AccumulatedTotal = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["ClosingBalance"]),
                            Value = Converter.To<double>(reader["ClosingBalance"])
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
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public StatementReportModel MemberStatement(MemberStatementCriteria criteria)
        {
            StatementReportModel result = new StatementReportModel
            {
                StatementList = new List<StatementModel>(),
                PageIndex = criteria.PageIndex
            };

            if (criteria.IsValid())
            {
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
                                HasData = !Convert.IsDBNull(reader["OpeningBalance"]),
                                Value = Converter.To<double>(reader["OpeningBalance"])
                            },
                            Turnover = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["turnover"]),
                                Value = Converter.To<double>(reader["turnover"])
                            },
                            VoidCancelled = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["VoidAmt"]),
                                Value = Converter.To<double>(reader["VoidAmt"])
                            },
                            Outstanding = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["Outstanding"]),
                                Value = Converter.To<double>(reader["Outstanding"])
                            },
                            WinLoss = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["WinLost"]),
                                Value = Converter.To<double>(reader["WinLost"])
                            },
                            Commission = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["Comm"]),
                                Value = Converter.To<double>(reader["Comm"])
                            },
                        GrandPrize = new DataColumnModel
                        {
                            HasData = !Convert.IsDBNull(reader["GrandPrize"]),
                            Value = Converter.To<double>(reader["GrandPrize"])
                        },
                            TransferAmount = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["TransferAmt"]),
                                Value = Converter.To<double>(reader["TransferAmt"])
                            },
                            AccumulatedTotal = new DataColumnModel
                            {
                                HasData = !Convert.IsDBNull(reader["ClosingBalance"]),
                                Value = Converter.To<double>(reader["ClosingBalance"])
                            }
                        };

                        statementModel.StatementDateString = statementModel.StatementDate.ToString("MM/dd/yyyy");
                        result.StatementList.Add(statementModel);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the transfer details.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The transfer details.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
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
            if (criteria.IsValid())
            {
                using (var objDb = new ObjectDb(criteria.GetSettingKey()))
                {
                    objDb.AssignParamValues(criteria.GetSpParams());
                    var reader = objDb.ExecuteReader();
                    while (reader.Read())
                    {
                        var transferAmount = Converter.To<double>(reader["amount"]);
                        var statementModel = new StatementModel
                        {
                            StatementDate = Converter.ToDateTime(reader["transdate"]),
                            TransactionType = Converter.ToString2(reader["transtype"]),
                            Description = Converter.ToString2(reader["transdesc"]),
                            TransferAmount = new DataColumnModel
                            {
                                HasData = true,
                                Value = transferAmount
                            },
                            Creator = Converter.ToString2(reader["creator"])
                        };

                        if (criteria.BalanceUpDate <= Converter.ToDateTime(criteria.TransferDate))
                        {
                            statementModel.ExchangeRate = Converter.ToString2(reader["ExRate"]);
                        }

                        statementModel.StatementDateString = statementModel.StatementDate.ToString("dddd <br> MM/dd/yyyy <br> hh:mm:ss tt");
                        result.StatementList.Add(statementModel);
                        totalTransferAmount += transferAmount;
                    }
                }
            }

            result.Total.TransferAmount.Value = Math.Round(totalTransferAmount, 2);
            return result;
        }

        /// <summary>
        /// Gets the customer information.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The customer information.</returns>
        public CustomerInformationModel GetCustomerInfo(GetCustomerInformationByUsername criteria)
        {
            CustomerInformationModel result = new CustomerInformationModel();
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                while (reader.Read())
                {
                    result.CustomerId = Converter.ToInt(reader["custid"]);
                    result.RoleId = Converter.ToInt(reader["roleid"]);
                }
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
            bool result = false;
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.AssignParamValues(criteria.GetSpParams());
                var reader = objDb.ExecuteReader();
                var outputParams = objDb.GetOuputParams();
                result = Converter.ToBool(outputParams["isinternal"]);
            }

            return result;
        }

        /// <summary>
        /// Gets balance up day.
        /// </summary>
        /// <returns>DataTable object.</returns>
        public DataTable GetBalanceUpDay()
        {
            using (var db = new ObjectDb("Age_SelBalanceUpDay"))
            {
                return db.ExecuteDataSet().Tables[0];
            }
        }
    }
}
