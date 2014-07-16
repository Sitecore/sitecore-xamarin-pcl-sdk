namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  [TestFixture]
  public class CreateItemsTest
  {
    private TestEnvironment        testData;
    private ISitecoreWebApiSession session ;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      session =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .Site(testData.ShellSite)
          .BuildSession();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session = null;
    }
    /*
    [Test]
    public async void TestCreateItemByIdWithoutFieldsSet()
    {
      var expectedItem = this.CreateTestItem("Create by parent id");

      var request = this.CreateByIdRequestBuilder()
         .ItemTemplate(expectedItem.Template)
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }

    [Test]
    public async void TestCreateItemByPathAndTemplateIdWithoutFieldsSet()
    {
      var expectedItem = this.CreateTestItem("Create by parent path and template ID");

      var request = this.CreateByIdRequestBuilder()
         .ItemTemplate("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}")
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }
    

    [Test]
    public async void TestCreateItemByPathWithSpecifiedFields()
    {
      var expectedItem = this.CreateTestItem("Create with fields");
      const string CreatedTitle = "Created title";
      const string CreatedText = "Created text";
      var request = this.CreateByPathRequestBuilder()
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName("Title", CreatedTitle)
         .AddFieldsRawValuesByName("Text", CreatedText)
         .AddFields("Text", "Title")
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(CreatedText, resultItem.FieldWithName("Text").RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }

    [Test]
    public async void TestCreateItemByIdWithInternationalNameAndFields()
    {
      var expectedItem = this.CreateTestItem("International Слава Україні _ ウクライナへの栄光");
      const string CreatedTitle = "ఉక్రెయిన్ కు గ్లోరీ Ruhm für die Ukraine";
      const string CreatedText = "युक्रेन गौरव גלורי לאוקראינה";
      var request = this.CreateByIdRequestBuilder()
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName("Title", CreatedTitle)
         .AddFieldsRawValuesByName("Text", CreatedText)
         .Payload(PayloadType.Content)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(CreatedText, resultItem.FieldWithName("Text").RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }

    [Test]
    public async void TestCreateItemByIdWithNotExistentFields()
    {
      var expectedItem = this.CreateTestItem("Set not existent field");
      const string CreatedTitle = "Existent title";
      const string CreatedTexttt = "Not existent texttt";
      var request = this.CreateByIdRequestBuilder()
         .Payload(PayloadType.Content)
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName("Title", CreatedTitle)
         .AddFieldsRawValuesByName("Texttt", CreatedTexttt)

         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(null, resultItem.FieldWithName("Texttt"));

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }

    [Test]
    public async void TestCreateItemByPathAndSetFieldWithSpacesInName()
    {
      var expectedItem = this.CreateTestItem("Set standard field value");
      const string FieldName = "__Standard values";
      const string FieldValue = "Created standard value 000!! ))";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName)
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName(FieldName, FieldValue)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(FieldValue, resultItem.FieldWithName(FieldName).RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }

    [Test]
    public async void TestCreateItemByIdAndSetHtmlFieldValue()
    {
      var expectedItem = this.CreateTestItem("Set HTML in field");
      const string FieldName = "Text";
      const string FieldValue = "<div>Welcome to Sitecore!</div><div><br /><a href=\"~/link.aspx?_id=A2EE64D5BD7A4567A27E708440CAA9CD&amp;_z=z\">Accelerometer</a></div>";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName)
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName(FieldName, FieldValue)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(FieldValue, resultItem.FieldWithName(FieldName).RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }
    */
    [Test]
    public void TestCreateItemByIdAndGetDuplicateFields()
    {
      const string FieldName = "Text";
      Exception exception = Assert.Throws<ArgumentException>(() => 
        ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
         .AddFields(FieldName, "Title", FieldName)
         .ItemName("Get duplicate fields")
         .ItemTemplate(testData.Items.Home.Template)
         .Build());
      Assert.AreEqual("RequestBuilder : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathAndSetDuplicateFields()
    {
      const string FieldName = "Text";
      const string FieldValue = "Duplicate value";
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemName("Set duplicate fields")
         .AddFieldsRawValuesByName(FieldName, FieldValue)
         .ItemTemplate(testData.Items.Home.Template)
         .AddFieldsRawValuesByName(FieldName, FieldValue)
         .Build());
      Assert.AreEqual("RequestBuilder : duplicate fields are not allowed", exception.Message);
    }
    /*
    [Test]
    public async void TestCreateItemByPathAndGetInvalidEmptyAndNullFields()
    {
      var expectedItem = this.CreateTestItem("Create and get invalid field");
      const string FieldName = "@*<<invalid!`fieldname=)";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName, null, "")
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(0, resultItem.Fields.Count);

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }
    
    [Test]
    public async void TestCreateItemByIdAndSetInvalidEmptyAndNullFields()
    {
      var expectedItem = this.CreateTestItem("Create and set invalid field");
      const string FieldName = "@*<<%#==_&@";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName)
         .AddFieldsRawValuesByName(FieldName, FieldName)
         .AddFieldsRawValuesByName(null, "")
         .AddFieldsRawValuesByName("", null)
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(0, resultItem.Fields.Count);

      this.GetAndCheckItem(expectedItem, resultItem);
      //this.RemoveItem(resultItem);
    }
    */
    [Test]
    public void TestCreateItemByIdWithEmptyName()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
         .ItemName("")
         .ItemTemplate(testData.Items.Home.Template)
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemName : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInItemName()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemName("  ")
         .ItemTemplate(testData.Items.Home.Template)
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemName : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByQueryWithNullItemName()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => /*Change to SitecoreQuery*/ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemName(null)
         .ItemTemplate(testData.Items.Home.Template)
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemName : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithoutItemName()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplate(testData.Items.Home.Template)
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemName : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByIdWithInvalidItemName()
    {
      const string ItemName = "@*<<%#==_&@";
      var request = CreateByPathRequestBuilder()
         .ItemName(ItemName)
         .Build();
      TestDelegate testCode = async () =>
      {
        var task = session.CreateItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("An item name cannot contain any of the following characters: \\/:?\"<>|[] (controlled by the setting InvalidItemNameChars)", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByPathWithInvalidItemTemplate()
    {
      const string Template = "@*<<%#==_&@";
      var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .Database("master")
        .ItemName("item with invalid template")
        .ItemTemplate(Template)
        .Build();
      TestDelegate testCode = async () =>
      {
        var task = session.CreateItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Template item not found.", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByIdWithoutItemTemplate()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
        .ItemName("Item without template")
        .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemTemplate : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithEmptyTemplate()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemName("Item with empty template")
         .ItemTemplate("")
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemTemplate : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByQueryWithSpacesOnlyInTemplate()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => /*Put Query here*/ ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemName("Item with empty template")
         .ItemTemplate("  	")
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemTemplate : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestCreateItemByIdhWithNullTemplate()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
         .ItemName("Item with empty template")
         .ItemTemplate(null)
         .Build());
      Assert.AreEqual("AbstractCreateItemRequestBuilder.ItemTemplate : The input cannot be null or empty", exception.Message);
    }




    private async void GetAndCheckItem(TestEnvironment.Item expectedItem, ISitecoreItem resultItem)
    {
      var readResponse = await this.GetItemById(resultItem.Id);

      this.testData.AssertItemsCount(1, readResponse);
      this.testData.AssertItemsAreEqual(expectedItem, readResponse.Items[0]);
    }

    private async Task<ScItemsResponse> GetItemById(string id)
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(id).Database("master").Build();
      var response = await this.session.ReadItemAsync(request);
      return response;
    }

    private ICreateItemRequestParametersBuilder<ICreateItemByIdRequest> CreateByIdRequestBuilder()
    {
      return ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
        .ItemTemplate(testData.Items.Home.Template)
        .Database("master");
    }

    private ICreateItemRequestParametersBuilder<ICreateItemByPathRequest> CreateByPathRequestBuilder()
    {
      return ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .Database("master")
        .ItemTemplate(testData.Items.Home.Template);
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

    private ISitecoreItem CheckCreatedItem(ScItemsResponse createResponse, TestEnvironment.Item expectedItem)
    {
      this.testData.AssertItemsCount(1, createResponse);
      ISitecoreItem resultItem = createResponse.Items[0];
      this.testData.AssertItemsAreEqual(expectedItem, resultItem);
      return resultItem;
    }

  }
}