using Sitecore.MobileSDK;
using SitecoreMobileSDKMockObjects;

namespace MobileSDKIntegrationTest
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
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
      var item = response.Items[0];
      Assert.AreEqual(homeId, item.Id);

      Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
    }

    [Test]
    public async void TestItemByPathRequestMutationDoesNotAffectSession()
    {
      string homePath = "/sitecore/content/home";

      IQueryParameters payload = new QueryParameters(PayloadType.Min, null);

      MockGetItemsByPathParameters mockMutableRequest = new MockGetItemsByPathParameters();
      mockMutableRequest.ItemPath = homePath;
      mockMutableRequest.QueryParameters = payload;
      mockMutableRequest.ItemSource = new ItemSourcePOD(null, null, null);

      Task<ScItemsResponse> loadItemsTask = this.session.ReadItemAsync(mockMutableRequest);
      //      await Task.Factory.StartNew(() => mockMutableRequest.ItemPath = this.env.Items.MediaLibrary.Path);
      ScItemsResponse response = await loadItemsTask;
      var item = response.Items[0];
      Assert.AreEqual(homePath.ToLowerInvariant(), item.Path.ToLowerInvariant());

      Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
    }

    [Test]
    public async void TestItemByQueryRequestMutationDoesNotAffectSession()
    {
      string homePath = "/sitecore/content/home";

      IQueryParameters payload = new QueryParameters(PayloadType.Min, null);

      MockGetItemsByQueryParameters mockMutableRequest = new MockGetItemsByQueryParameters();
      mockMutableRequest.SitecoreQuery = homePath;
      mockMutableRequest.QueryParameters = payload;
      mockMutableRequest.ItemSource = new ItemSourcePOD(null, null, null);

      Task<ScItemsResponse> loadItemsTask = this.session.ReadItemAsync(mockMutableRequest);
      //      await Task.Factory.StartNew(() => mockMutableRequest.SitecoreQuery = "/sitecore/content/media library");
      ScItemsResponse response = await loadItemsTask;
      var item = response.Items[0];
      Assert.AreEqual(homePath.ToLowerInvariant(), item.Path.ToLowerInvariant());

      Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
    }

    [Test]
    public async void TestMediaOptionsMutationDoesNotAffectSession()
    {
      var resizing = new MockMutableMediaOptions();
      resizing.SetWidth(100);
      resizing.SetHeight(500);
        
      string mediaPath = "/sitecore/media library/xyz.png.ashx";

      try
      {
        IReadMediaItemRequest request = new ReadMediaItemParameters(null, ItemSource.DefaultSource(), resizing, mediaPath);

        using (Stream imageStream = await this.session.DownloadResourceAsync(request))
        {
          Assert.IsNotNull(imageStream);
        }
      }
      catch (Exception)
      {
        //IDLE
      }
      finally
      {
        Assert.AreEqual(1, resizing.CopyConstructorInvocationCount);
      }
    }
  }
}

