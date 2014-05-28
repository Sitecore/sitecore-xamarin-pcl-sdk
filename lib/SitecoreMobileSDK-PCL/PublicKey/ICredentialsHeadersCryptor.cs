
namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface ICredentialsHeadersCryptor
    {
        Task<HttpRequestMessage> AddEncryptedCredentialHeaders(HttpRequestMessage httpRequest);
    }
}

