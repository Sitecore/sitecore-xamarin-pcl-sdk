namespace Sitecore.MobileSDK.API.Session
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  public interface ISitecoreWebApiSessionState : IDisposable
  {
    IItemSource DefaultSource { get; }
    ISessionConfig Config { get; }
    IWebApiCredentials Credentials { get; }
  }
}

