using Sitecore.MobileSDK.UrlBuilder;

namespace Sitecore.MobileSDK.CrudTasks
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;


    public class GetItemsByIdTasks : IRestApiCallTasks<GetItemsByIdParameters, HttpRequestMessage, string, ScItemsResponse>
    {
        public GetItemsByIdTasks(ItemByIdUrlBuilder urlBuilder, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.urlBuilder = urlBuilder;
        }

        #region  IRestApiCallTasks

        public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(GetItemsByIdParameters request)
        {
            string url = this.UrlToGetItemWithRequest(request);
            HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

            result = await request.CredentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result);
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

        private string UrlToGetItemWithRequest(GetItemsByIdParameters request)
        {
            return this.urlBuilder.GetUrlForRequest(request);
        }

        private readonly HttpClient httpClient;
        private readonly ItemByIdUrlBuilder urlBuilder;
    }
}

