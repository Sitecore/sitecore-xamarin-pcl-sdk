

namespace MobileSDKUnitTest.Mock
{
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class MockGetItemsByPathParameters : IReadItemsByPathRequest
  {
    public MockGetItemsByPathParameters()
    {
      this.CopyInvocationCount = 0;
    }

    public IReadItemsByPathRequest DeepCopyGetItemByPathRequest()
    {
      ++this.CopyInvocationCount;

      MockGetItemsByPathParameters result = new MockGetItemsByPathParameters();
      result.ItemPath = this.ItemPath;

      if (null != this.ItemSource)
      {
        result.ItemSource = this.ItemSource.ShallowCopy();
      }

      if (null != this.SessionSettings)
      {
        result.SessionSettings = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null == this.QueryParameters)
      {
        result.QueryParameters = this.QueryParameters.DeepCopy();
      }

      return result;
    }

    public IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByPathRequest();
    }

    public IItemSource ItemSource { get; set; }

    public ISessionConfig SessionSettings { get; set; }

    public IQueryParameters QueryParameters { get; set; }

    public string ItemPath { get; set; }

    public int CopyInvocationCount { get; private set; }
  }
}
