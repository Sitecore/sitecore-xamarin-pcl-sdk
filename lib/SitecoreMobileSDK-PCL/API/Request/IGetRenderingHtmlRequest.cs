
namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Items;

  /// <summary>
  /// Interface represents data set for rendering html requests.
  /// </summary>
  public interface IGetRenderingHtmlRequest
  {

    /// <summary>
    /// Rendering item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <value>
    /// The rendering item GUID.
    /// </value>
    string RenderingId { get; }

    /// <summary>
    /// Source item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <value>
    /// The source item GUID.
    /// </value>
    string SourceId { get; }

    /// <summary>
    /// Gets the item source.
    /// </summary>
    /// <value>
    /// The item source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource ItemSource { get; }

    /// <summary>
    /// Gets the session settings.
    /// </summary>
    /// <value>
    /// The session settings.
    /// </value>
    /// <seealso cref="ISessionConfig" />
    ISessionConfig SessionSettings { get; }

    /// <summary>
    /// Additional custom parameters set added to the rendering.
    /// key   - must contain parameter name.
    /// value - must contain parameter value.
    /// </summary>
    /// <returns>
    /// Parameter name, parameter value pairs.
    /// </returns>
    IDictionary<string, string> ParametersValuesByName { get; }

    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><seealso cref="IGetRenderingHtmlRequest"/></returns>
    IGetRenderingHtmlRequest DeepCopyGetRenderingHtmlRequest();

  }
}

