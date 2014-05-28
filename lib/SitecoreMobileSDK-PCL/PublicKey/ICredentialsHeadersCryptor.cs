
namespace Sitecore.MobileSDK.PublicKey
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface ICredentialsHeadersCryptor
    {
        Task<HttpRequestMessage> AddEncryptedCredentialHeadersAsync(HttpRequestMessage httpRequest);
		HttpRequestMessage AddEncryptedCredentialHeaders (HttpRequestMessage httpRequest);
    }
}

