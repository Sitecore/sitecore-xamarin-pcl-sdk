namespace Sitecore.MobileSDK.API.Session
{
  using System;
  using Sitecore.MobileSDK.API.Items;

  public interface ISitecoreWebApiSessionState : IDisposable
  {
    IItemSource DefaultSource { get; }
    ISessionConfig Config { get; }
    IWebApiCredentials Credentials { get; }
  }
}

