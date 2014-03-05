// --------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Anders.Le" date="2012.10.11">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Extensions
{
    using System;
    using Fanex.CustomerSetting.Module.Core.Entities;

    /// <summary>
    /// Contains date extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static string ToShortFormatedDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToShortFormatedDate();
            }

            return string.Empty;
        }

        public static string ToShortFormatedDate(this DateTime date)
        {
            return date.ToString(Constants.SHORT_DATE_FORMAT);
        }

        public static string ToLongFormatedDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToLongFormatedDate();
            }

            return string.Empty;
        }

        public static string ToLongFormatedDate(this DateTime date)
        {
            return date.ToString(Constants.LONG_DATE_FORMAT);
        }

        public static string ToMonthAndDay(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToMonthAndDay();
            }

            return string.Empty;
        }

        public static string ToMonthAndDay(this DateTime date)
        {
            return date.ToString("dd-MMM");
        }

        public static string ToMonthYear(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToMonthYear();
            }

            return string.Empty;
        }

        public static string ToMonthYear(this DateTime date)
        {
            return date.ToString(Constants.MONTH_YEAR_DATE_FORMAT);
        }

        public static string ToShortFormatedDateWithSlash(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToShortFormatedDateWithSlash();
            }

            return string.Empty;
        }

        public static string ToLongFormatedDateWithSlash(this DateTime date)
        {
            return date.ToString(Constants.LONG_DATE_FORMAT_WITH_SLASH);
        }

        public static string ToShortFormatedDateWithSlash(this DateTime date)
        {
            return date.ToString(Constants.SHORT_DATE_FORMAT_WITH_SLASH);
        }

        public static string ToMonthYearWithSlash(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToMonthYearWithSlash();
            }

            return string.Empty;
        }

        public static string ToMonthYearWithSlash(this DateTime date)
        {
            return date.ToString(Constants.MONTH_YEAR_DATE_FORMAT_WITH_SLASH);
        }
    }
}