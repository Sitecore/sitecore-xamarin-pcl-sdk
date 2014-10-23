namespace MobileSDKIntegrationTest
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PasswordProvider.Interface;
  using Sitecore.MobileSDK.PublicKey;

  public class ScTestApiSession : ScApiSession
  {
    public ScTestApiSession(
      ISessionConfig config,
      IWebApiCredentials credentials,
      IMediaLibrarySettings mediaSettings,
      ItemSource defaultSource = null) :
      base(config, credentials, mediaSettings, defaultSource)
    {
      this.GetPublicKeyInvocationsCount = 0;
    }

    public async Task<PublicKeyX509Certificate> GetPublicKeyAsyncPublic(CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.GetPublicKeyAsync(cancelToken);
    }

    public async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsyncPublic(CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.GetCredentialsCryptorAsync(cancelToken);
    }


    protected override async Task<PublicKeyX509Certificate> GetPublicKeyAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      ++this.GetPublicKeyInvocationsCount;
      return await base.GetPublicKeyAsync(cancelToken);
    }

    protected override async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      return await base.GetCredentialsCryptorAsync(cancelToken);
    }

    public int GetPublicKeyInvocationsCount { get; private set; }
  }
}

