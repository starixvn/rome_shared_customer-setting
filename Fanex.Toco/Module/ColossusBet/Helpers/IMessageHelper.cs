namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Helpers
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;

    public interface IMessageHelper
    {
        void SetErrorCodeAndErrorMessage(string errorCode, string errorMessage);

        IList<Result> GetErrors();
    }
}