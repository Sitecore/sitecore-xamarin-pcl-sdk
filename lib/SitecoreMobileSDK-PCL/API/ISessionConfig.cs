namespace Sitecore.MobileSDK.API
{
  /// <summary>
  /// Interface represents settings that specifies the rules to build requests. 
  /// </summary>
  public interface ISessionConfig
  {

    /// <summary>
    /// Performs shallow copy of session setting.
    /// </summary>
    /// <returns>
    /// this instance copy.<see cref="ISessionConfig" />
    /// </returns>
    ISessionConfig SessionConfigShallowCopy();

    /// <summary>
    /// Specifies URL to the Sitecore instance, must starts with "http://" prefix.
    /// 
    /// The value is case insensitive.
    /// </summary>
    string InstanceUrl
    {
      get;
    }

    /// <summary>
    /// Specifies site parameter, must starts with "/" symbol.
    /// For example: "/sitecore/shell".
    /// 
    /// The value is case insensitive.
    /// </summary>
    string Site
    {
      get;
    }

  }
}
