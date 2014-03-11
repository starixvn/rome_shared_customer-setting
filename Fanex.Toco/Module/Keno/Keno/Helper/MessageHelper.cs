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
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageHelper : IMessageHelper
    {
        private const string Separator = "\a";
        private const int NoDataIsChangedCode = 1000;
        private const int MessageIsNotValidCode = 1001;
        private string _errorCode = string.Empty;
        private string _errorMsg = string.Empty;
        private List<Result> _errorResults;

        public MessageHelper()
        {
            _errorResults = new List<Result>();
        }

        public MessageHelper(string errorCode, string errorMsg)
            : this()
        {
            _errorCode = errorCode;
            _errorMsg = errorMsg;
        }

        internal List<Result> ErrorResults
        {
            get { return _errorResults; }
        }

        public void SetErrorCodeAndErrorMessage(string errorCode, string errorMessage)
        {
            _errorCode = errorCode;
            _errorMsg = errorMessage;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <returns>The list of errors.</returns>
        public List<Result> GetErrors()
        {
            if (NoDataIsChanged())
            {
                AddNotificationMessageWhenClientDataIsNotChanged();
            }
            else if (MessageIsNotValid())
            {
                AddErrorMessageInCaseMessageIsNotValid();
            }
            else
            {
                AddErrorMessageWhenClientDataIsChanged();
                AddSuccessfulMessageInCaseThereIsNoError();
            }

            return _errorResults;
        }

        internal static string GetResourceFromErrorCode(string errorCode)
        {
            return errorCode;
        }

        internal static bool IfThisIsAnError(int errorCode)
        {
            return errorCode != 0;
        }

        internal bool NoDataIsChanged()
        {
            return string.IsNullOrEmpty(this._errorCode);
        }

        internal void AddErrorMessageWhenClientDataIsChanged()
        {
            var errorCodes = _errorCode.Split(new[] { Separator }, StringSplitOptions.None);
            var errorMsgs = _errorMsg.Split(new[] { Separator }, StringSplitOptions.None);

            for (int i = 0; i < errorCodes.Length; i++)
            {
                AddErrorMessage(Convert.ToInt32(errorCodes[i]), errorMsgs[i]);
            }
        }

        internal void AddSuccessfulMessageInCaseThereIsNoError()
        {
            if (_errorResults.Count == 0)
            {
                _errorResults.Add(new Result()
                {
                    ErrorCode = 0,
                    ErrorMessage = GetResourceFromErrorCode("0")
                });
            }
        }

        internal void AddErrorMessage(int errorCode, string errorMsg)
        {
            if (IfThisIsAnError(errorCode))
            {
                _errorResults.Add(new Result()
                {
                    ErrorCode = errorCode,
                    ErrorMessage = errorCode > 0 ? errorMsg : GetResourceFromErrorCode(errorCode.ToString())
                });
            }
        }

        internal void AddNotificationMessageWhenClientDataIsNotChanged()
        {
            _errorResults.Add(new Result()
            {
                ErrorCode = NoDataIsChangedCode,
                ErrorMessage = GetResourceFromErrorCode(NoDataIsChangedCode.ToString())
            });
        }

        /// <summary>
        /// Messages the is not valid.
        /// </summary>
        /// <returns>Whether the message is valid or not.</returns>
        internal bool MessageIsNotValid()
        {
            if (!string.IsNullOrEmpty(_errorCode) && string.IsNullOrEmpty(_errorMsg))
            {
                return true;
            }

            var errorCodes = _errorCode.Split(new[] { Separator }, StringSplitOptions.None);
            var errorMsgs = _errorMsg.Split(new[] { Separator }, StringSplitOptions.None);

            if (errorCodes.Length != errorMsgs.Length)
            {
                return true;
            }

            return false;
        }

        internal void AddErrorMessageInCaseMessageIsNotValid()
        {
            _errorResults.Add(new Result()
            {
                ErrorCode = MessageIsNotValidCode,
                ErrorMessage = GetResourceFromErrorCode(MessageIsNotValidCode.ToString())
            });
        }
    }
}