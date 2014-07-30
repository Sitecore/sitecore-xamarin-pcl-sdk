namespace Sitecore.MobileSDK.PublicKey
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Net.Http;


  // TODO : use secure string
  public class AuthenticedSessionCryptor : ICredentialsHeadersCryptor
  {
    public AuthenticedSessionCryptor (string login, string password, PublicKeyX509Certificate certificate)
    {
      //      TODO: validate params
      this.login = login;
      this.password = password;
      this.certificate = certificate;
    }

    public async Task<HttpRequestMessage> AddEncryptedCredentialHeadersAsync (HttpRequestMessage httpRequest, CancellationToken cancelToken)
    {
      Func<HttpRequestMessage> setupEncryptedHeaders = () =>
      {
        return this.AddEncryptedCredentialHeaders( httpRequest );
      };
      HttpRequestMessage requestMessage = await Task.Factory.StartNew (setupEncryptedHeaders, cancelToken);
      return requestMessage;
    }

    public HttpRequestMessage AddEncryptedCredentialHeaders (HttpRequestMessage httpRequest)
    {
      EncryptionUtil cryptor = new EncryptionUtil (this.certificate);

      var encryptedLogin = cryptor.Encrypt (this.login);
      var encryptedPassword = cryptor.Encrypt (this.password);

      httpRequest.Headers.Add ("X-Scitemwebapi-Username", encryptedLogin);
      httpRequest.Headers.Add ("X-Scitemwebapi-Password", encryptedPassword);
      httpRequest.Headers.Add ("X-Scitemwebapi-Encrypted", "1");

      return httpRequest;

    }

    private readonly string password;
    private readonly string login;
    private PublicKeyX509Certificate certificate;
  }
}

