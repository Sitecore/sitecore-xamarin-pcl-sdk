

namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.TaskFlow;


    public class GetPublicKeyTasks : IRestApiCallTasks<string, Stream, PublicKeyX509Certificate>
    {
        #region Private Variables

        private readonly HttpClient httpClient;

        #endregion Private Variables

        public GetPublicKeyTasks(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #region IRestApiCallTasks

        public async Task<string> BuildRequestUrlForRequestAsync(string instanceUrl)
        {
            return await Task.Factory.StartNew(() => instanceUrl + "/-/item/v1/-/actions/getpublickey");
        }

        public async Task<Stream> SendRequestForUrlAsync(string requestUrl)
        {
            return await this.httpClient.GetStreamAsync(requestUrl);
        }

        // disposes httpData
        public async Task<PublicKeyX509Certificate> ParseResponseDataAsync(Stream httpData)
        {
            using (Stream publicKeyStream = httpData)
            {
                Func<PublicKeyX509Certificate> syncParsePublicKey = () =>
                {
                    return new PublicKeyXmlParser().Parse(publicKeyStream);
                };
                PublicKeyX509Certificate result = await Task.Factory.StartNew(syncParsePublicKey);
                return result;
            }
        }

        #endregion IRestApiCallTasks
    }
}

