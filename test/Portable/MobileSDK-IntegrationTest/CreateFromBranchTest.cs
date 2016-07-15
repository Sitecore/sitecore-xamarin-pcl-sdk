﻿namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.MockObjects;


  [TestFixture]
  public class CreateFromBranchTest
  {
    private TestEnvironment testData;
    private ISitecoreSSCSession session;
    private ISitecoreSSCSession noThrowCleanupSession;

    const string NonExistingGuid = "{DEADBEEF-CDED-45AF-99BF-2DE9883B7AC3}";

    #region Setup
    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = this.CreateSession();

      // Same as this.session
      var cleanupSession = this.CreateSession();
      this.noThrowCleanupSession = new NoThrowSSCSession(cleanupSession);
    }

    private ISitecoreSSCSession CreateSession()
    {
      var result = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .BuildSession();

      return result;
    }

    private TestEnvironment.Item CreateTestItem(string name)
    {
      return new TestEnvironment.Item
      {
        DisplayName = name,
        Path = testData.Items.CreateItemsHere.Path + "/" + name,
        Template = this.testData.Items.Home.Template
      };
    }
    #endregion Setup

    #region TearDown
    [TearDown]
    public void TearDown()
    {
      this.testData = null;

      this.session.Dispose();
      this.session = null;

      this.noThrowCleanupSession.Dispose();
      this.noThrowCleanupSession = null;
    }

    public async Task<ScDeleteItemsResponse> RemoveTestItemsFromMasterAndWebAsync()
    {
      await this.DeleteAllItems("master");
      return await this.DeleteAllItems("web");
    }

    private async Task<ScDeleteItemsResponse> DeleteAllItems(string database)
    {
      var deleteFromMaster = ItemSSCRequestBuilder.DeleteItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
        .Database(database)
        .Build();
      return await this.noThrowCleanupSession.DeleteItemAsync(deleteFromMaster);
    }
    #endregion TearDown


    #region RequestsToServer
    [Test]
    public async void TestCreateItemByPathFromBranch()
    {
      await this.RemoveTestItemsFromMasterAndWebAsync();

      const string ItemFromBranchName = "Multiple item branch";
      TestEnvironment.Item expectedItem = this.CreateTestItem(ItemFromBranchName);

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .BranchId("{14416817-CDED-45AF-99BF-2DE9883B7AC3}")
        .ItemName(ItemFromBranchName)
        .Database("master")
        .Language("en")
        .Build();

      var createResponse = await session.CreateItemAsync(request);
      this.testData.AssertItemsCount(1, createResponse);

      ISitecoreItem resultItem = createResponse[0];
      this.testData.AssertItemsAreEqual(expectedItem, resultItem);


      var readJustCreatedItemRequest = ItemSSCRequestBuilder.ReadItemsRequestWithId(resultItem.Id)
        .Database("master")
        .Build();
      var readJustCreatedItemResponse = await this.session.ReadItemAsync(readJustCreatedItemRequest);

      this.testData.AssertItemsCount(1, readJustCreatedItemResponse);
      this.testData.AssertItemsAreEqual(expectedItem, readJustCreatedItemResponse[0]);
    }

    [Test]
    public async void TestCreateItemByIdFromBranch()
    {
      await this.RemoveTestItemsFromMasterAndWebAsync();

      const string ItemFromBranchName = "ITEM ID   A default name of the branch should be used";
      //      const string itemFromBranchName = "Multiple item brunch";
      TestEnvironment.Item expectedItem = this.CreateTestItem(ItemFromBranchName);

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentId(this.testData.Items.CreateItemsHere.Id)
        .BranchId("{14416817-CDED-45AF-99BF-2DE9883B7AC3}")
        .ItemName(ItemFromBranchName)
        .Database("master")
        .Language("en")
        .Build();

      var createResponse = await session.CreateItemAsync(request);
      this.testData.AssertItemsCount(1, createResponse);

      ISitecoreItem resultItem = createResponse[0];
      this.testData.AssertItemsAreEqual(expectedItem, resultItem);


      var readJustCreatedItemRequest = ItemSSCRequestBuilder.ReadItemsRequestWithId(resultItem.Id)
        .Database("master")
        .Build();
      var readJustCreatedItemResponse = await this.session.ReadItemAsync(readJustCreatedItemRequest);

      this.testData.AssertItemsCount(1, readJustCreatedItemResponse);
      this.testData.AssertItemsAreEqual(expectedItem, readJustCreatedItemResponse[0]);
    }
    #endregion RequestsToServer

    #region IncorrectInput
    [Test]
    public async void TestCreateItemByPathFromUnknownBranchCausesException()
    {
      await this.RemoveTestItemsFromMasterAndWebAsync();

      const string ItemFromBranchName = "ITEM PATH   A default name of the branch should be used";
      //      const string itemFromBranchName = "Multiple item brunch";

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .BranchId(NonExistingGuid)
        .ItemName(ItemFromBranchName)
        .Database("master")
        .Language("en")
        .Build();

      var ex = Assert.Throws<ParserException>(async () =>
      {
        await session.CreateItemAsync(request);
      });


      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", ex.InnerException.GetType().FullName);
      var castedException = ex.InnerException as SSCJsonErrorException;

      Assert.AreEqual(500, castedException.Response.StatusCode);
      Assert.AreEqual("Template item not found.", castedException.Response.Message);
    }

    [Test]
    public async void TestCreateItemByIdFromUnknownBranchCausesException()
    {
      await this.RemoveTestItemsFromMasterAndWebAsync();

      const string ItemFromBranchName = "ITEM ID   A default name of the branch should be used";
      //      const string itemFromBranchName = "Multiple item brunch";

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentId(this.testData.Items.CreateItemsHere.Id)
        .BranchId(NonExistingGuid)
        .ItemName(ItemFromBranchName)
        .Database("master")
        .Language("en")
        .Build();


      var ex = Assert.Throws<ParserException>(async () =>
      {
        await session.CreateItemAsync(request);
      });


      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", ex.InnerException.GetType().FullName);
      var castedException = ex.InnerException as SSCJsonErrorException;

      Assert.AreEqual(500, castedException.Response.StatusCode);
      Assert.AreEqual("Template item not found.", castedException.Response.Message);
    }
    #endregion IncorrectInput

    #region Validations
    [Test]
    public void TestNullBranchIdCausesNullPointerException()
    {
      Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("/some/valid/path")
        .BranchId(null));
    }

    [Test]
    public void TestEmptyBranchIdCausesArgumentException()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("/some/valid/path")
        .BranchId(""));
    }

    [Test]
    public void TestWhitespaceBranchIdCausesArgumentException()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("/some/valid/path")
        .BranchId("  \n   \r  \t \t\n\r"));
    }

    [Test]
    public void TestOpeningBraceOnlyBranchIdCausesArgumentException()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("/some/valid/path")
        .BranchId("{"));
    }

    [Test]
    public void TestClosingBraceOnlyBranchIdCausesArgumentException()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("/some/valid/path")
        .BranchId("}"));
    }

    [Test]
    public void TestBracesOnlyBranchIdCausesArgumentException()
    {
      Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("/some/valid/path")
        .BranchId("{}"));
    }
    #endregion Validations
  }
}

