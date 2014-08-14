namespace Sitecore.MobileSDK.API.Session
{
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents media actions that actions on media items.
  /// </summary>
  public interface IMediaActions
  {
    /// <summary>
    /// Downloads media item asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    /// The <see cref="Task{Stream}" />.
    /// </returns>
    Task<Stream> DownloadResourceAsync(IMediaResourceDownloadRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

