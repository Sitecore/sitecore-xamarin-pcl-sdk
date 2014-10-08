namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Items;



  [TestFixture]
  public class HashedUrlRequestBuilderTest
  {
    private MediaItemUrlBuilder builder;
    private MediaItemUrlBuilder builderWithShellSite;


    [SetUp]
    public void SetUp()
    {
      SessionConfigPOD mockConfig = new SessionConfigPOD();
      mockConfig.InstanceUrl = "localhost";
      mockConfig.ItemWebApiVersion = "v1";
      mockConfig.Site = null;

      var itemSource = new ItemSourcePOD("master", "fr");

      var mediaSettings = new MutableMediaLibrarySettings();

      this.builder = new MediaItemUrlBuilder(
        RestServiceGrammar.ItemWebApiV2Grammar(),
        WebApiUrlParameters.ItemWebApiV2UrlParameters(),
        mockConfig,
        mediaSettings,
        itemSource);


      SessionConfigPOD mockConfigWithShell = new SessionConfigPOD();
      mockConfigWithShell.InstanceUrl = "localhost";
      mockConfigWithShell.ItemWebApiVersion = "v1";
      mockConfigWithShell.Site = "/sitecore/shell";
      this.builderWithShellSite = new MediaItemUrlBuilder(
        RestServiceGrammar.ItemWebApiV2Grammar(),
        WebApiUrlParameters.ItemWebApiV2UrlParameters(),
        mockConfigWithShell,
        mediaSettings,
        itemSource);
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
      this.builderWithShellSite = null;
    }

    [Test]
    public void TestHashedUrlBuilderEncodesPreviousUrl()
    {
      var options = new DownloadMediaOptions();
      options.SetDisplayAsThumbnail(true);

      const string imagePath = "/images/green_mineraly1";
      string original = this.builder.BuildUrlStringForPath(imagePath, options);
      Assert.AreEqual("http://localhost/~/media/images/green_mineraly1.ashx?thn=1&db=master&la=fr", original);

      string result = this.builder.BuildUrlToRequestHashForPath(imagePath, options);
      string expected = "http://localhost/-/item/v1/-/actions/getsignedmediaurl?url=http%3a%2f%2flocalhost%2f~%2fmedia%2fimages%2fgreen_mineraly1.ashx%3fthn%3d1%26db%3dmaster%26la%3dfr";
      Assert.AreEqual(expected, result);
    }


    [Test]
    public void TestHashedUrlBuilderAddsShellSiteIfSpecified()
    {
      var options = new DownloadMediaOptions();
      options.SetDisplayAsThumbnail(true);

      const string imagePath = "/images/green_mineraly1";
      string original = this.builderWithShellSite.BuildUrlStringForPath(imagePath, options);
      Assert.AreEqual("http://localhost/~/media/images/green_mineraly1.ashx?thn=1&db=master&la=fr", original);

      string result = this.builderWithShellSite.BuildUrlToRequestHashForPath(imagePath, options);
      string expected = "http://localhost/-/item/v1%2fsitecore%2fshell/-/actions/getsignedmediaurl?url=http%3a%2f%2flocalhost%2f~%2fmedia%2fimages%2fgreen_mineraly1.ashx%3fthn%3d1%26db%3dmaster%26la%3dfr";
      Assert.AreEqual(expected, result);
    }
  }
}

