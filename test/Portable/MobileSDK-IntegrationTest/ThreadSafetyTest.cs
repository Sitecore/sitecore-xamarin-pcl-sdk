﻿namespace MobileSDKIntegrationTest
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using NUnit.Framework;

  using SitecoreMobileSDKMockObjects;
  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
 
  using Sitecore.MobileSDK;
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
    private MediaLibrarySettings mediaSettings;

    [SetUp]
    public void Setup()
    {
      TestEnvironment env = TestEnvironment.DefaultTestEnvironment();

      this.mediaSettings = new MediaLibrarySettings(
        "/sitecore/media library",
        "ashx",
        "~/media/");

      var connection = new SessionConfig(this.env.InstanceUrl);
      var defaultSource = ItemSource.DefaultSource();

      this.session = new ScTestApiSession(connection, env.Users.Admin, this.mediaSettings, defaultSource);
      this.env = env;

      this.itemSource = new MutableItemSource("master", "en", "3872");
    }

    [TearDown]
    public void TearDown()
    {
      this.itemSource = null;
      this.session = null;
      this.env = null;
      this.mediaSettings = null;
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
        
      string mediaPath = "/sitecore/media library/xyz";

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

