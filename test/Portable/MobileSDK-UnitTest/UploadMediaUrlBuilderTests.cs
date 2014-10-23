
namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  using NUnit.Framework;

  using Sitecore.MobileSDK.MockObjects;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.API.MediaItem;

  [TestFixture]
  public class UploadMediaUrlBuilderTests
  {
    private UploadMediaUrlBuilder builder;
    private UserRequestMerger requestMerger;

    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
      IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemWebApiVersion = "v234";
      mutableSessionConfig.InstanceUrl = "mobiledev1ua1.dk.sitecore.net:7119";
      mutableSessionConfig.Site = "/sitecore/shell";

      ItemSource source = LegacyConstants.DefaultSource();
      this.requestMerger = new UserRequestMerger(mutableSessionConfig, source);

      IMediaLibrarySettings mediaSettings = mutableSessionConfig;

      this.builder = new UploadMediaUrlBuilder (restGrammar, webApiGrammar, mutableSessionConfig, mediaSettings);
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
    }
      
    [Test]
    public void TestCorrectParamsForParentId()
    {
      Stream stream = this.GenerateFakeStream();
      IMediaResourceUploadRequest request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId("{BC1BAE61-ADC6-4B37-B36E-01059B26CF84}")
        .ContentType("image/jpg")
        .ItemName("name1")
        .FileName("bugaga.jpg")
        .ItemTemplatePath("System/Media/Unversioned/Image")
        .ItemDataStream(stream)
        .Build();

      IMediaResourceUploadRequest autocompletedRequest = this.requestMerger.FillUploadMediaGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
        "-/item/v234%2f" +
        "sitecore%2fshell" +
        "?" +
        "sc_database=web&" +
        "name=name1&" +
        "template=System%2fMedia%2fUnversioned%2fImage&" +
        "sc_itemid=%7BBC1BAE61-ADC6-4B37-B36E-01059B26CF84%7D";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestEmptyParentId()
    {
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId (""));
    }

    [Test]
    public void TestNullParentId()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId (null));
    }
   
    [Test]
    public void TestEmptyParentPath()
    {
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("");
      Assert.NotNull (builder, "empty path should be available");
    }

    [Test]
    public void TestNullParentPath()
    {
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(null);
      Assert.NotNull (builder, "null path should be available");
    }

    [Test]
    public void TestItemDataStreamRequired()
    {
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("/some folder/path")
        .ContentType("image/jpg")
        .ItemName("name1")
        .FileName("bugaga.jpg")
        .ItemTemplatePath("System/Media/Unversioned/Image");

      Assert.Throws<ArgumentNullException>(() => builder.Build());
    }

    [Test]
    public void TestFileNameRequired()
    {
      Stream stream = this.GenerateFakeStream();
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath ("/some folder/path")
        .ContentType ("image/jpg")
        .ItemName ("name1")
        .ItemDataStream (stream);

      Assert.Throws<ArgumentNullException>(() => builder.Build());
    }

    [Test]
    public void TestItemNameRequired()
    {
      Stream stream = this.GenerateFakeStream();
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath ("/some folder/path")
        .ContentType ("image/jpg")
        .FileName("bugaga.jpg")
        .ItemDataStream (stream);

      Assert.Throws<ArgumentNullException>(() => builder.Build());
    }

    [Test]
    public void TestContentTypeIsNotRequired()
    {
      Stream stream = this.GenerateFakeStream();
      var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath ("/some folder/path")
        .ItemName("name1")
        .FileName("bugaga.jpg")
        .ItemDataStream (stream)
        .Build();
     
      Assert.NotNull (request, "null Content type should be available");
    }
      
    [Test]
    public void TestEmptyFileName()
    {
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath ("/some folder/path");
      Assert.Throws<ArgumentException>(() => builder.FileName(""));
    }

    [Test]
    public void TestEmptyItemName()
    {
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath ("/some folder/path");
      Assert.Throws<ArgumentException>(() => builder.ItemName(""));
    }

    [Test]
    public void TestEmptyContentType()
    {
      var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath ("/some folder/path");
      Assert.Throws<ArgumentException>(() => builder.ContentType(""));
    }


    [Test]
    public void TestCorrectParamsForParentPath()
    {
      Stream stream = this.GenerateFakeStream();
      IMediaResourceUploadRequest request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("/some folder/path")
        .ContentType("image/jpg")
        .ItemName("name1")
        .FileName("bugaga.jpg")
        .ItemTemplatePath("System/Media/Unversioned/Image")
        .ItemDataStream(stream)
        .Build();

      IMediaResourceUploadRequest autocompletedRequest = this.requestMerger.FillUploadMediaGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
                        "-/item/v234%2f" +
                        "sitecore%2fshell" +
                        "%2fsome%20folder%2fpath" +
                        "?" +
                        "sc_database=web&" +
                        "name=name1&" +
                        "template=System%2fMedia%2fUnversioned%2fImage";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestTemplateIsOptionalParams()
    {
      Stream stream = this.GenerateFakeStream();
      IMediaResourceUploadRequest request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("/some folder/path")
        .ContentType("image/jpg")
        .ItemName("name1")
        .FileName("bugaga.jpg")
        .ItemDataStream(stream)
        .Build();

      IMediaResourceUploadRequest autocompletedRequest = this.requestMerger.FillUploadMediaGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
        "-/item/v234%2f" +
        "sitecore%2fshell" +
        "%2fsome%20folder%2fpath" +
        "?" +
        "sc_database=web&" +
        "name=name1";

      Assert.AreEqual(expected, result);
    }

    public Stream GenerateFakeStream()
    {
      MemoryStream stream = new MemoryStream();
      StreamWriter writer = new StreamWriter(stream);
      writer.Write("bla");
      writer.Flush();
      stream.Position = 0;
      return stream;
    }
  }
}
