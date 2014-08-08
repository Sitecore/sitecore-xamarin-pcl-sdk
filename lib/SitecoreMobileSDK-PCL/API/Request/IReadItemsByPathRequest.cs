namespace Sitecore.MobileSDK.API.Request
{
  public interface IReadItemsByPathRequest : IBaseReadItemsRequest
  {
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    string ItemPath { get; }
  }
}
