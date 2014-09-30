namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;


  [TestFixture]
  public class PagingRequestBuilderTest
  {
    [Test]
    public void TestPagingSettingsCanBeReordered()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dynamo}")
        .AddScope(ScopeType.Children)
        .PageNumber(2)
        .ItemsPerPage(5)
        .Build();
    }

    [Test]
    public void TestPagingSettingsUsedForSelfScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{karpaty}")
        .AddScope(ScopeType.Self)
        .PageNumber(5)
        .ItemsPerPage(10)
        .Build();
    }

    [Test]
    public void TestPagingSettingsUsedForOmittedScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{karpaty}")
        .PageNumber(44)
        .ItemsPerPage(5)
        .Build();
    }
  }
}

