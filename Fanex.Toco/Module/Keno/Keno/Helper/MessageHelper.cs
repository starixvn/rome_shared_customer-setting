// --------------------------------------------------------------------------
// <copyright file="MessageHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.Services;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageHelper
    {
        /// <summary>
        /// Parses the error.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMsg">The error MSG.</param>
        /// <returns>The list of result object.</returns>
        public static List<Result> ParseError(string errorCode, string errorMsg)
        {
            var errorResults = new List<Result>();

            if (!string.IsNullOrEmpty(errorCode))
            {
                string[] splitChars = new[] { "\a" };
                var arrErrorCode = errorCode.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                var arrErrorMsgs = errorMsg.Split(splitChars, StringSplitOptions.None);
                if (arrErrorCode.Length > 0)
                {
                    var totalErrors = arrErrorCode.Length;
                    for (int i = 0; i < totalErrors; i++)
                    {
                        var errCode = Converter.ToInt(arrErrorCode[i]);
                        if (errCode == 0)
                        {
                            continue;
                        }

                        var errMsg = errCode > 0
                                        ? arrErrorMsgs[i]
                                        : string.Empty; ////ResourceHelper.GetResource(errCode.ToString());

                        errorResults.Add(new Result()
                        {
                            ErrorCode = errCode,
                            ErrorMessage = errMsg
                        });
                    }

                    if (errorResults.Count == 0)
                    {
                        errorResults.Add(new Result()
                        {
                            ErrorCode = 0,
                            ErrorMessage = string.Empty ////ResourceHelper.GetResource("0")
                        });
                    }
                }
            }
            else
            {
                errorResults.Add(new Result()
                {
                    ErrorCode = 1000,
                    ErrorMessage = string.Empty ////ResourceHelper.GetResource("Unchanged")
                });
            }

            return errorResults;
        }
    }
}