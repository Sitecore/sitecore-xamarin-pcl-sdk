
namespace Sitecore.MobileSDK.API.Request
{
    public interface IReadItemsByIdRequest : IBaseGetItemRequest
  {
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    string ItemId { get; }
  }
}
