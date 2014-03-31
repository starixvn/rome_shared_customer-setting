// --------------------------------------------------------------------------
// <copyright file="MessageHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Helpers
{
    using System;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageHelper : IMessageHelper
    {
        private const string Separator = "\a";
        private const int DataIsNotChangedCode = 1000;
        private const int DatabaseResponseIsNotValidCode = 1001;
        private const string MessageFormat = "Msg_{0}";
        private string _errorCode = string.Empty;
        private string _errorMsg = string.Empty;
        private IList<Result> _errorResults;

        public MessageHelper()
        {
        }

        public MessageHelper(string errorCode, string errorMessage)
            : this()
        {
            SetErrorCodeAndErrorMessage(errorCode, errorMessage);
        }

        internal IList<Result> ErrorResults
        {
            get { return _errorResults; }
        }

        public void SetErrorCodeAndErrorMessage(string errorCode, string errorMessage)
        {
            _errorResults = new List<Result>();
            _errorCode = errorCode;
            _errorMsg = errorMessage;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <returns>The list of errors.</returns>
        public IList<Result> GetErrors()
        {
            if (DataIsNotChangedOrNotUpdated())
            {
                AddMessageWhenDataIsNotChangedOrNotUpdated();
            }
            else if (DatabaseResponseIsNotValid())
            {
                AddMessageInCaseDatabaseResponseIsNotValid();
            }
            else
            {
                AddMessageWhenDataIsUpdated();
            }

            return _errorResults;
        }

        internal static string GetResourceFromErrorCode(string errorCode)
        {
            string result = Messages.ResourceManager.GetString(string.Format(MessageFormat, errorCode));
            return !string.IsNullOrEmpty(result) ? result : errorCode;
        }

        internal static bool IfErrorCodeIsValid(string errorCode)
        {
            int formattedErrorCode = 0;
            return int.TryParse(errorCode, out formattedErrorCode);
        }

        internal bool DataIsNotChangedOrNotUpdated()
        {
            return string.IsNullOrEmpty(this._errorCode);
        }

        internal void AddMessageWhenDataIsUpdated()
        {
            var errorCodes = _errorCode.Split(new[] { Separator }, StringSplitOptions.None);
            var errorMsgs = _errorMsg.Split(new[] { Separator }, StringSplitOptions.None);

            for (int i = 0; i < errorCodes.Length; i++)
            {
                AddErrorMessage(errorCodes[i], errorMsgs[i]);
            }
        }

        internal void AddErrorMessage(string errorCode, string errorMsg)
        {
            if (IfErrorCodeIsValid(errorCode))
            {
                _errorResults.Add(new Result()
                {
                    ErrorCode = Convert.ToInt32(errorCode),
                    ErrorMessage = !string.IsNullOrEmpty(errorMsg) ? errorMsg : GetResourceFromErrorCode(errorCode)
                });
            }
        }

        internal void AddMessageWhenDataIsNotChangedOrNotUpdated()
        {
            _errorResults.Add(new Result()
            {
                ErrorCode = DataIsNotChangedCode,
                ErrorMessage = GetResourceFromErrorCode(DataIsNotChangedCode.ToString())
            });
        }

        /// <summary>
        /// Database response is not valid.
        /// </summary>
        /// <returns>
        /// Whether the database response is valid or not.
        /// </returns>
        internal bool DatabaseResponseIsNotValid()
        {
            var errorCodes = _errorCode.Split(new[] { Separator }, StringSplitOptions.None);
            var errorMsgs = _errorMsg.Split(new[] { Separator }, StringSplitOptions.None);

            if (errorCodes.Length != errorMsgs.Length)
            {
                return true;
            }

            return false;
        }

        internal void AddMessageInCaseDatabaseResponseIsNotValid()
        {
            _errorResults.Add(new Result()
            {
                ErrorCode = DatabaseResponseIsNotValidCode,
                ErrorMessage = GetResourceFromErrorCode(DatabaseResponseIsNotValidCode.ToString())
            });
        }
    }
}