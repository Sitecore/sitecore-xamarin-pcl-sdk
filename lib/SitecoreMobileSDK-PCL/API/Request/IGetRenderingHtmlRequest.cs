
namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Items;

  public interface IGetRenderingHtmlRequest
  {

    string RenderingId { get; }
    string SourceId { get; } // @igk, same as itemId!!!

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

    IDictionary<string, string> ParametersValuesByName { get; }

    IGetRenderingHtmlRequest DeepCopyGetRenderingHtmlRequest();

    //TODO: @igk we do not need "IQueryParameters QueryParameters { get; }" here, from IBaseItemRequest, 
    //TODO: @igk probably we need new main request?
  }
}

