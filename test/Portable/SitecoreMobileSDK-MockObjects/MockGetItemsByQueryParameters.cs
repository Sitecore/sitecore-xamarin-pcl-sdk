

namespace MobileSDKUnitTest.Mock
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class MockGetItemsByQueryParameters : IReadItemsByQueryRequest
  {
    public MockGetItemsByQueryParameters()
    {
      this.CopyInvocationCount = 0;
    }

    public virtual IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest()
    {
      ++this.CopyInvocationCount;

      MockGetItemsByQueryParameters result = new MockGetItemsByQueryParameters();
      result.SitecoreQuery = this.SitecoreQuery;

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

    public virtual IBaseGetItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByQueryRequest();
    }

    public IItemSource ItemSource { get; set; }

    public ISessionConfig SessionSettings { get; set; }

    public IQueryParameters QueryParameters { get; set; }

    public string SitecoreQuery { get; set; }

    public int CopyInvocationCount { get; private set; }
  }
}

