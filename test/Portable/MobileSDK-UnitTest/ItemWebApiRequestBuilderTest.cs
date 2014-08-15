namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

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
        .Version(1)
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
      Assert.AreEqual(1, result.ItemSource.VersionNumber);
      Assert.AreEqual( PayloadType.Full, result.QueryParameters.Payload );
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
      Assert.IsNull(result.ItemSource.VersionNumber);
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
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(""));
    }

    [Test]
    public void TestItemIdRequestBuilderWithWhitespaceIdCrashes()
    {

      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId("\t \r \n"));
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
        .Version(100500)
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
      Assert.AreEqual(100500, result.ItemSource.VersionNumber);
      Assert.AreEqual(PayloadType.Content, result.QueryParameters.Payload );
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
      Assert.IsNull(result.ItemSource.VersionNumber);
      Assert.IsNull(result.QueryParameters.Payload);
    }

    [Test]
    public void TestItemPathRequestBuilderWithNullPathCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(null));
    }

    [Test]
    public void TestItemPathRequestBuilderWithEmptyPathCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(""));
    }

    [Test]
    public void TestItemPathRequestBuilderWithWhitespacePathCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath("\t \r \n"));
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

        // @adk : does not compile by design
        //        .Version("341")
        .Build();


      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.SitecoreQuery);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);

      Assert.AreEqual("fast:/sitecore/content/HOME/*", result.SitecoreQuery);
      Assert.AreEqual("de", result.ItemSource.Language);
      Assert.AreEqual("core", result.ItemSource.Database);

