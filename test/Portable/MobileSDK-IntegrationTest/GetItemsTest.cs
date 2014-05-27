using NUnit.Framework;
using System;
using Sitecore.MobileSDK;
using System.Net.Http;
using Sitecore.MobileSDK.Items;

namespace MobileSDKIntegrationTest
{
	[TestFixture]
	public class GetItemsTest
	{
		private ScApiSession sessionWithAnonymousAccess;

		private HttpClient httpClient;

		[SetUp]
		public void Setup()
		{
			SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\creatorex", "creatorex");
			this.sessionWithAnonymousAccess = new ScApiSession(config);
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