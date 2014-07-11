using System.Collections.Generic;

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK;

  [TestFixture]
  public class CreateItemByIdUrlBuilderTests
  {
    private CreateItemByIdUrlBuilder builder;
    private ISessionConfig sitecoreShellConfig;
    private QueryParameters payload;
    private UserRequestMerger requestMerger;

    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
      IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

      this.builder = new CreateItemByIdUrlBuilder(restGrammar, webApiGrammar);

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemWebApiVersion = "v234";
      mutableSessionConfig.InstanceUrl = "mobiledev1ua1.dk.sitecore.net:7119";
      mutableSessionConfig.Site = "/sitecore/shell";
      this.sitecoreShellConfig = mutableSessionConfig;

      ItemSource source = ItemSource.DefaultSource();
      this.requestMerger = new UserRequestMerger(mutableSessionConfig, source);

      this.payload = new QueryParameters( PayloadType.Min, null, null );
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
    }
      
    [Test]
    public void TestCorrectParamsWithoutFields()
    {
      ICreateItemByIdRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("item name")
        .Build();

      ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps (request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&language=en&template=sample%2fsample%20item&name=item%20name&sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestCorrectParamsWithFields()
    {
      ICreateItemByIdRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("item name")
        .AddFieldsRawValuesByName("field1","value1")
        .AddFieldsRawValuesByName("field2","value2")
        .Build();

      ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps (request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&language=en&template=sample%2fsample%20item&name=item%20name&sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=value1&field2=value2";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestFieldsValuesIsCaseInsensitive()
    {
      ICreateItemByIdRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("item name")
        .AddFieldsRawValuesByName("field1","VaLuE1")
        .AddFieldsRawValuesByName("field2","VaLuE2")
        .Build();

      ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps (request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&language=en&template=sample%2fsample%20item&name=item%20name&sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestItemNameIsCaseInsensitive()
    {
      ICreateItemByIdRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName("field1","VaLuE1")
        .AddFieldsRawValuesByName("field2","VaLuE2")
        .Build();

      ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps (request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&language=en&template=sample%2fsample%20item&name=ItEmNaMe&sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }

    [Test]
    public void TestFieldWithDoublicatedKeyWillCrash()
    {
      var requestBuilder = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName("field1", "VaLuE1")
        .AddFieldsRawValuesByName("field2", "VaLuE2");
        
      TestDelegate action = () => requestBuilder.AddFieldsRawValuesByName("field1","VaLuE3");
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestAppendingFields()
    {
      var requestBuilder = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName("field1", "VaLuE1")
        .AddFieldsRawValuesByName("field2", "VaLuE2");

      TestDelegate action = () => requestBuilder.AddFieldsRawValuesByName("field1","VaLuE3");
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestNameIsMandatoryField()
    {
      var requestBuilder = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .AddFieldsRawValuesByName("field1", "VaLuE1")
        .AddFieldsRawValuesByName("field2", "VaLuE2");

      TestDelegate action = () => requestBuilder.Build();
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestTemplateIsMandatoryField()
    {
      var requestBuilder = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName("field1", "VaLuE1")
        .AddFieldsRawValuesByName("field2", "VaLuE2");

      TestDelegate action = () => requestBuilder.Build();
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestFieldsAppending()
    {
      Dictionary<string,string> fields = new Dictionary<string,string>();
      fields.Add("field1","VaLuE1");
      fields.Add("field2","VaLuE2");

      ICreateItemByIdRequest request = ItemWebApiRequestBuilder.CreateItemRequestWithId("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}")
        .ItemTemplate("Sample/Sample Item")
        .ItemName("ItEmNaMe")
        .AddFieldsRawValuesByName(fields)
        .AddFieldsRawValuesByName("field3","VaLuE3")
        .Build();

      ICreateItemByIdRequest autocompletedRequest = this.requestMerger.FillCreateItemByIdGaps (request);

      string result = this.builder.GetUrlForRequest(autocompletedRequest);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&language=en&template=sample%2fsample%20item&name=ItEmNaMe&sc_itemid=%7b110d559f-dea5-42ea-9c1c-8a5df7e70ef9%7d";

      string fieldsResult = this.builder.GetFieldValuesList(autocompletedRequest);
      string expectedFieldsResult = "field1=VaLuE1&field2=VaLuE2&field3=VaLuE3";

      Assert.AreEqual(expected, result);
      Assert.AreEqual(expectedFieldsResult, fieldsResult);
    }
  }
}
