namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Collections.Generic;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.UserRequest;

  [TestFixture()]
  public class UpdateItemByPathUrlBuilderTests
  {
    private UpdateItemByPathUrlBuilder builder;
    private UserRequestMerger requestMerger;

    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemSSCV2Grammar();
      ISSCUrlParameters webApiGrammar = SSCUrlParameters.ItemSSCV2UrlParameters();

      this.builder = new UpdateItemByPathUrlBuilder(restGrammar, webApiGrammar);

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemSSCVersion = "v234";
      mutableSessionConfig.InstanceUrl = "mobiledev1ua1.dk.sitecore.net:7119";
      mutableSessionConfig.Site = "/sitecore/shell";

      ItemSource source = LegacyConstants.DefaultSource();
      this.requestMerger = new UserRequestMerger(mutableSessionConfig, source);

    }

    [Test]
    public void TestFieldsAppending()
    {
      Dictionary<string, string> fields = new Dictionary<string, string>();
      fields.Add("field1", "VaLuE1");
      fields.Add("field2", "VaLuE2");

      IUpdateItemByPathRequest request = ItemSSCRequestBuilder.UpdateItemRequestWithPath("/sitecore/content/home")
        .Database("db")
        .Language("lg")
        .Version(2)
        .AddFieldsRawValuesByNameToSet(fields)
        .AddFieldsRawValuesByNameToSet("field3", "VaLuE3")
        .Build();

      IUpdateItemByPathRequest autocompletedRequest = this.requestMerger.FillUpdateItemByPathGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected =
        "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell%2"
        + "fsitecore%2fcontent%2fhome"
        + "?sc_database=db"
        + "&language=lg"
        + "&sc_itemversion=2"
        + "&payload=full";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2&field3=VaLuE3";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestItemNameAndFieldNameIsCaseInsensitive()
    {
      IUpdateItemByPathRequest request = ItemSSCRequestBuilder.UpdateItemRequestWithPath("/sitecore/content/home")
        .AddFieldsRawValuesByNameToSet("field1", "VaLuE1")
        .AddFieldsRawValuesByNameToSet("field2", "VaLuE2")
        .Build();

      IUpdateItemByPathRequest autocompletedRequest = this.requestMerger.FillUpdateItemByPathGaps(request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell%2f"
        + "sitecore%2fcontent%2fhome"
        + "?sc_database=web"
        + "&language=en";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestFieldWithDuplicatedKeyWillCrash()
    {
      var requestBuilder = ItemSSCRequestBuilder.UpdateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .AddFieldsRawValuesByNameToSet("field1", "VaLuE1")
        .AddFieldsRawValuesByNameToSet("field2", "VaLuE2");

      TestDelegate action = () => requestBuilder.AddFieldsRawValuesByNameToSet("field1", "VaLuE3");
      Assert.Throws<InvalidOperationException>(action);
    }
  }
}

