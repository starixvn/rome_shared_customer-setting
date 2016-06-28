// --------------------------------------------------------------------------
// <copyright file="StatementReportModel.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Models.StatementReport
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Statement Report Model.
    /// </summary>
    [Serializable]
    public class StatementReportModel
    {
        public List<StatementModel> StatementList { get; set; }

        public StatementModel Total { get; set; }

        public int TotalItems { get; set; }

        public int PageIndex { get; set; }
    }
}
