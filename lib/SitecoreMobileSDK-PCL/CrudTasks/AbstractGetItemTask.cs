

namespace Sitecore.MobileSDK.CrudTasks
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.TaskFlow;
    using Sitecore.MobileSDK.PublicKey;


    public abstract class AbstractGetItemTask<TRequest> : IRestApiCallTasks<TRequest, HttpRequestMessage, string, ScItemsResponse>
        where TRequest: class
    {
        private AbstractGetItemTask()
        {
        }

        public AbstractGetItemTask(HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
        {
            this.httpClient = httpClient;
            this.credentialsHeadersCryptor = credentialsHeadersCryptor;

            this.Validate ();
        }

        #region  IRestApiCallTasks

        public async Task<HttpRequestMessage> BuildRequestUrlForRequestAsync(TRequest request, CancellationToken cancelToken)
        {
            string url = this.UrlToGetItemWithRequest(request);
            HttpRequestMessage result = new HttpRequestMessage(HttpMethod.Get, url);

            result = await this.credentialsHeadersCryptor.AddEncryptedCredentialHeadersAsync(result, cancelToken);
            return result;
        }

        public async Task<string> SendRequestForUrlAsync(HttpRequestMessage requestUrl, CancellationToken cancelToken)
        {
			//TODO: @igk debug request output, remove later
			Debug.WriteLine("REQUEST: " + requestUrl);
            HttpResponseMessage httpResponse = await this.httpClient.SendAsync(requestUrl, cancelToken);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        public async Task<ScItemsResponse> ParseResponseDataAsync(string data, CancellationToken cancelToken)
        {
            Func<ScItemsResponse> syncParseResponse = () =>
            {
				//TODO: @igk debug response output, remove later
				Debug.WriteLine("RESPONSE: " + data);
                return ScItemsParser.Parse(data, cancelToken);
            };
            return await Task.Factory.StartNew(syncParseResponse, cancelToken);
        }

        #endregion IRestApiCallTasks

        private void Validate()
        {
            if (null == this.httpClient)
            {
                throw new ArgumentNullException ("AbstractGetItemTask.httpClient cannot be null");
            }
            else if (null == this.credentialsHeadersCryptor)
            {
                throw new ArgumentNullException ("AbstractGetItemTask.credentialsHeadersCryptor cannot be null");
            }
        }

        protected abstract string UrlToGetItemWithRequest (TRequest request);


        private HttpClient httpClient;
        private ICredentialsHeadersCryptor credentialsHeadersCryptor;
    }
}




