// --------------------------------------------------------------------------
// <copyright file="StatementModel.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Models.StatementReport
{
    using System;

    /// <summary>
    /// Statement Model.
    /// </summary>
    [Serializable]
    public class StatementModel
    {
        public DateTime StatementDate { get; set; }

        public string StatementDateString { get; set; }

        public DataColumnModel OpeningBalance { get; set; }

        public DataColumnModel Turnover { get; set; }

        public DataColumnModel VoidCancelled { get; set; }

        public DataColumnModel Outstanding { get; set; }

        public DataColumnModel WinLoss { get; set; }

        public DataColumnModel Commission { get; set; }

        public DataColumnModel GrandPrize { get; set; }

        public DataColumnModel TransferAmount { get; set; }

        public DataColumnModel AccumulatedTotal { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }

        public string ExchangeRate { get; set; }

        public string Creator { get; set; }
    }
}
