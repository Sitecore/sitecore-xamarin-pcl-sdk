namespace MobileSDKIntegrationTest
{
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [TestFixture]
  public class UrlAutocompleteTest
  {
    private TestEnvironment testData;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
    }

    [Test]
    public async void TestWithoutHttpInUrlByPath()
    {
      var urlWithoutHttp = this.RemoveHttpSymbols(this.testData.InstanceUrl);
      var response = await this.GetAuthencationRequestWithHomeItemPath(urlWithoutHttp);
      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestWithoutHttpInUrlAndWithTwoSlashInTheEndByPath()
    {
      var urlWithTwoSlash = testData.InstanceUrl + "//";
      var url = this.RemoveHttpSymbols(urlWithTwoSlash);
      var response = await this.GetAuthencationRequestWithHomeItemPath(url);
      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestWithoutHttpInUrlByQuery()
    {
      var urlWithoutHttp = this.RemoveHttpSymbols(this.testData.InstanceUrl);
      var response = await this.GetAuthencationRequestWithHomeItemQuery(urlWithoutHttp);
      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestWithHttpInUrlAndWithOneSlashInTheEndByQuery()
    {
      var urlWithOneSlash = testData.InstanceUrl + "/";
      var response = await this.GetAuthencationRequestWithHomeItemQuery(urlWithOneSlash);
      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestWithHttpsInUrlById()
    {
      var response = await this.GetAuthencationRequestWithHomeItemId("https://scmobileteam.sitecoretest.net");
      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    private async Task<ScItemsResponse> GetAuthencationRequestWithHomeItemPath(string url)
    {
      var session = this.testData.GetSession(url, this.testData.Users.Admin.Username, this.testData.Users.Admin.Password);
      var requestWithItemPath = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Payload(PayloadType.Content).Build();
      var response = await session.ReadItemAsync(requestWithItemPath);
      return response;
    }

    private async Task<ScItemsResponse> GetAuthencationRequestWithHomeItemQuery(string url)
    {
      var session = this.testData.GetSession(url, this.testData.Users.Admin.Username, this.testData.Users.Admin.Password);
      var requestWithItemQuery = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path).Payload(PayloadType.Content).Build();
      var response = await session.ReadItemAsync(requestWithItemQuery);
      return response;
    }

    private async Task<ScItemsResponse> GetAuthencationRequestWithHomeItemId(string url)
    {
      var session = this.testData.GetSession(url, this.testData.Users.Admin.Username, this.testData.Users.Admin.Password);
      var requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id).Payload(PayloadType.Content).Build();
      var response = await session.ReadItemAsync(requestWithItemId);
      return response;
    }

    private string RemoveHttpSymbols(string url)
    {
      return url.Remove(0, 7);
    }
  }
}
