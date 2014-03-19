// --------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Anders.Le" date="2012/10/11">Created.</change>
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
            return date.HasValue ? date.Value.ToShortFormatedDate() : string.Empty;
        }

        public static string ToShortFormatedDate(this DateTime date)
        {
            return date.ToString(Constants.ShortDateFormat);
        }

        public static string ToLongFormatedDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToLongFormatedDate() : string.Empty;
        }

        public static string ToLongFormatedDate(this DateTime date)
        {
            return date.ToString(Constants.LongDateFormat);
        }

        public static string ToMonthAndDay(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToMonthAndDay() : string.Empty;
        }

        public static string ToMonthAndDay(this DateTime date)
        {
            return date.ToString("dd-MMM");
        }

        public static string ToMonthYear(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToMonthYear() : string.Empty;
        }

        public static string ToMonthYear(this DateTime date)
        {
            return date.ToString(Constants.MonthYearDateFormat);
        }

        public static string ToShortFormatedDateWithSlash(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortFormatedDateWithSlash() : string.Empty;
        }

        public static string ToLongFormatedDateWithSlash(this DateTime date)
        {
            return date.ToString(Constants.LongDateFormatWithSlash);
        }

        public static string ToShortFormatedDateWithSlash(this DateTime date)
        {
            return date.ToString(Constants.ShortDateFormatWithSlash);
        }

        public static string ToMonthYearWithSlash(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToMonthYearWithSlash() : string.Empty;
        }

        public static string ToMonthYearWithSlash(this DateTime date)
        {
            return date.ToString(Constants.MonthYearDateFormatWithSlash);
        }
    }
}