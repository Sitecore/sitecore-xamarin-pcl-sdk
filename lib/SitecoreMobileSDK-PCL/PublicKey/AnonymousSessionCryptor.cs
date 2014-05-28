
namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;


    public class AnonymousSessionCryptor : ICredentialsHeadersCryptor
    {
		public async Task<HttpRequestMessage> AddEncryptedCredentialHeadersAsync (HttpRequestMessage httpRequest)
        {
			return this.AddEncryptedCredentialHeaders(httpRequest);
        }

		public HttpRequestMessage AddEncryptedCredentialHeaders (HttpRequestMessage httpRequest)
		{
			return httpRequest;
		}
    }
}

