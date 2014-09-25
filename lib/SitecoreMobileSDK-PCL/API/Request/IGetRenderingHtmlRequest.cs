namespace Sitecore.MobileSDK.API.Request
{
  public interface IGetRenderingHtmlRequest : IBaseItemRequest
  {

    string RenderingId { get; }
    string SourceId { get; } // @igk, same as itemId!!!

    IGetRenderingHtmlRequest DeepCopyGetRenderingHtmlRequest();

    //TODO: @igk we do not need "IQueryParameters QueryParameters { get; }" here, from IBaseItemRequest, 
    //TODO: @igk probably we need new main request?
  }
}

