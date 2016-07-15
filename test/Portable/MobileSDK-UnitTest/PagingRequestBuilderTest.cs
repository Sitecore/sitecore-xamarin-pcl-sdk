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
      Assert.Throws<InvalidOperationException>(() =>
      {
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/A/B/C/D/E")
          .PageNumber(5)
          .ItemsPerPage(10)
          .PageNumber(1);
      });
    }

    [Test]
    public void TestPagingSettingsUsedForOmittedScopeAndSingleItemResponse()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath("/x/y/z/*")
        .PageNumber(44)
        .ItemsPerPage(5)
        .Build();

      Assert.AreEqual(44, request.PagingSettings.PageNumber);
      Assert.AreEqual(5, request.PagingSettings.ItemsPerPageCount);
    }
  }
}

