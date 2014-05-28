

namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;


    public class AuthenticedSessionCryptor : ICredentialsHeadersCryptor
    {
        Task<HttpRequestMessage> AddEncryptedCredentialHeaders(HttpRequestMessage httpRequest)
        {
        }
    }
}