//      Assert.AreEqual("341", result.ItemSource.VersionNumber);
      Assert.IsNull(result.ItemSource.VersionNumber);

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
      Assert.IsNull(result.ItemSource.VersionNumber);
    }

    [Test]
    public void TestQueryRequestBuilderWithNullQueryCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(null));
    }

    [Test]
    public void TestItemQueryRequestBuilderWithEmptyQueryCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(""));
    }

    [Test]
    public void TestQueryRequestBuilderWithWhitespaceQueryCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("\t \r \n"));
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
        .AddFieldsToRead(fields)
        .AddFieldsToRead(moarFields)
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-c0de}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.VersionNumber);
      Assert.IsNull(result.QueryParameters.Payload);
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }

    [Test]
    public void TestMultipleItemFieldsCanBeAddedByAnyCollection()
    {
      string[] fields = { "Мама", "Мыла", "Раму" };
      string[] moarFields = { "1", "2", "4" };
      List<string> moarFieldsList = new List<string>(moarFields);

      string[] expectedFields = { "Мама", "Мыла", "Раму", "1", "2", "4" };

      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead(fields)
        .AddFieldsToRead(moarFieldsList)
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-c0de}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.VersionNumber);
      Assert.IsNull(result.QueryParameters.Payload);
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }



    [Test]
    public void TestSingleItemFieldsCanBeAddedIncrementally()
    {
      string[] expectedFields = { "Мыла", "Раму", "Мама" };

      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("Мыла")
        .AddFieldsToRead("Раму")
        .AddFieldsToRead("Мама")
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-c0de}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.VersionNumber);
      Assert.IsNull(result.QueryParameters.Payload);
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }


    [Test]
    public void TestDuplicatedFieldsCauseException()
    {
      Assert.Throws<InvalidOperationException>(() => 
      ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("XXXXX")
        .AddFieldsToRead("YYY")
        .AddFieldsToRead("XXXXX")
        .Build());
    }

    [Test]
    public void TestCaseInsensitiveDuplicatedFieldsCauseException()
    {
      Assert.Throws<InvalidOperationException>(() => 
        ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("XXXXX")
        .AddFieldsToRead("YYY")
        .AddFieldsToRead("xxXXx")
        .Build());
    }


    [Test]
    public void TestEmptyFieldsAreIgnored()
    {
      var request = 
        ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("")
        .Build();

      Assert.IsNotNull(request);
      Assert.IsNotNull(request.QueryParameters);
      if (null != request.QueryParameters.Fields)
      {
        Assert.AreEqual(0, request.QueryParameters.Fields.Count());
      }
    }

    [Test]
    public void TestNullFieldsAreNotIgnored()
    {
      {
        TestDelegate action = () => ItemWebApiRequestBuilder.ReadItemsRequestWithId ("{dead-c0de}").AddFieldsToRead ((string)null);
        Assert.Throws<ArgumentNullException>(action);
      }

      {
        TestDelegate action = () => ItemWebApiRequestBuilder.ReadItemsRequestWithId ("{dead-c0de}").AddFieldsToRead ((IEnumerable<string>)null);
        Assert.Throws<ArgumentNullException>(action);
      }
    }

    [Test]
    public void TestWhitespaceFieldsAreIgnored()
    {
      var request = 
        ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("\n   \t   \r")
        .Build();

      Assert.IsNotNull(request);
      Assert.IsNotNull(request.QueryParameters);
      if (null != request.QueryParameters.Fields)
      {
        Assert.AreEqual(0, request.QueryParameters.Fields.Count());
      }
    }

    [Test]
    public void TestAddFieldMethodSupportsParamsKeyword()
    {
      string[] expectedFields = { "alpha", "beta", "gamma" };

      IReadItemsByIdRequest result =  ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("alpha", "beta", "gamma")
        .Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);



      Assert.AreEqual("{dead-c0de}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.VersionNumber);
      Assert.IsNull(result.QueryParameters.Payload);
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }
    #endregion Fields

    #region Database Validation
    [Test]
    public void TestNullDatabaseCanBeAssignedExplicitly()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Database(null)
        .Build();

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestEmptyDatabaseCanBeAssignedExplicitly()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Database(string.Empty)
        .Build();

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestDatabaseCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/pppp/sss/*")
        .Database("master")
        .Database("web")
      );
    }

    [Test]
    public void TestWhitespaceDatabaseCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Database("\t   \r  \n")
      );
    }
    #endregion Database Validation


    #region Language Validationvar request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
     
    [Test]
    public void TestNullLanguageCanBeAssignedExplicitly()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/pppp/sss/*")
        .Language(null)
        .Build();

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestEmptyLanguageCanBeAssignedExplicitly()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Language(string.Empty)
        .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestLanguageCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Language("en")
        .Language("fr")
      );
    }

    [Test]
    public void TestWhitespaceLanguageCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Language("\t   \r  \n")
      );
    }
    #endregion Language Validation

    #region Version Validation
    [Test]
    public void TestNullVersionCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentNullException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Version(null)
      );
    }

    [Test]
    public void TestZeroVersionCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/pppp/sss")
        .Version(0)
      );
    }

    [Test]
    public void TestNegativeVersionCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/pppp/sss")
        .Version(-34)
      );
    }

    [Test]
    public void TestVersionCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Version(2)
        .Version(99)
      );
    }
    #endregion Version Validation

    #region Payload Validation
    [Test]
    public void TestPayloadCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Payload(PayloadType.Content)
        .Payload(PayloadType.Min)
      );
    }
    #endregion Payload Validation
  
    #region Scope
    [Test]
    public void TestAddScopeThrowsExceptionOnDuplicates()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/SitecoreDotShell")
        .AddScope(ScopeType.Self)
        .AddScope(ScopeType.Self));
    }

    [Test]
    public void TestAddScopeSupportsParamsSyntax()
    {
      var request = 
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/sHell")
          .AddScope(ScopeType.Self, ScopeType.Parent, ScopeType.Children);

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestAddScopeSupportsCollection()
    {
      ScopeType[] scopeArgs = { ScopeType.Self, ScopeType.Parent, ScopeType.Children };
      var scopeArgsList = new List<ScopeType>(scopeArgs);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/aaaa")
        .AddScope(scopeArgsList);
      Assert.IsNotNull(request);
    }


    [Test]
    public void TestAddScopeThrowsExceptionOnDuplicatesInParams()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecoreDOTshell")
        .AddScope(ScopeType.Self, ScopeType.Parent, ScopeType.Self ) );
    }

    [Test]
    public void TestAddScopeThrowsExceptionOnDuplicatesInIncrementCalls()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecoreDOTshell")
        .AddScope(ScopeType.Self, ScopeType.Parent )
        .AddScope(ScopeType.Self) );
    }
    #endregion Scope
  }
}

