// --------------------------------------------------------------------------
// <copyright file="NumberExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Extensions
{
    using System;

    /// <summary>
    /// Number Extensions.
    /// </summary>
    public static class NumberExtensions
    {
        public static string Format(this int value)
        {
            return DoFormat(value, 0);
        }

        public static string Format(this object value)
        {
            return DoFormat(value, 0);
        }

        public static string Format(this decimal value, byte decimalDigits)
        {
            return DoFormat(value, decimalDigits);
        }

        public static string Format(this object value, byte decimalDigits)
        {
            return DoFormat(value, decimalDigits);
        }

        public static string Format(this double value, byte decimalDigits)
        {
            return DoFormat(value, decimalDigits);
        }

        public static string Format(this float value, byte decimalDigits)
        {
            return DoFormat(value, decimalDigits);
        }

        public static string FormatNegative(this double value, byte decimalDigits)
        {
            return value < 0 ? "(" + DoFormat(-value, decimalDigits) + ")" : DoFormat(value, decimalDigits);
        }

        private static string DoFormat(object value, byte decimalDigits)
        {
            ThrowExceptionWhenInputValueIsNull(value);
            return DoFormatInCaseInputValueIsValid(value, decimalDigits);
        }

        private static string DoFormatInCaseInputValueIsValid(object value, byte decimalDigits)
        {
            string formatString = "{0:n" + decimalDigits.ToString() + "}";
            return string.Format(formatString, value);
        }

        private static void ThrowExceptionWhenInputValueIsNull(object value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
