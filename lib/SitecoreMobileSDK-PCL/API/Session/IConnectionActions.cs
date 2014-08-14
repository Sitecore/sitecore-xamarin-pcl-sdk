namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>
  /// Interface represnets connection actions like authenticate.
  /// </summary>
  public interface IConnectionActions
  {
    /// <summary>
    /// Performs asynchronous authentication.
    /// </summary>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns><see cref="Task{TResult}"/></returns>
    Task<bool> AuthenticateAsync(CancellationToken cancelToken = default(CancellationToken));
  }
}

