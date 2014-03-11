// --------------------------------------------------------------------------
// <copyright file="IMessageHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Helper
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;

    public interface IMessageHelper
    {
        void SetErrorCodeAndErrorMessage(string errorCode, string errorMessage);

        List<Result> GetErrors();
    }
}