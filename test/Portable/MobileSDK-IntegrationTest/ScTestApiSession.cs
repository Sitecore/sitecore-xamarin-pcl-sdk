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

    public async Task<string> GetPublicKeyAsyncPublic(CancellationToken cancelToken = default(CancellationToken))
    {
      return await this.GetPublicKeyAsync(cancelToken);
    }



    protected override async Task<string> GetPublicKeyAsync(CancellationToken cancelToken = default(CancellationToken))
    {
      ++this.GetPublicKeyInvocationsCount;
      return await base.GetPublicKeyAsync(cancelToken);
    }

   

    public int GetPublicKeyInvocationsCount { get; private set; }
  }
}

