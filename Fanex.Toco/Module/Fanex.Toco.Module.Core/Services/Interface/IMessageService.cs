using System.Collections.Generic;
using System.Data;
using Fanex.CustomerSetting.Module.Core.Models;

namespace Fanex.CustomerSetting.Module.Core.Services.Interface
{
    public interface IMessageService
    {
        Result SendPrivateMsgToUsers(DataTable dtMsg, string matchId, Message pmsg, ref List<string> listTransIdDone);
    }
}
