namespace Sitecore.MobileSDK.API.Request
{
  public interface IBaseCreateItemRequest : IBaseChangeItemRequest
  {
    string ItemName { get; }
    string ItemTemplate { get; }
  }
}

