namespace Sitecore.MobileSDK.PublicKey
{
  using System;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;

  public interface ICredentialsHeadersCryptor : IDisposable
  {
    string AuthToken { get; }
    Task<HttpRequestMessage> AddEncryptedCredentialHeadersAsync(HttpRequestMessage httpRequest, CancellationToken cancelToken);
    HttpRequestMessage AddEncryptedCredentialHeaders(HttpRequestMessage httpRequest);
  }
}

