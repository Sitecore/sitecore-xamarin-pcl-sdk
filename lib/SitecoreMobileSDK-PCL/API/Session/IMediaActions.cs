namespace Sitecore.MobileSDK.API.Session
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.UrlBuilder.MediaItem;

    public interface IMediaActions
  {
    Task<Stream> DownloadResourceAsync(IReadMediaItemRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

