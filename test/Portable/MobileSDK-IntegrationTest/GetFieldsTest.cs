namespace MobileSDKIntegrationTest
{
  using System;
  using System.Linq;
  using System.Collections.ObjectModel;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class GetFieldsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession sessionAuthenticatedUser;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      this.sessionAuthenticatedUser =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser.Dispose();
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    [Test]
    public async void TestGetItemByIdWithContentFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(2, item.FieldsCount);
      Assert.AreEqual("Sitecore", item["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithAllFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);

      Assert.IsTrue(response[0].FieldsCount > 70);
      Assert.AreEqual("Home", response[0]["__Display name"].RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithoutFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Min).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      Assert.AreEqual(0, response[0].FieldsCount);
    }

    [Test]
    public async void TestGetItemByQueryWithContentFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/ancestor-or-self::*").Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(3, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(2, item.FieldsCount);
      Assert.AreEqual("Sitecore", item["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithInternationalFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Android/Static/Japanese/宇都宮").Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response[0].DisplayName);
      Assert.AreEqual("/sitecore/content/Home/Android/Static/Japanese/宇都宮", response[0].Path);
      ISitecoreItem item = response[0];

      Assert.AreEqual(2, item.FieldsCount);
      Assert.AreEqual("宇都宮", item["Title"].RawValue);
    }

    [Test]
    public async void TestGetHtmlField()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).AddFieldsToRead(new Collection<string> { "Text" }).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      ISitecoreItem item = response[0];


      Assert.AreEqual(1, item.FieldsCount);
      Assert.AreEqual("Text", item.Fields.ElementAt(0).Name);
      Assert.True(item["Text"].RawValue.Contains("<div>Welcome to Sitecore!</div>"));
    }

    [Test]
    public async void TestGet2FieldsSpecifiedExplicitly()
    {
      var fields = new Collection<string>
      {
        "CheckBoxField",
        "MultiListField"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.TestFieldsItem.Id).AddFieldsToRead(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(2, item.FieldsCount);
      Assert.AreEqual(item["CheckBoxField"].RawValue, "1");
      Assert.AreEqual(item["MultiListField"].RawValue, "{2075CBFF-C330-434D-9E1B-937782E0DE49}");
    }

    [Test]
    public async void TestGetEmptyField()
    {
      var fields = new Collection<string>
      {
        "",
      };

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.TestFieldsItem.Path)
        .AddFieldsToRead(fields)
        .Payload(PayloadType.Default)
        .Build();

      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response[0]);
      Assert.AreEqual(0, response[0].FieldsCount);
    }

    [Test]
    public async void TestGetFieldsWithEnglishLanguage()
    {
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFieldsToRead(fields).Language("en").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(1, item.FieldsCount);
      Assert.AreEqual("English version 2 web", item["title"].RawValue);
    }

    [Test]
    public async void TestGetFieldsById()
    {
      var fields = new Collection<string>
      {
        "{75577384-3C97-45DA-A847-81B00500E250}"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFieldsToRead(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(1, item.FieldsCount);
      Assert.AreEqual("English version 2 web", item["title"].RawValue);
    }

    [Test]
    public async void TestGetFieldsByIdAndPathSimultaneously()
    {
      var fields = new Collection<string>
      {
        "{75577384-3C97-45DA-A847-81B00500E250}",
        "Title"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFieldsToRead(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(1, item.FieldsCount);
      Assert.AreEqual("English version 2 web", item["title"].RawValue);
    }

    [Test]
    public async void TestGetFieldsWithDanishLanguage()
    {
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFieldsToRead(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(1, item.FieldsCount);
      Assert.AreEqual("Danish version 2 web", item["title"].RawValue);
    }

    [Test]
    public async void TestGetItemByIdWithInvalidFieldName()
    {
      var fields = new Collection<string>
      {
        ".!@#$%^&*()_+*/"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFieldsToRead(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(0, item.FieldsCount);
    }

    [Test]
    public async void TestGetSeveralItemsByQueryWithContentFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path + "/*")
        .Payload(PayloadType.Content)
        .Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);

      var expectedAllowedParentItem = new TestEnvironment.Item
      {
        DisplayName = "Allowed_Parent",
        Template = "Sample/Sample Item"
      };
      ISitecoreItem actualAllowedParentItem = response[0];
      testData.AssertItemsAreEqual(expectedAllowedParentItem, actualAllowedParentItem);

      Assert.AreEqual(2, actualAllowedParentItem.FieldsCount);
      Assert.AreEqual("Allowed_Parent", actualAllowedParentItem["Title"].RawValue);


      var expectedTestFieldsItem = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };

      ISitecoreItem actualTestFieldsItem = response[3];
      testData.AssertItemsAreEqual(expectedTestFieldsItem, actualTestFieldsItem);

      Assert.AreEqual(19, actualTestFieldsItem.FieldsCount);
      Assert.AreEqual("Text", actualTestFieldsItem["Text"].RawValue);
      Assert.AreEqual("1", actualTestFieldsItem["CheckBoxField"].RawValue);
      Assert.AreEqual("Normal Text", actualTestFieldsItem["Normal Text"].RawValue);
    }

    [Test]
    public async void TestGetItemByIdWithAllFieldsWithoutReadAccessToSomeFields()
    {
      var sessionCreatorexUser =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Creatorex)
          .BuildReadonlySession();

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}").Payload(PayloadType.Full).Build();

      var responseCreatorex = await sessionCreatorexUser.ReadItemAsync(request);
      var responseAdmin = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      Assert.IsTrue(responseCreatorex[0].FieldsCount < responseAdmin[0].FieldsCount);
    }

    [Test]
    public async void TestGetFieldsWithSymbolsAndSpacesInNameFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}").AddFieldsToRead("Normal Text", "__Owner").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var expectedItemTestTemplate = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };
      testData.AssertItemsAreEqual(expectedItemTestTemplate, response[0]);
      ISitecoreItem item = response[0];

      Assert.AreEqual(2, item.FieldsCount);
      Assert.AreEqual("Normal Text", item["Normal Text"].Name);
      Assert.AreEqual("sitecore\\admin", item["__Owner"].RawValue);
    }

    [Test]
    public void TestGetItemByIdWithDuplicateFieldsReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).AddFieldsToRead("Title", "Text").AddFieldsToRead("title").Build());

      Assert.AreEqual("ReadItemByIdRequestBuilder.Fields : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public void TestGetItemByPathWithDuplicateFieldsReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).AddFieldsToRead("Text", "Text").Build());

      Assert.AreEqual("ReadItemByPathRequestBuilder.Fields : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public void TestGetItemByQueryWithDuplicateAndEmptyFieldReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path).AddFieldsToRead("Title", "", "Title").Build());

      Assert.AreEqual("ReadItemByQueryRequestBuilder.Fields : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public async void TestGetItemByIdWithDefaultPayload()     // ALR: PayloadType.Default constant should be removed
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Payload(PayloadType.Default).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      var item = response[0];

      Assert.AreEqual(0, item.FieldsCount);
    }

    [Test]
    public async void TestGetItemByIdWithoutPayload()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      var item = response[0];

      Assert.AreEqual(2, item.FieldsCount);
      Assert.AreEqual("Sitecore", item["Title"].RawValue);
    }

    [Test]
    public void TestGetItemByIdWithOverridenPayloadReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id)
        .Payload(PayloadType.Content)
        .Payload(PayloadType.Full)
        .Build());
      Assert.AreEqual("ReadItemByIdRequestBuilder.Payload : Property cannot be assigned twice.", exception.Message);

    }

    [Test]
    public void TestGetItemByPathWithOverridenPayloadReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path)
        .Payload(PayloadType.Full)
        .Payload(PayloadType.Min)
        .Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Payload : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestGetItemByQueryWithOverridenPayloadReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path)
        .Payload(PayloadType.Content)
        .Payload(PayloadType.Content)
        .Build());
      Assert.AreEqual("ReadItemByQueryRequestBuilder.Payload : Property cannot be assigned twice.", exception.Message);
    }
  }
}