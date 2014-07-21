namespace Sitecore.MobileSDK.Items.Delete
{
  public interface IDeleteItemsByQueryRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest();
    string SitecoreQuery { get; }
  }
}
