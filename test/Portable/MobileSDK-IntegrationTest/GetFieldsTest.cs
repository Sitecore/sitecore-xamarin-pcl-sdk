namespace MobileSDKIntegrationTest
{
  using System;
  using System.Collections.ObjectModel;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
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
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      this.sessionAuthenticatedUser = new ScApiSession(config, ItemSource.DefaultSource());
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
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.Home.Id).Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(2, item.Fields.Count());
      //Assert.AreEqual("Sitecore", item.Fields("Title").RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithAllFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.Greater(50, item.Fields.Count());
      //Assert.AreEqual("Home", item.Fields("__Display name").RawValue);
      //Assert.AreEqual("The Home item is the default starting point for a website.", item.Fields("__Long description").RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithoutFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Min).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(0, item.Fields.Count());
    }

    [Test]
    public async void TestGetItemByQueryWithContentFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithSitecoreQuery("/sitecore/content/Home/ancestor-or-self::*").Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      testData.AssertItemsCount(3, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(3, item.Fields.Count());
      //Assert.AreEqual("Sitecore", item.Fields("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithInternationalFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath("/sitecore/content/Home/Android/Static/Japanese/宇都宮").Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
      Assert.AreEqual("/sitecore/content/Home/Android/Static/Japanese/宇都宮", response.Items[0].Path);
      ScItem item = response.Items[0];
      //Assert.AreEqual(2, item.Fields.Count());
      //Assert.AreEqual("宇都宮", item.Fields("Title").RawValue);
    }

    [Test]
    public async void TestGetHtmlField()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(testData.Items.Home.Path).Payload(PayloadType.Content).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(2, item.Fields.Count());
      //Assert.True(item.Fields("Title").RawValue.Contains("<div>Welcome to Sitecore!</div>"));
    }

    [Test]
    public async void TestGet2FieldsSpecifiedExplicitly()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var fields = new Collection<string>
      {
        "CheckBoxField",
        "MultiListField"
      };
      var request = requestBuilder.RequestWithId(testData.Items.TestFieldsItem.Id).LoadFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(2, item.Fields.Count());
      //Assert.AreEqual(item.Fields("CheckBoxField").RawValue, "1");
      //Assert.AreEqual(item.Fields("MultiListField").RawValue, "{2075CBFF-C330-434D-9E1B-937782E0DE49}");
    }

    [Test]
    public async void TestGetNotExistedFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var fields = new Collection<string>
      {
        "",
      };
      var request = requestBuilder.RequestWithSitecoreQuery(testData.Items.TestFieldsItem.Path).LoadFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

     testData.AssertItemsCount(1, response);
     testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response.Items[0]);
      ScItem item = response.Items[0];
    //  Assert.AreEqual(0, item.Fields.Count());
    }

    [Test]
    public async void TestGetFieldsWithEnglishLanguage()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = requestBuilder.RequestWithId(testData.Items.ItemWithVersions.Id).LoadFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(1, item.Fields.Count());
      //Assert.AreEqual("English version 2 web",item.Fields("title").RawValue);
    }

    [Test]
    public async void TestGetFieldsWithDanishLanguage()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = requestBuilder.RequestWithId(testData.Items.ItemWithVersions.Id).LoadFields(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(1, item.Fields.Count());
      //Assert.AreEqual("Danish version 2 web",item.Fields("title").RawValue);
    }
    [Test]
    public async void TestGetItemByIdWithInvalidFieldName()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var fields = new Collection<string>
      {
        "!@#$%^&*()_+*/"
      };
      var request = requestBuilder.RequestWithId(testData.Items.ItemWithVersions.Id).LoadFields(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ScItem item = response.Items[0];
     // Assert.AreEqual(0, item.Fields.Count());
    }

    [Test]
    public async void TestGetSeveralItemsByQueryWithContentFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithSitecoreQuery(testData.Items.Home.Path + "/*").Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      testData.AssertItemsCount(4, response);
      var expectedItemSamleTemplate = new TestEnvironment.Item
      {
        DisplayName = "Allowed_Parent",
        Id = "{2075CBFF-C330-434D-9E1B-937782E0DE49}",
        Path = "/sitecore/content/Home/Allowed_Parent",
        Template = "Sample/Sample Item"
      };
      testData.AssertItemsAreEqual(expectedItemSamleTemplate, response.Items[0]);
      //Assert.AreEqual("Danish version 2 web",response.Items[0].Fields("title").RawValue);
      //Assert.AreEqual("",response.Items[0].Fields("Text").RawValue);

      var expectedItemTestTemplate = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };
      testData.AssertItemsAreEqual(expectedItemTestTemplate, response.Items[3]);
      //Assert.AreEqual("Normal Text",response.Items[3].Fields("Normal Text").RawValue);
      // Assert.AreEqual("1", response.Items[3].Fields("CheckBoxField").RawValue);
    }

    [Test]
    public async void TestGetItemByIdWithAllFieldsWithoutReadAcessToSomeFields()
    {
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Creatorex.Username, testData.Users.Creatorex.Password);
      this.sessionAuthenticatedUser = new ScApiSession(config, ItemSource.DefaultSource());

      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}").Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);

      var expectedItemTestTemplate = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };
      testData.AssertItemsAreEqual(expectedItemTestTemplate, response.Items[0]);
      ScItem item = response.Items[0];

      //Assert.AreEqual("20120201T120000", item.Fields("DateTimeField").RawValue);
     // Assert.AreEqual("Text", item.Fields("Text").RawValue);  //this field doesn't exist
    }

    [Test]
    public async void TestGetFieldsWithSymbolsAndSpacesInNameFields()
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var fields = new Collection<string>
      {
        "Normal Text",
       "__Owner"  
      };
      var request = requestBuilder.RequestWithId("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}").LoadFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      var expectedItemTestTemplate = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };
      testData.AssertItemsAreEqual(expectedItemTestTemplate, response.Items[0]);
      ScItem item = response.Items[0];
      //Assert.AreEqual(2, item.Fields.Count());
      //Assert.AreEqual("sitecore\admin",item.Fields("__Owner").RawValue);
      //Assert.AreEqual("Normal Text",item.Fields("Normal Text").RawValue);
    }
  }
}