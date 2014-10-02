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
    public void TestPagingSettingsCanBeCalledOnlyOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
      {
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/A/B/C/D/E")
          .AddScope(ScopeType.Self)
          .PageNumber(5)
          .ItemsPerPage(10)
          .PageNumber(1);
      });
    }

    [Test]
    public void TestPagingSettingsUsedForSelfScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/A/B/C/D/E")
        .AddScope(ScopeType.Self)
        .PageNumber(5)
        .ItemsPerPage(10)
        .Build();

      Assert.IsFalse(request.QueryParameters.ScopeParameters.ChildrenScopeIsSet);
      Assert.IsFalse(request.QueryParameters.ScopeParameters.ParentScopeIsSet);
      Assert.IsTrue(request.QueryParameters.ScopeParameters.SelfScopeIsSet);

      Assert.AreEqual(5, request.PagingSettings.PageNumber);
      Assert.AreEqual(10, request.PagingSettings.ItemsPerPageCount);
    }

    [Test]
    public void TestPagingSettingsUsedForOmittedScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/x/y/z/*")
        .PageNumber(44)
        .ItemsPerPage(5)
        .Build();

      Assert.AreEqual(44, request.PagingSettings.PageNumber);
      Assert.AreEqual(5, request.PagingSettings.ItemsPerPageCount);
    }
  }
}

