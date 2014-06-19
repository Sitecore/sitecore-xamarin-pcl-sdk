using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using System.Threading;
using Sitecore.MobileSDK;
using System.Threading.Tasks;

namespace MobileSDKIntegrationTest
{
    using System;
    using NUnit.Framework;


    [TestFixture]
    public class OperationCancelTest
    {
        private ScTestApiSession session;
        private ItemWebApiRequestBuilder requestBuilder;
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
            this.requestBuilder = null;
        }


        [Test]
        [ExpectedException(typeof(TaskCanceledException))]
        public async void TestCancelExceptionIsNotWrappedForGetPublicKeyRequest()
        {
            //        [ExpectedException(typeof(OperationCanceledException))]

            var cancel = new CancellationTokenSource ();

            Task<PublicKeyX509Certificate> action = this.session.GetPublicKeyAsyncPublic(cancel.Token);
            cancel.Cancel();
         
            await action;
            Assert.Fail ("OperationCanceledException not thrown");
        }


        [Test]
        [ExpectedException(typeof(TaskCanceledException))]
        public async void TestCancelExceptionIsNotWrappedForItemByIdRequest()
        {
            //        [ExpectedException(typeof(OperationCanceledException))]

            var cancel = new CancellationTokenSource ();
            var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId (this.env.Items.Home.Id).Build();

            Task<ScItemsResponse> action = this.session.ReadItemAsync (request, cancel.Token);
            cancel.Cancel();

            await action;
            Assert.Fail ("OperationCanceledException not thrown");
        }



        [Test]
        [ExpectedException(typeof(TaskCanceledException))]
        public async void TestCancelExceptionIsNotWrappedForItemByPathRequest()
        {
            //        [ExpectedException(typeof(OperationCanceledException))]

            var cancel = new CancellationTokenSource ();
            var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath ("/sitecore/content/home").Build();

            Task<ScItemsResponse> action = this.session.ReadItemAsync (request, cancel.Token);
            cancel.Cancel();

            await action;
            Assert.Fail ("OperationCanceledException not thrown");
        }


        [Test]
        [ExpectedException(typeof(TaskCanceledException))]
        public async void TestCancelExceptionIsNotWrappedForItemByQueryRequest()
        {
            //        [ExpectedException(typeof(OperationCanceledException))]

            var cancel = new CancellationTokenSource ();
            var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery ("/sitecore/content/home/*").Build();

            Task<ScItemsResponse> action = this.session.ReadItemAsync (request, cancel.Token);
            cancel.Cancel();

            await action;
            Assert.Fail ("OperationCanceledException not thrown");
        }
    }
}

