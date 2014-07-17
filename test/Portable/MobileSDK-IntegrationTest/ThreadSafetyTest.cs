using Sitecore.MobileSDK;
using SitecoreMobileSDKMockObjects;
using Sitecore.MobileSDK.Exceptions;

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
    private MutableItemSource itemSource;

    [SetUp]
    public void Setup()
    {
      TestEnvironment env = TestEnvironment.DefaultTestEnvironment();

      var connection = SessionConfig.NewAuthenticatedSessionConfig(
                   env.InstanceUrl,
                   env.Users.Admin.Login,
                   env.Users.Admin.Password);
      var defaultSource = ItemSource.DefaultSource();

      this.session = new ScTestApiSession(connection, defaultSource);
      this.env = env;

      this.itemSource = new MutableItemSource("master", "en", "3872");
    }

    [TearDown]
    public void TearDown()
    {
      this.itemSource = null;
      this.session = null;
      this.env = null;
    }

    [Test]
    public async void TestItemByIdRequestMutationDoesNotAffectSession()
    {
      string homeId = this.env.Items.Home.Id;

      IQueryParameters payload = new QueryParameters(PayloadType.Min, null, null);

      MockGetItemsByIdParameters mockMutableRequest = new MockGetItemsByIdParameters();
      mockMutableRequest.ItemId = homeId;
      mockMutableRequest.QueryParameters = payload;
      mockMutableRequest.ItemSource = this.itemSource;

      try
      {
        Task<ScItemsResponse> loadItemsTask = this.session.ReadItemAsync(mockMutableRequest);
  //      await Task.Factory.StartNew(() => mockMutableRequest.ItemId = this.env.Items.MediaLibrary.Id);
        ScItemsResponse response = await loadItemsTask;
        var item = response.Items[0];
        Assert.AreEqual(homeId, item.Id);
      }
      catch (LoadDataFromNetworkException)
      {
        //IDLE
      }
      finally
      {
        Assert.AreEqual(1, this.itemSource.CopyInvocationCount);
        Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
      }
    }

    [Test]
    public async void TestItemByPathRequestMutationDoesNotAffectSession()
    {
      string homePath = "/sitecore/content/home";

      IQueryParameters payload = new QueryParameters(PayloadType.Min, null, null);

      MockGetItemsByPathParameters mockMutableRequest = new MockGetItemsByPathParameters();
      mockMutableRequest.ItemPath = homePath;
      mockMutableRequest.QueryParameters = payload;
      mockMutableRequest.ItemSource = this.itemSource;

      try
      {
        Task<ScItemsResponse> loadItemsTask = this.session.ReadItemAsync(mockMutableRequest);
        //      await Task.Factory.StartNew(() => mockMutableRequest.ItemPath = this.env.Items.MediaLibrary.Path);
        ScItemsResponse response = await loadItemsTask;
        var item = response.Items[0];
        Assert.AreEqual(homePath.ToLowerInvariant(), item.Path.ToLowerInvariant());
      }
      catch (LoadDataFromNetworkException)
      {
        //IDLE
      }
      finally
      {
        Assert.AreEqual(1, this.itemSource.CopyInvocationCount);
        Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
      }
    }

    [Test]
    public async void TestItemByQueryRequestMutationDoesNotAffectSession()
    {
      string homePath = "/sitecore/content/home";

      IQueryParameters payload = new QueryParameters(PayloadType.Min, null, null);

      MockGetItemsByQueryParameters mockMutableRequest = new MockGetItemsByQueryParameters();
      mockMutableRequest.SitecoreQuery = homePath;
      mockMutableRequest.QueryParameters = payload;
      mockMutableRequest.ItemSource = this.itemSource;

      try
      {
        Task<ScItemsResponse> loadItemsTask = this.session.ReadItemAsync(mockMutableRequest);
        //      await Task.Factory.StartNew(() => mockMutableRequest.SitecoreQuery = "/sitecore/content/media library");
        ScItemsResponse response = await loadItemsTask;
        var item = response.Items[0];
        Assert.AreEqual(homePath.ToLowerInvariant(), item.Path.ToLowerInvariant());
      }
      catch (LoadDataFromNetworkException)
      {
        //IDLE
      }
      finally
      {
        Assert.AreEqual(1, this.itemSource.CopyInvocationCount);
        Assert.AreEqual(1, mockMutableRequest.CopyInvocationCount);
      }
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
        IReadMediaItemRequest request = new ReadMediaItemParameters(null, this.itemSource, resizing, mediaPath);

        using (Stream imageStream = await this.session.DownloadResourceAsync(request))
        {
          Assert.IsNotNull(imageStream);
        }
      }
      catch (LoadDataFromNetworkException)
      {
        //IDLE
      }
      finally
      {
        Assert.AreEqual(1, resizing.CopyConstructorInvocationCount);
        Assert.AreEqual(1, this.itemSource.CopyInvocationCount);
      }
    }
  }
}

