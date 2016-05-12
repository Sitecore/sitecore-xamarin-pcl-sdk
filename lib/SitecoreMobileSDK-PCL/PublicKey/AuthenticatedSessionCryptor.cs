using System.Net;

namespace Sitecore.MobileSDK.PublicKey
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Net.Http;

  // Do not store references to this class
  // Instantiate it only in ```using () {} ``` blocks
  public class AuthenticatedSessionCryptor : ICredentialsHeadersCryptor
  {
    private string password;
    private string login;
    private string certificate;


    public void Dispose()
    {
      this.login = null;
      this.password = null;
      this.certificate = null;
      this.AuthToken = null;
    }

    public string AuthToken { get; private set; }

    public AuthenticatedSessionCryptor(string login, string password, string certificate)
    {
      //      TODO: validate params
      this.login = login;
      this.password = password;
      this.certificate = certificate;
    }

    public async Task<HttpRequestMessage> AddEncryptedCredentialHeadersAsync(HttpRequestMessage httpRequest, CancellationToken cancelToken)
    {
      Func<HttpRequestMessage> setupEncryptedHeaders = () =>
      {
        return this.AddEncryptedCredentialHeaders(httpRequest);
      };
      HttpRequestMessage requestMessage = await Task.Factory.StartNew(setupEncryptedHeaders, cancelToken);
      return requestMessage;
    }

    public HttpRequestMessage AddEncryptedCredentialHeaders(HttpRequestMessage httpRequest)
    {
      //TODO: @igk exclude this
      #if !ENCRYPTION_DISABLED
     // EncryptionUtil cryptor = new EncryptionUtil(this.certificate);
//      var encryptedLogin = cryptor.Encrypt(this.login);
//      var encryptedPassword = cryptor.Encrypt(this.password);
      #else
      var encryptedLogin = this.login;
      var encryptedPassword = this.password;
      #endif

//      httpRequest.Headers.Add("Cookie", "cookie1=value1; cookie2=value2");


//      httpRequest.Headers.Add("X-Scitemwebapi-Username", encryptedLogin);
//      httpRequest.Headers.Add("X-Scitemwebapi-Password", encryptedPassword);
//
//      #if !ENCRYPTION_DISABLED
//      httpRequest.Headers.Add("X-Scitemwebapi-Encrypted", "1");
//      #endif

      return httpRequest;
    }
  }
}

