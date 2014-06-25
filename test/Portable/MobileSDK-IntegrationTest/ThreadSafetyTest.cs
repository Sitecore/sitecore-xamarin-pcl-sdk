

namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;

  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Items;


  [TestFixture]
  public class ThreadSafetyTest
  {
    private ScTestApiSession session;
    private TestEnvironment env;

    [SetUp]
    public void Setup()
    {
      TestEnvironment env = TestEnvironment.DefaultTestEnvironment();

      var connection = new SessionConfig(
                   env.InstanceUrl,
                   env.Users.Admin.Username,
                   env.Users.Admin.Password);
      var defaultSource = ItemSource.DefaultSource();

      this.session = new ScTestApiSession(connection, defaultSource);
      this.env = env;
    }

    [TearDown]
    public void TearDown()
    {
      this.session = null;
      this.env = null;
    }

    [Test]
    public async void TestItemByIdRequestMutationDoesNotAffectSession()
    {
      string homeId = this.env.Items.Home.Id;

      IQueryParameters payload = new QueryParameters(PayloadType.Min, null);

      MockGetItemsByIdParameters mockMutableRequest = new MockGetItemsByIdParameters();
      mockMutableRequest.ItemId = homeId;
      mockMutableRequest.QueryParameters = payload;
      mockMutableRequest.ItemSource = new ItemSourcePOD(null, null, null);

      Task<ScItemsResponse> loadItemsTask = this.session.ReadItemAsync(mockMutableRequest);
//      await Task.Factory.StartNew(() => mockMutableRequest.ItemId = this.env.Items.MediaLibrary.Id);
      ScItemsResponse response = await loadItemsTask;
//      var item = response.Items[0];
//      Assert.AreEqual(homeId, item.Id);

      Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
    }


  }
}

