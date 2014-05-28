
namespace MobileSDKIntegrationTest
{
    using System;
    using NUnit.Framework;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;


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
            ScItemsResponse response = await this.sessionWithAnonymousAccess.GetItemById (mediaLibraryId);
			Assert.AreEqual (1, response.TotalCount);
			Assert.AreEqual (1, response.ResultCount);
			Assert.AreEqual (1, response.Items.Count);
		}

		[TearDown]
		public void TearDown()
		{
			this.sessionWithAnonymousAccess = null;
		}
	}
}