

namespace Sitecore.MobileSDK.CrudTasks
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;
    using Sitecore.MobileSDK.PublicKey;


    public class GetItemsTasks : IRestApiCallTasks<ItemRequestConfig, HttpRequestMessage, string, ScItemsResponse>
    {
        public GetItemsTasks(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #region  IRestApiCallTasks
        public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(ItemRequestConfig request)
        {
            string url = this.UrlToGetItemWithRequest(request);
            HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

            result = await request.CredentialsCryptor.AddEncryptedCredentialHeadersAsync(result);
            return result;
        }

        public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl)
        {
            HttpResponseMessage httpResponse = await this.httpClient.SendAsync(requestUrl);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<ScItemsResponse> ParseResponseDataAsync(string data)
        {
            Func<ScItemsResponse> syncParseResponse = () =>
            {
                return ScItemsParser.Parse(data);
            };
            return await Task.Factory.StartNew(syncParseResponse);
        }

        #endregion IRestApiCallTasks

        private string UrlToGetItemWithRequest(ItemRequestConfig request)
        {
            //TODO : extract me
            string result = request.InstanceUrl + "/-/item/v1?" + "sc_itemid=" + Uri.EscapeDataString(request.Id);
            return result;

            //		    return "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v1?sc_itemid=%7B3D6658D8-A0BF-4E75-B3E2-D050FABCF4E1%7D";
        }

        private readonly HttpClient httpClient;
    }
}

