namespace Sitecore.MobileSDK.API.Request
{
  public interface IReadItemsByIdRequest : IBaseReadItemsRequest
  {
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    string ItemId { get; }
  }
}
