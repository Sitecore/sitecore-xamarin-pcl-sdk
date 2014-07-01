
namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using NUnit.Framework;

  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK;



  [TestFixture]
  public class OperationCancelTest
  {
    private ScTestApiSession session;
    private TestEnvironment env;

    [SetUp]
    public void SetUp()
    {
      this.env = TestEnvironment.DefaultTestEnvironment ();

      SessionConfig config = new SessionConfig (this.env.InstanceUrl, this.env.Users.Admin.Username, this.env.Users.Admin.Password);
      ItemSource defaultSource = ItemSource.DefaultSource();

      this.session = new ScTestApiSession (config, defaultSource);
    }

    [TearDown]
    public void TearDown()
    {
      this.session = null;
    }


    [Test]
    public void TestCancelExceptionIsNotWrappedForGetPublicKeyRequest()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource ();

        Task<PublicKeyX509Certificate> action = this.session.GetPublicKeyAsyncPublic(cancel.Token);
        cancel.Cancel();

        await action;
      };
      Assert.Catch<OperationCanceledException>(testAction);
    }


    [Test]
    public void TestCancelExceptionIsNotWrappedForItemByIdRequest()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource ();
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId (this.env.Items.Home.Id).Build();

        Task<ScItemsResponse> action = this.session.ReadItemAsync (request, cancel.Token);
        cancel.Cancel();

        await action;
      };
      Assert.Catch<OperationCanceledException>(testAction);
    }



    [Test]
    public void TestCancelExceptionIsNotWrappedForItemByPathRequest()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource ();
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath ("/sitecore/content/home").Build();

        Task<ScItemsResponse> action = this.session.ReadItemAsync (request, cancel.Token);
        cancel.Cancel();

        await action;
      };
      Assert.Catch<OperationCanceledException>(testAction);
    }


    [Test]
    public void TestCancelExceptionIsNotWrappedForItemByQueryRequest()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource ();
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery ("/sitecore/content/home/*").Build();

        Task<ScItemsResponse> action = this.session.ReadItemAsync (request, cancel.Token);
        cancel.Cancel();

        await action;
      };
      Assert.Catch<OperationCanceledException>(testAction);
    }
  }
}

