
namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;


    public class AnonymousSessionCryptor : ICredentialsHeadersCryptor
    {
        public Task<HttpRequestMessage> AddEncryptedCredentialHeaders (HttpRequestMessage httpRequest)
        {
            return httpRequest;
        }
    }
}

