namespace Sitecore.MobileSDK.API.Session
{
  using System;
  using Sitecore.MobileSDK.API.Items;

  /// <summary>
  /// Interface represents session state.
  /// </summary>
  public interface ISitecoreWebApiSessionState : IDisposable
  {
    /// <summary>
    /// Gets the default source.
    /// </summary>
    /// <value>
    /// The default source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource DefaultSource { get; }

    /// <summary>
    /// Gets the sessiobn configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    /// <seealso cref="ISessionConfig"/>
    ISessionConfig Config { get; }

    /// <summary>
    /// Gets the credentials.
    /// </summary>
    /// <value>
    /// The credentials.
    /// </value>
    /// <seealso cref="IWebApiCredentials" />
    IWebApiCredentials Credentials { get; }
  }
}

