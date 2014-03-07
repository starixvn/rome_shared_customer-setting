// --------------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Fanex.Common;

    /// <summary>
    /// The ListExtensions class.
    /// </summary>
    public static class ListExtensions
    {
        public const string ASC = "asc";
        public const string DESC = "desc";

        /// <summary>
        /// Sorts the by property.
        /// </summary>
        /// <typeparam name="T">Generic type in list.</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortColumn">The sort column.</param>
        public static void SortByProperty<T>(this List<T> dataSource, string sortDirection, string sortColumn)
        {
            PropertyInfo sortColumnInformation = typeof(T).GetProperty(sortColumn);
            if (sortColumnInformation == null || dataSource == null || dataSource.Count == 0
                || (sortDirection.ToLower() != ASC && sortDirection.ToLower() != DESC))
            {
                return;
            }

            bool isAscendingSort = sortDirection.ToLower().Equals(ASC) ? true : false;

            Comparison<T> comparison = delegate(T a, T b)
            {
                object valueA = Converter.ToDecimal(sortColumnInformation.GetValue(a, null));
                object valueB = Converter.ToDecimal(sortColumnInformation.GetValue(b, null));

                return valueA is IComparable ? isAscendingSort ? ((IComparable)valueA).CompareTo(valueB) : ((IComparable)valueB).CompareTo(valueA) : 0;
            };

            dataSource.Sort(comparison);
        }
    }
}
