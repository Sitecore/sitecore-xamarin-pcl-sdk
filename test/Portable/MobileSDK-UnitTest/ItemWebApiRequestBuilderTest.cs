using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;


  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;


  [TestFixture]
  public class ItemWebApiRequestBuilderTest
  {
    #region ItemId
    [Test]
    public void TestItemIdRequestBuilderWithAllFields()
    {
      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Database("web")
        .Language("en")
        .Version("1")
        .Payload(PayloadType.Full)
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-beef}", result.ItemId);
      Assert.AreEqual("en", result.ItemSource.Language);
      Assert.AreEqual("web", result.ItemSource.Database);
      Assert.AreEqual("1", result.ItemSource.Version);
      Assert.AreEqual( PayloadType.Full, result.QueryParameters.Payload );
    }

    [Test]
    public void TestLatestCallsOverrideFirstOnes()
    {
      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Database("web")
        .Language("en")
        .Version("1")
        .Payload(PayloadType.Full)

        .Database("core")
        .Language("fr")
        .Version("3872")
        .Payload(PayloadType.Content)

        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-beef}", result.ItemId);
      Assert.AreEqual("fr", result.ItemSource.Language);
      Assert.AreEqual("core", result.ItemSource.Database);
      Assert.AreEqual("3872", result.ItemSource.Version);
      Assert.AreEqual( PayloadType.Content, result.QueryParameters.Payload );
    }

    [Test]
    public void TestItemIdRequestBuilderWithIdOnly()
    {
      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{abra-kadabra}").Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);


      Assert.AreEqual("{abra-kadabra}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.Version);
      Assert.IsNull(result.QueryParameters.Payload);
    }

    [Test]
    public void TestItemIdRequestBuilderWithNullIdCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(null));
    }

    [Test]
    public void TestItemIdRequestBuilderWithEmptyIdCrashes()
    {

      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(""));
    }

    [Test]
    public void TestItemIdRequestBuilderWithWhitespaceIdCrashes()
    {

      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId("\t \r \n"));
    }

    [Test]
    public void TestItemIdWithoutBracesCrashesBuilder()
    {

      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId("ololololo"));
    }

    [Test]
    public void TestItemIdWithBracesOnlyCrashesBuilder()
    {

      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId("{}"));
    }
    #endregion ItemId


    #region ItemPath
    [Test]
    public void TestItemPathRequestBuilderWithAllFields()
    {
      IReadItemsByPathRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content")
        .Database("master")
        .Language("da")
        .Version("100500")
        .Payload( PayloadType.Content )
        .Build();


      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemPath);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);

      Assert.AreEqual("/sitecore/content", result.ItemPath);
      Assert.AreEqual("da", result.ItemSource.Language);
      Assert.AreEqual("master", result.ItemSource.Database);
      Assert.AreEqual("100500", result.ItemSource.Version);
      Assert.AreEqual( PayloadType.Content, result.QueryParameters.Payload );
    }

    [Test]
    public void TestItemPathRequestBuilderWithPathOnly()
    {
      IReadItemsByPathRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content").Build();


      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemPath);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);

      Assert.AreEqual("/sitecore/content", result.ItemPath);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.Version);
      Assert.IsNull(result.QueryParameters.Payload );
    }

    [Test]
    public void TestItemPathRequestBuilderWithNullPathCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(null));
    }

    [Test]
    public void TestItemPathRequestBuilderWithEmptyPathCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(""));
    }

    [Test]
    public void TestItemPathRequestBuilderWithWhitespacePathCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath("\t \r \n"));
    }

    [Test]
    public void TestItemPathWithoutStartingSlashCrashesBuilder()
    {
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath("blablabla"));
    }
    #endregion ItemPath


    #region SitecoreQuery
    [Test]
    public void TestSitecoreQueryRequestBuilderWithAllFields()
    {
      IReadItemsByQueryRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("fast:/sitecore/content/HOME/*")
        .Database("core")
        .Language("de")
        .Version("341")
        .Build();


      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.SitecoreQuery);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);

      Assert.AreEqual("fast:/sitecore/content/HOME/*", result.SitecoreQuery);
      Assert.AreEqual("de", result.ItemSource.Language);
      Assert.AreEqual("core", result.ItemSource.Database);
      Assert.AreEqual("341", result.ItemSource.Version);
      Assert.IsNull(result.QueryParameters.Payload);
    }

    [Test]
    public void TestQueryRequestBuilderWithQueryOnly()
    {
      IReadItemsByQueryRequest result = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("sitecore/content/HOME/*").Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.SitecoreQuery);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);

      Assert.AreEqual("sitecore/content/HOME/*", result.SitecoreQuery);
      Assert.IsNull(result.QueryParameters.Payload);

      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.Version);
    }

    [Test]
    public void TestQueryRequestBuilderWithNullQueryCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(null));
    }

    [Test]
    public void TestItemQueryRequestBuilderWithEmptyQueryCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(""));
    }

    [Test]
    public void TestQueryRequestBuilderWithWhitespaceQueryCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("\t \r \n"));
    }            
    #endregion SitecoreQuery


    #region Fields
    [Test]
    public void TestMultipleItemFieldsCanBeAddedIncrementally()
    {
      string[] fields = { "Мама", "Мыла", "Раму" };
      string[] moarFields = { "1", "2", "4" };
      string[] expectedFields = { "Мама", "Мыла", "Раму", "1", "2", "4" };

      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFields(fields)
        .AddFields(moarFields)
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-c0de}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.Version);
      Assert.IsNull(result.QueryParameters.Payload);
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }


    [Test]
    public void TestSingleItemFieldsCanBeAddedIncrementally()
    {
      string[] expectedFields = { "Мыла", "Раму", "Мама" };

      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFields("Мыла")
        .AddFields("Раму")
        .AddFields("Мама")
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-c0de}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.Version);
      Assert.IsNull(result.QueryParameters.Payload);
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }
    #endregion Fields
  }
}

