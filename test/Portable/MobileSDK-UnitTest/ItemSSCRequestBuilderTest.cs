namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  [TestFixture]
  public class ItemSSCRequestBuilderTest
  {
    #region ItemId
    [Test]
    public void TestItemIdRequestBuilderWithAllFields()
    {
      IReadItemsByIdRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Database("web")
        .Language("en")
        .Version(1)
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
    }


    [Test]
    public void TestItemIdRequestBuilderWithIdOnly()
    {
      IReadItemsByIdRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithId("{abra-kadabra}").Build();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemId);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);


      Assert.AreEqual("{abra-kadabra}", result.ItemId);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.VersionNumber);
    }

    [Test]
    public void TestItemIdRequestBuilderWithNullIdCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId(null));
    }

    [Test]
    public void TestItemIdRequestBuilderWithEmptyIdCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId(""));
    }

    [Test]
    public void TestItemIdRequestBuilderWithWhitespaceIdCrashes()
    {

      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId("\t \r \n"));
    }

    [Test]
    public void TestItemIdWithoutBracesCrashesBuilder()
    {

      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId("ololololo"));
    }

    [Test]
    public void TestItemIdWithBracesOnlyCrashesBuilder()
    {

      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId("{}"));
    }
    #endregion ItemId


    #region ItemPath
    [Test]
    public void TestItemPathRequestBuilderWithAllFields()
    {
      IReadItemsByPathRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithPath("/sitecore/content")
        .Database("master")
        .Language("da")
        .Version(100500)
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
    }

    [Test]
    public void TestItemPathRequestBuilderWithPathOnly()
    {
      IReadItemsByPathRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithPath("/sitecore/content").Build();


      Assert.IsNotNull(result);
      Assert.IsNotNull(result.ItemSource);
      Assert.IsNotNull(result.ItemPath);
      Assert.IsNotNull( result.QueryParameters );
      Assert.IsNull(result.SessionSettings);

      Assert.AreEqual("/sitecore/content", result.ItemPath);
      Assert.IsNull(result.ItemSource.Language);
      Assert.IsNull(result.ItemSource.Database);
      Assert.IsNull(result.ItemSource.VersionNumber);
    }

    [Test]
    public void TestItemPathRequestBuilderWithNullPathCrashes()
    {
      Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithPath(null));
    }

    [Test]
    public void TestItemPathRequestBuilderWithEmptyPathCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithPath(""));
    }

    [Test]
    public void TestItemPathRequestBuilderWithWhitespacePathCrashes()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithPath("\t \r \n"));
    }

    [Test]
    public void TestItemPathWithoutStartingSlashCrashesBuilder()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithPath("blablabla"));
    }
    #endregion ItemPath

    #region Fields
    [Test]
    public void TestMultipleItemFieldsCanBeAddedIncrementally()
    {
      string[] fields = { "Мама", "Мыла", "Раму" };
      string[] moarFields = { "1", "2", "4" };
      string[] expectedFields = { "Мама", "Мыла", "Раму", "1", "2", "4" };

      IReadItemsByIdRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
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
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }

    [Test]
    public void TestMultipleItemFieldsCanBeAddedByAnyCollection()
    {
      string[] fields = { "Мама", "Мыла", "Раму" };
      string[] moarFields = { "1", "2", "4" };
      List<string> moarFieldsList = new List<string>(moarFields);

      string[] expectedFields = { "Мама", "Мыла", "Раму", "1", "2", "4" };

      IReadItemsByIdRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
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
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }



    [Test]
    public void TestSingleItemFieldsCanBeAddedIncrementally()
    {
      string[] expectedFields = { "Мыла", "Раму", "Мама" };

      IReadItemsByIdRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
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
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }


    [Test]
    public void TestDuplicatedFieldsCauseException()
    {
      Assert.Throws<InvalidOperationException>(() => 
      ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("XXXXX")
        .AddFieldsToRead("YYY")
        .AddFieldsToRead("XXXXX")
        .Build());
    }

    [Test]
    public void TestCaseInsensitiveDuplicatedFieldsCauseException()
    {
      Assert.Throws<InvalidOperationException>(() => 
        ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
        .AddFieldsToRead("XXXXX")
        .AddFieldsToRead("YYY")
        .AddFieldsToRead("xxXXx")
        .Build());
    }


    [Test]
    public void TestEmptyFieldsAreIgnored()
    {
      var request = 
        ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
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
        TestDelegate action = () => ItemSSCRequestBuilder.ReadItemsRequestWithId ("{dead-c0de}").AddFieldsToRead ((string)null);
        Assert.Throws<ArgumentNullException>(action);
      }

      {
        TestDelegate action = () => ItemSSCRequestBuilder.ReadItemsRequestWithId ("{dead-c0de}").AddFieldsToRead ((IEnumerable<string>)null);
        Assert.Throws<ArgumentNullException>(action);
      }
    }

    [Test]
    public void TestWhitespaceFieldsAreIgnored()
    {
      var request = 
        ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
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

      IReadItemsByIdRequest result =  ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-c0de}")
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
      Assert.AreEqual(expectedFields, result.QueryParameters.Fields);
    }
    #endregion Fields

    #region Database Validation
    [Test]
    public void TestNullDatabaseCanBeAssignedExplicitly()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Database(null)
        .Build();

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestEmptyDatabaseCanBeAssignedExplicitly()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Database(string.Empty)
        .Build();

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestDatabaseCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
                                               ItemSSCRequestBuilder.ReadItemsRequestWithPath("/pppp/sss/*")
                                               .Database("master")
                                               .Database("web")
      );
    }

    [Test]
    public void TestWhitespaceDatabaseCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Database("\t   \r  \n")
      );
    }
    #endregion Database Validation


    #region Language Validationvar request = ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
     
    [Test]
    public void TestNullLanguageCanBeAssignedExplicitly()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath("/pppp/sss/*")
        .Language(null)
        .Build();

      Assert.IsNotNull(request);
    }

    [Test]
    public void TestEmptyLanguageCanBeAssignedExplicitly()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Language(string.Empty)
        .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestLanguageCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Language("en")
        .Language("fr")
      );
    }

    [Test]
    public void TestWhitespaceLanguageCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Language("\t   \r  \n")
      );
    }
    #endregion Language Validation

    #region Version Validation
    [Test]
    public void TestNullVersionCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentNullException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/aaa/bb/fff")
        .Version(null)
      );
    }

    [Test]
    public void TestZeroVersionCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/pppp/sss")
        .Version(0)
      );
    }

    [Test]
    public void TestNegativeVersionCannotBeAssignedExplicitly()
    {
      Assert.Throws<ArgumentException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithPath("/pppp/sss")
        .Version(-34)
      );
    }

    [Test]
    public void TestVersionCannotBeAssignedTwice()
    {
      Assert.Throws<InvalidOperationException>( () =>
        ItemSSCRequestBuilder.ReadItemsRequestWithId("{dead-beef}")
        .Version(2)
        .Version(99)
      );
    }
    #endregion Version Validation

  }
}

