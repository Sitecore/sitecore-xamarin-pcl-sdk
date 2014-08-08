namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;

  public interface IConnectionActions
  {
    Task<bool> AuthenticateAsync(CancellationToken cancelToken = default(CancellationToken));
  }
}

