namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Collections.Generic;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  using Sitecore.MobileSDK.SessionSettings;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.UrlBuilder.RenderingHtml;


  [TestFixture]
  public class GetHtmlRenderingUrlBuilderTests
  {
    private RenderingHtmlUrlBuilder builder;
    private UserRequestMerger requestMerger;

    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
      IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

      this.builder = new RenderingHtmlUrlBuilder(restGrammar, webApiGrammar);

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemWebApiVersion = "v234";
      mutableSessionConfig.InstanceUrl = "mobiledev1ua1.dk.sitecore.net:7119";
      mutableSessionConfig.Site = "/sitecore/shell";

      ItemSource source = LegacyConstants.DefaultSource();
      this.requestMerger = new UserRequestMerger(mutableSessionConfig, source);

    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
    }
      
    [Test]
    public void TestDefaultHTMLRendering()
    {
      IGetRenderingHtmlRequest request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}", "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}")
        .Build();

      IGetRenderingHtmlRequest autocompletedRequest = this.requestMerger.FillGetRenderingHtmlGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
        "-/item/v234" +
        "%2fsitecore%2fshell/" +
        "-/actions/getrenderinghtml?" +
        "sc_database=web&" +
        "language=en&" +
        "sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d&" +
        "renderingid=%7b220d559f-dea5-42ea-9c1c-8a5df7e70e22%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestHTMLRenderingWithEmptySourceId()
    {
      TestDelegate action = () =>  ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("", "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}");
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestHTMLRenderingWithNullSourceId()
    {

      TestDelegate action = () =>  ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(null, "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}");
      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestHTMLRenderingWithEmptyRederingId()
    {
      TestDelegate action = () =>  ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}", "");
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestHTMLRenderingWithNullRederingId()
    {
      TestDelegate action = () =>  ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}", null);
      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestHTMLRenderingWithWrongFormatSourceId()
    {
      TestDelegate action = () =>  ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("dsdfsdfsf", "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}");
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestHTMLRenderingWithWrongFormatRederingId()
    {
      TestDelegate action = () =>  ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}", "dfdfdf");
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestDefaultHTMLRenderingWithAdditionalParameters()
    {
      IGetRenderingHtmlRequest request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}", "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}")
        .SourceAndRenderingDatabase("web")
        .SourceAndRenderingLanguage("bla")
        .SourceVersion(99)
        .Build();

      IGetRenderingHtmlRequest autocompletedRequest = this.requestMerger.FillGetRenderingHtmlGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
        "-/item/v234" +
        "%2fsitecore%2fshell/" +
        "-/actions/getrenderinghtml?" +
        "sc_database=web&" +
        "language=bla&" +
        "sc_itemversion=99&" +
        "sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d&" +
        "renderingid=%7b220d559f-dea5-42ea-9c1c-8a5df7e70e22%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDefaultHTMLRenderingwithCustomParameters()
    {
      Dictionary<string, string> paramDict = new Dictionary<string, string> ();
      paramDict.Add ("param3", "value3");
      paramDict.Add ("param4", "value4");

      IGetRenderingHtmlRequest request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId ("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}", "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}")
        .AddRenderingParameterNameValue("param1", "value1")
        .AddRenderingParameterNameValue("param2", "value2")
        .AddRenderingParameterNameValue(paramDict)
        .Build();

      IGetRenderingHtmlRequest autocompletedRequest = this.requestMerger.FillGetRenderingHtmlGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
        "-/item/v234" +
        "%2fsitecore%2fshell/" +
        "-/actions/getrenderinghtml?" +
        "sc_database=web&" +
        "language=en&" +
        "sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d&" +
        "renderingid=%7b220d559f-dea5-42ea-9c1c-8a5df7e70e22%7d&" +
        "param1=value1&" +
        "param2=value2&" +
        "param3=value3&" +
        "param4=value4";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDefaultHTMLRenderingCustomParametersIsCaseSensitive()
    {

      IGetRenderingHtmlRequest request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId ("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}", "{220D559F-DEA5-42EA-9C1C-8A5DF7E70E22}")
        .AddRenderingParameterNameValue("PaRam", "VALue")
        .Build();

      IGetRenderingHtmlRequest autocompletedRequest = this.requestMerger.FillGetRenderingHtmlGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/" +
        "-/item/v234" +
        "%2fsitecore%2fshell/" +
        "-/actions/getrenderinghtml?" +
        "sc_database=web&" +
        "language=en&" +
        "sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d&" +
        "renderingid=%7b220d559f-dea5-42ea-9c1c-8a5df7e70e22%7d&" +
        "PaRam=VALue";

      Assert.AreEqual(expected, result);
    }

  }
}
