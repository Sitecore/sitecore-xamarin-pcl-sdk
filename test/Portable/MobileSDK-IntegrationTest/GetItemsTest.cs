
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
			SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\creatorex", "creatorex");
            this.sessionWithAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());
		}

		[Test]
		public async void TestValidGetItemsRequest ()
		{
			ScItemsResponse response = await this.sessionWithAnonymousAccess.GetItemById ("id");
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