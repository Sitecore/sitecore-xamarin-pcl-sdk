using System;
using Sitecore.MobileSDK.PublicKey;
using System.Threading.Tasks;
using System.Net.Http;

namespace Sitecore.MobileSDK
{
    public class AnonymousSessionCryptor : ICredentialsHeadersCryptor
    {
        public Task<HttpRequestMessage> AddEncryptedCredentialHeaders (HttpRequestMessage httpRequest)
        {
            return httpRequest;
        }
    }
}

