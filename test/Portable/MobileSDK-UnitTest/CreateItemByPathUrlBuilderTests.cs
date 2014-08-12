namespace Sitecore.MobileSdkUnitTest
{
  using NUnit.Framework;
  using System;
  using System.Collections.Generic;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Items;


  [TestFixture()]
  public class CreateItemByPathUrlBuilderTests
  {
    private CreateItemByPathUrlBuilder builder;
    private UserRequestMerger requestMerger;

    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
      IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

      this.builder = new CreateItemByPathUrlBuilder(restGrammar, webApiGrammar);

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemWebApiVersion = "v234";
      mutableSessionConfig.InstanceUrl = "mobiledev1ua1.dk.sitecore.net:7119";
      mutableSessionConfig.Site = "/sitecore/shell";

      ItemSource source = LegacyConstants.DefaultSource();
      this.requestMerger = new UserRequestMerger(mutableSessionConfig, source);

    }

    [Test]
    public void TestFieldsAppending()
    {
      Dictionary<string,string> fields = new Dictionary<string,string>();
      fields.Add("field1","VaLuE1");
      fields.Add("field2","VaLuE2");

      ICreateItemByPathRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithParentPath("/sitecore/content/home")
        .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .Database("db")
        .Language("lg")
        .Payload(PayloadType.Full)
        .AddFieldsRawValuesByName(fields)
        .AddFieldsRawValuesByName("field3","VaLuE3")
        .Build();

      ICreateItemByPathRequest autocompletedRequest = this.requestMerger.FillCreateItemByPathGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = 
        "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell%2"
        + "fsitecore%2fcontent%2fhome"
        + "?sc_database=db"
        + "&language=lg"
        + "&payload=full"
        + "&template=sample%2fsample%20item"
        + "&name=ItEmNaMe";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2&field3=VaLuE3";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestItemNameAndFieldNameIsCaseInsensitive()
    {
      ICreateItemByPathRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithParentPath("/sitecore/content/home")
        .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName("field1","VaLuE1")
        .AddFieldsRawValuesByName("field2","VaLuE2")
        .Build();

      ICreateItemByPathRequest autocompletedRequest = this.requestMerger.FillCreateItemByPathGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected =  "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell%2f"
        + "sitecore%2fcontent%2fhome"
        + "?sc_database=web"
        + "&language=en"
        + "&template=sample%2fsample%20item"
        + "&name=ItEmNaMe";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestFieldWithDuplicatedKeyWillCrash()
    {
      var requestBuilder = ItemWebApiRequestBuilder.CreateItemRequestWithParentId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
       .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName("field1", "VaLuE1")
        .AddFieldsRawValuesByName("field2", "VaLuE2");

      TestDelegate action = () => requestBuilder.AddFieldsRawValuesByName("field1","VaLuE3");
      Assert.Throws<InvalidOperationException>(action);
    }
  }
}

