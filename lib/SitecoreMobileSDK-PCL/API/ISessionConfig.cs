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
    ///   <see cref="ISessionConfig" />
    /// </returns>
    ISessionConfig SessionConfigShallowCopy();

    /// <summary>
    /// Specifies URL to the Sitecore instance.
    /// </summary>
    string InstanceUrl
    {
      get;
    }

    /// <summary>
    /// Specifies site parameter, foe example '/sitecore/shell'.
    /// </summary>
    string Site
    {
      get;
    }

    /// <summary>
    /// Specifies WebAPI vertion, for example 'v1'.
    /// </summary>
    string ItemWebApiVersion
    {
      get;
    }

  }
}
