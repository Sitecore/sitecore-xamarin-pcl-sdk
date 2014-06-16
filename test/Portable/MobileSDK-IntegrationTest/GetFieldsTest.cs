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
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.Home.Id);
      var request = requestBuilder.Payload(PayloadType.Content).Build();
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
      var requestBuilder = new ReadItemByPathRequestBuilder(testData.Items.Home.Path);
      var request = requestBuilder.Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);

      Assert.Greater(response.Items[0].Fields.Count,70);
      Assert.AreEqual("Home", response.Items[0].FieldWithName("__Display name").RawValue);
    }

    [Test]
    public async void TestGetItemByPathWithoutFields()
    {
      var requestBuilder = new ReadItemByPathRequestBuilder(testData.Items.Home.Path);
      var request = requestBuilder.Payload(PayloadType.Min).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
      Assert.AreEqual(0, response.Items[0].Fields.Count);
    }

    [Test]
    public async void TestGetItemByQueryWithContentFields()
    {
      var requestBuilder = new ReadItemByQueryRequestBuilder("/sitecore/content/Home/ancestor-or-self::*");
      var request = requestBuilder.Payload(PayloadType.Content).Build();
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
      var requestBuilder = new ReadItemByPathRequestBuilder("/sitecore/content/Home/Android/Static/Japanese/宇都宮");
      var request = requestBuilder.Payload(PayloadType.Content).Build();
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
      var requestBuilder = new ReadItemByPathRequestBuilder(testData.Items.Home.Path);
      var request = requestBuilder.Payload(PayloadType.Content).Build();
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
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.TestFieldsItem.Id);
      var fields = new Collection<string>
      {
        "CheckBoxField",
        "MultiListField"
      };
      var request = requestBuilder.LoadFields(fields).Build();
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
      var requestBuilder = new ReadItemByQueryRequestBuilder(testData.Items.TestFieldsItem.Path);
      var fields = new Collection<string>
      {
        "",
      };
      var request = requestBuilder.LoadFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

     testData.AssertItemsCount(1, response);
     testData.AssertItemsAreEqual(testData.Items.TestFieldsItem, response.Items[0]);
     Assert.AreEqual(0, response.Items[0].Fields.Count);
    }

    [Test]
    public async void TestGetFieldsWithEnglishLanguage()
    {
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.ItemWithVersions.Id);
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = requestBuilder.LoadFields(fields).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ISitecoreItem item = response.Items[0];
      Assert.AreEqual(1, item.Fields.Count);
      Assert.AreEqual("English version 2 web",item.FieldWithName("title").RawValue);
    }

    [Test]
    public async void TestGetFieldsWithDanishLanguage()
    {
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.ItemWithVersions.Id);
      var fields = new Collection<string>
      {
        "Title"
      };
      var request = requestBuilder.LoadFields(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ISitecoreItem item = response.Items[0];
      Assert.AreEqual(1, item.Fields.Count);
      Assert.AreEqual("Danish version 2 web",item.FieldWithName("title").RawValue);
    }

    [Test]
    public async void TestGetItemByIdWithInvalidFieldName()
    {
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.ItemWithVersions.Id);
      var fields = new Collection<string>
      {
        "!@#$%^&*()_+*/"
      };
      var request = requestBuilder.LoadFields(fields).Language("da").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, response.Items[0]);
      ISitecoreItem item = response.Items[0];
      Assert.AreEqual(0, item.Fields.Count);
    }

    [Test]
    public async void TestGetSeveralItemsByQueryWithContentFields()
    {
      var requestBuilder = new ReadItemByQueryRequestBuilder(testData.Items.Home.Path + "/*");
      var request = requestBuilder.Payload(PayloadType.Full).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
      var expectedItemSamleTemplate = new TestEnvironment.Item
      {
        DisplayName = "Allowed_Parent",
        Id = "{2075CBFF-C330-434D-9E1B-937782E0DE49}",
        Path = "/sitecore/content/Home/Allowed_Parent",
        Template = "Sample/Sample Item"
      };
      testData.AssertItemsAreEqual(expectedItemSamleTemplate, response.Items[0]);

      Assert.AreEqual("Allowed_Parent", response.Items[0].FieldWithName("Title").RawValue);
      Assert.AreEqual("", response.Items[0].FieldWithName("Text").RawValue);

      var expectedItemTestTemplate = new TestEnvironment.Item
      {
        DisplayName = "Test Fields",
        Template = "Test Templates/Sample fields"
      };
      testData.AssertItemsAreEqual(expectedItemTestTemplate, response.Items[3]);

      Assert.AreEqual("Text", response.Items[3].FieldWithName("Text").RawValue);
      Assert.AreEqual("1", response.Items[3].FieldWithName("CheckBoxField").RawValue);
      Assert.AreEqual("Normal Text",response.Items[3].FieldWithName("Normal Text").RawValue);     
    }

    [Test]
    public async void TestGetItemByIdWithAllFieldsWithoutReadAcessToSomeFields()
    {
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Creatorex.Username, testData.Users.Creatorex.Password);
      var sessionCreatorexUser = new ScApiSession(config, ItemSource.DefaultSource());
      var requestBuilder = new ReadItemByIdRequestBuilder("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}");
      var request = requestBuilder.Payload(PayloadType.Content).Build();

      var responseCreatorex = await sessionCreatorexUser.ReadItemAsync(request);
      var responseAdmin = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      Assert.Less(responseCreatorex.Items[0].Fields.Count, responseAdmin.Items[0].Fields.Count); 
    }

    [Test]
    public async void TestGetFieldsWithSymbolsAndSpacesInNameFields()
    {
      var requestBuilder = new ReadItemByIdRequestBuilder("{00CB2AC4-70DB-482C-85B4-B1F3A4CFE643}");
      var fields = new Collection<string>
      {
        "Normal Text",
       "__Owner"  
      };
      var request = requestBuilder.LoadFields(fields).Build();
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