using NapApi.Client.Authentication;
using NapApi.Client.Configurations;
using NapApi.Client.Exceptions;
using NapApi.Client.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace NapApi.Client
{
    public class NapApiClient
    {
        public static List<UpdateDangerousCustomerResponse> UpdateDangerousLevel(UpdateDangerousCustomerRequest request)
        {
            CustomDelegatingHandler customDelegatingHandler = new CustomDelegatingHandler();
            HttpClient client = HttpClientFactory.Create(customDelegatingHandler);
            string apiBaseAddress = NapApiConfiguration.Settings.ServiceUris;
            HttpResponseMessage response = client.PostAsJsonAsync(apiBaseAddress + "DangerousCustomer/UpdateDangerousCustomer", request).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new NapApiClientException("Unhandled Exception was throw. Request ID: .Please contact Api's owner for help.");
            }

            if (Is404Error(response))
            {
                throw new HttpException(404, "Uri not found");
            }

            if (!IsAuthorized(response))
            {
                throw new NapApiClientUnauthorizedException("Unauthorized Request. Please check your Secret Key or contact Api's owner for help.");
            }

            return JsonConvert.DeserializeObject<List<UpdateDangerousCustomerResponse>>(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// NAP has private bussiness that redirect to Index.aspx when got 404 error, so we never get 404, instead we got Index.aspx details.
        /// Must hard code to check whether the request is redirect to Index.aspx beside Status Code to recognize Api 404 error.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static bool Is404Error(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode && response.RequestMessage.RequestUri.ToString().Contains("Index.aspx");
        }

        /// <summary>
        /// Because cannot implement ApiController on NAP due to NewtonsoftJson 4.5.0.0. NAP is not upgradeabled to that version.
        /// When a request is not authorize, then the response will redirect to SignIn.aspx page.
        /// This method check for Uri contains SignIn.asps to recognize UnAuthorized Request instead base on Status Code.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static bool IsAuthorized(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode && !response.RequestMessage.RequestUri.ToString().Contains("SignIn.aspx");
        }
    }
}
