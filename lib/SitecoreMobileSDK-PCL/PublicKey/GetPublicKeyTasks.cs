

namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.TaskFlow;


	public class GetPublicKeyTasks : IRestApiCallTasks<string, string, Stream, PublicKeyX509Certificate>
    {
        #region Private Variables

        private readonly HttpClient httpClient;

        #endregion Private Variables

        public GetPublicKeyTasks(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #region IRestApiCallTasks

        public async Task<string> BuildRequestUrlForRequestAsync(string instanceUrl, CancellationToken cancelToken)
        {
            return await Task.Factory.StartNew(() => instanceUrl + "/-/item/v1/-/actions/getpublickey", cancelToken);
        }

        public async Task<Stream> SendRequestForUrlAsync(string requestUrl, CancellationToken cancelToken)
        {
            HttpResponseMessage httpResponse = await this.httpClient.GetAsync (requestUrl, cancelToken);
            HttpContent responseContent = httpResponse.Content;

            Stream result = await responseContent.ReadAsStreamAsync ();
            return result;
        }

        // disposes httpData
        public async Task<PublicKeyX509Certificate> ParseResponseDataAsync(Stream httpData, CancellationToken cancelToken)
        {
            using (Stream publicKeyStream = httpData)
            {
                Func<PublicKeyX509Certificate> syncParsePublicKey = () =>
                {
                    return new PublicKeyXmlParser().Parse(publicKeyStream, cancelToken);
                };
                PublicKeyX509Certificate result = await Task.Factory.StartNew(syncParsePublicKey, cancelToken);
                return result;
            }
        }

        #endregion IRestApiCallTasks
    }
}

