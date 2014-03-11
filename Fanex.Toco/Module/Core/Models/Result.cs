// --------------------------------------------------------------------------
// <copyright file="Result.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Models
{
    using System;

    /// <summary>
    /// The result class.
    /// </summary>
    [Serializable]
    public class Result
    {
        public Result()
        {
            ErrorCode = 0;
            ErrorMessage = string.Empty;
        }

        public Result(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            IsSuccess = ErrorCode == 0 || ErrorMessage == string.Empty;
        }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; private set; }
    }
}
