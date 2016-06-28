// --------------------------------------------------------------------------
// <copyright file="IMessageHelper.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Helpers
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;

    public interface IMessageHelper
    {
        void SetErrorCodeAndErrorMessage(string errorCode, string errorMessage);

        IList<Result> GetErrors();
    }
}