using NapApi.Client.Authentication;
using NapApi.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NapApi.Client.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = NapApiClient.UpdateDangerousLevel(new UpdateDangerousCustomerRequest()
            {
                ListUserName = "member01,member02, m1,m2m3,m5653",
                DangerousLevel = Enums.DangerousLevel.Level1,
                DangerousType = Enums.DangerousType.NonLive,
                UserName = "test"
            });

            Console.ReadLine();
        }
    }
}
