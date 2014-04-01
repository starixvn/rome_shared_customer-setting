// --------------------------------------------------------------------------
// <copyright file="DataRecordExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Helpers
{
    using System;
    using System.Data;

    /// <summary>
    /// The DataRecordExtensions class.
    /// </summary>
    public static class DataRecordExtensions
    {
        /// <summary>
        /// Determines whether the specified data record has column.
        /// </summary>
        /// <param name="dataRecord">The data record.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The boolean value.</returns>
        public static bool HasColumn(this IDataRecord dataRecord, string columnName)
        {
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                if (dataRecord.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}