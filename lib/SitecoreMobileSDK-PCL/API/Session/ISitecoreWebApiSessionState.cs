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
    /// Gets the default settings: database name, language and item version number.
    /// </summary>
    /// <value>
    /// The default source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource DefaultSource { get; }

    /// <summary>
    /// Gets the session settings: instance url, site, web API version.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    /// <seealso cref="ISessionConfig"/>
    ISessionConfig Config { get; }

    /// <summary>
    /// Gets the credentials: user name and password.
    /// </summary>
    /// <value>
    /// The credentials.
    /// </value>
    /// <seealso cref="IWebApiCredentials" />
    IWebApiCredentials Credentials { get; }
  }
}

