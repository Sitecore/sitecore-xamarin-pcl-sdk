using MobileSDKUnitTest.Mock;


namespace MobileSDKIntegrationTest
{
    using System;
    using NUnit.Framework;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;


	[TestFixture]
	public class GetItemsTest
	{
		private ScApiSession sessionWithAnonymousAccess;

		[SetUp]
		public void Setup()
		{
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "sitecore\\admin", "b");
            this.sessionWithAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());
		}

		[Test]
		public async void TestValidGetItemsRequest ()
		{
            string mediaLibraryId = "{3D6658D8-A0BF-4E75-B3E2-D050FABCF4E1}";

            var request = new MockGetItemsByIdParameters ();
            request.ItemId = mediaLibraryId;

            
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync (request);
			Assert.AreEqual (1, response.TotalCount);
			Assert.AreEqual (1, response.ResultCount);
			Assert.AreEqual (1, response.Items.Count);

            Assert.AreEqual("Media Library", response.Items[0].DisplayName);
        }

        [Test]
        public async void TestValidGetItemsByPathRequest()
        {
            string homePath = "/sitecore/content/home";

            var request = new MockGetItemsByPathParameters ();
            request.ItemPath = homePath;

            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual("Home", response.Items[0].DisplayName);
        }

        [Test]
        public async void TestValidGetItemsByQueryRequest()
        {
            string query = "/sitecore/content/HOME/AllowED_PARent/*";

            var request = new MockGetItemsByQueryParameters ();
            request.SitecoreQuery = query;

            
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);

            Assert.AreEqual(2, response.TotalCount );
            Assert.AreEqual(2, response.ResultCount);
            Assert.AreEqual(2, response.Items.Count);
        }

        [TearDown]
        public void TearDown()
        {
            this.sessionWithAnonymousAccess = null;
        }
    }
}