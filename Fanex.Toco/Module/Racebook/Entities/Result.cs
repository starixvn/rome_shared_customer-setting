// ************************************************************************************
// <copyright file="Result.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Result.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Result
    {
        public Result()
        {
            ErrorCode = 0;
            ErrorMessage = "Successfull";
        }

        public Result(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public Dictionary<int, string> ErrorHandler { get; set; }
    }
}
