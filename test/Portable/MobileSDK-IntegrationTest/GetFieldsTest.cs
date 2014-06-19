namespace MobileSDKIntegrationTest
{
  using System.Collections.ObjectModel;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [TestFixture]
  public class GetFieldsTest
  {
    private TestEnvironment testData;
    private ScApiSession sessionAuthenticatedUser;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      this.sessionAuthenticatedUser = testData.GetSession(
        testData.InstanceUrl,
        testData.Users.Admin.Username,
        testData.Users.Admin.Password);
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    [Test]
    public async void TestGetItemByIdWithContentFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(2, item.Fields.Count);
      Assert.AreEqual("Sitecore", item.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithAllFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);

      Assert.IsTrue(response.Items[0].Fields.Count > 70);
      Assert.AreEqual("Home", response.Items[0].FieldWithName("__Display name").RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithoutFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Min).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      Assert.AreEqual(0, response.Items[0].Fields.Count);
    }

    [Test]
    public async void TestGetItemByQueryWithContentFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/ancestor-or-self::*").Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(3, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(2, item.Fields.Count);
      Assert.AreEqual("Sitecore", item.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithInternationalFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Android/Static/Japanese/宇都宮").Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
      Assert.AreEqual("/sitecore/content/Home/Android/Static/Japanese/宇都宮", response.Items[0].Path);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(2, item.Fields.Count);
      Assert.AreEqual("宇都宮", item.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetHtmlField()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(2, item.Fields.Count);
      Assert.AreEqual("Text", item.Fields[1].Name);
      Assert.True(item.Fields[1].RawValue.Contains("<div>Welcome to Sitecore!</div>"));
    }

    [Test]
    public async void TestGet2FieldsSpecifiedExplicitly()
    {
      var fields = new Collection<string>
      {
        "CheckBoxField",
        "MultiListField"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.TestFieldsItem.Id).AddFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(2, item.Fields.Count);
      Assert.AreEqual(item.FieldWithName("CheckBoxField").RawValue, "1");
      Assert.AreEqual(item.FieldWithName("MultiListField").RawValue, "{2075CBFF-C330-434D-9E1B-937782E0DE49}");
    }

    [Test]
    public async void TestGetNotExistedFields()
    {
      var fields = new Collection<string>
      {
        "",
      };

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.TestFieldsItem.Path).AddFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response.Items[0]);
      Assert.AreEqual(0, response.Items[0].Fields.Count);
    }

    [Test]
    public async void TestGetFieldsWithEnglishLanguage()
    {
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(1, item.Fields.Count);
      Assert.AreEqual("English version 2 web", item.FieldWithName("title").RawValue);
    }

    [Test]
    public async void TestGetFieldsWithDanishLanguage()
    {
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFields(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(1, item.Fields.Count);
      Assert.AreEqual("Danish version 2 web", item.FieldWithName("title").RawValue);
    }

    [Test]
    public async void TestGetItemByIdWithInvalidFieldName()
    {
      var fields = new Collection<string>
      {
        "!@#$%^&*()_+*/"
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).AddFields(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ISitecoreItem item = response.Items[0];

      Assert.AreEqual(0, item.Fields.Count);
    }

    [Test]
    public async void TestGetSeveralItemsByQueryWithContentFields()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path + "/*").Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);

      var expectedAllowedParentItem = new TestEnvironment.Item
      {
        DisplayName = "Allowed_Parent",
        Template = "Sample/Sample Item"
      };
      ISitecoreItem actualAllowedParentItem = response.Items[0];
      testData.AssertItemsAreEqual(expectedAllowedParentItem, actualAllowedParentItem);

      Assert.AreEqual(2, actualAllowedParentItem.Fields.Count);
      Assert.AreEqual("Allowed_Parent", actualAllowedParentItem.FieldWithName("Title").RawValue);


      var expectedTestFieldsItem = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };

      ISitecoreItem actualTestFieldsItem = response.Items[3];
      testData.AssertItemsAreEqual(expectedTestFieldsItem, actualTestFieldsItem);

      Assert.AreEqual(19, actualTestFieldsItem.Fields.Count);
      Assert.AreEqual("Text", actualTestFieldsItem.FieldWithName("Text").RawValue);
      Assert.AreEqual("1", actualTestFieldsItem.FieldWithName("CheckBoxField").RawValue);
      Assert.AreEqual("Normal Text", actualTestFieldsItem.FieldWithName("Normal Text").RawValue);
    }

    [Test]
    public async void TestGetItemByIdWithAllFieldsWithoutReadAcessToSomeFields()
    {
      var sessionCreatorexUser = testData.GetSession(testData.InstanceUrl, testData.Users.Creatorex.Username, testData.Users.Creatorex.Password);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}").Payload(PayloadType.Full).Build();

      var responseCreatorex = await sessionCreatorexUser.ReadItemAsync(request);
      var responseAdmin = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      Assert.IsTrue(responseCreatorex.Items[0].Fields.Count < responseAdmin.Items[0].Fields.Count);
    }

    [Test]
    public async void TestGetFieldsWithSymbolsAndSpacesInNameFields()
    {
      var fields = new Collection<string>
      {
        "Normal Text",
       "__Owner"  
      };
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}").AddFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var expectedItemTestTemplate = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };
      testData.AssertItemsAreEqual(expectedItemTestTemplate, response.Items[0]);
      ISitecoreItem item = response.Items[0];


      Assert.AreEqual(2, item.Fields.Count);
      Assert.AreEqual("Normal Text", item.FieldWithName("Normal Text").Name);
      Assert.AreEqual("sitecore\\admin", item.FieldWithName("__Owner").RawValue);
    }
  }
}