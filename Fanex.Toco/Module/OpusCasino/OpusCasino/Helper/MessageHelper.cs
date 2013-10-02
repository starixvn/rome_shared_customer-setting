using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Fanex.CustomerSetting.OpusCasino.Services;
using Fanex.PTRacing.BusinessObjects;

namespace Fanex.CustomerSetting.OpusCasino.Helper
{
    internal class MessageHelper
    {
       public static IList<Result> ParseError(string errorCode, string errorMsg)
        {
            var errorResults = new List<Result>();

            if (!string.IsNullOrEmpty(errorCode))
            {
                var arrErrorCode = Regex.Split(errorCode, "\a");
                var arrErrorMsgs = Regex.Split(errorMsg, "\a");
                if (arrErrorCode.Length > 0)
                {
                    var totalErrors = arrErrorCode.Length;
                    for (int i = 0; i < totalErrors; i++)
                    {
                        var errCode = Fanex.Common.Converter.ToInt(arrErrorCode[i]);
                        if (errCode == 0) { continue; }
                        var errMsg = errCode > 0
                                        ? arrErrorMsgs[i]
                                        : ResourceHelper.GetResource(errCode.ToString());

                        errorResults.Add(new Result()
                        {
                            ErrorCode = errCode.ToString(CultureInfo.InvariantCulture),
                            ErrorMessage = errMsg
                        });
                    }

                    if (errorResults.Count == 0)
                    {
                        errorResults.Add(new Result()
                        {
                            ErrorCode = "0",
                            ErrorMessage = ResourceHelper.GetResource("0")
                        });
                    }
                }
            }
            else
            {
                errorResults.Add(new Result()
                {
                    ErrorCode = "1000",
                    ErrorMessage = ResourceHelper.GetResource("Unchanged")
                });
            }
            return errorResults;
        }
    }
}