
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
        private string HomeitemId = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";
	    private string HomeitemPath = "/sitecore/content/Home";
	    private string HomeitemName="Home";
	    private string SampleitemTemplate = "Sample/Sample Item";

        // for this scenario we should created two the same items with path /sitecore/content/T E S T/i t e m
       private string ItemWithSpacesPath = "/sitecore/content/T E S T/i t e m";
       private string ItemWithSpacesName = "i t e m";

        [SetUp]
		public void Setup()
		{
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "sitecore\\admin", "b");
            this.sessionWithAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());
		}

		[Test]
		public async void TestGetItemById()
		{
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(HomeitemId);
			Assert.AreEqual (1, response.TotalCount);
			Assert.AreEqual (1, response.ResultCount);
			Assert.AreEqual (1, response.Items.Count);

            Assert.AreEqual(HomeitemName, response.Items[0].DisplayName);
            Assert.AreEqual(HomeitemId, response.Items[0].Id);
            Assert.AreEqual(SampleitemTemplate, response.Items[0].Template);
        }
                                  
        [Test]
        public async void TestGetItemByInvalidId()
        {
            string itemInvalidId = "{4%75_B3E2 D050FA|cF4E1}";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(itemInvalidId);
            Assert.AreEqual(0, response.TotalCount);
            Assert.AreEqual(0, response.ResultCount);
            Assert.AreEqual(0, response.Items.Count);
        }

        [Test]
        public async void TestGetItemByNotExistentId()
        {
            string NotExistentId = "{3D6658D8-QQQQ-QQQQ-B3E2-D050FABCF4E1}";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(NotExistentId);
            Assert.AreEqual(0, response.TotalCount);
            Assert.AreEqual(0, response.ResultCount);
            Assert.AreEqual(0, response.Items.Count);
        }

        [Test]
        public async void TestGetItemByPath()
        {
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(HomeitemPath);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual(HomeitemName, response.Items[0].DisplayName);
            Assert.AreEqual(HomeitemPath, response.Items[0].Path);
            Assert.AreEqual(SampleitemTemplate, response.Items[0].Template);
        }

        [Test]
        public async void TestGetItemByPathWithSpaces() 
       // for this scenario we should created item with path /sitecore/content/T E S T/i t e m
        {
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(ItemWithSpacesPath);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual(ItemWithSpacesName, response.Items[0].DisplayName);
            Assert.AreEqual(ItemWithSpacesPath, response.Items[0].Path);
            Assert.AreEqual(SampleitemTemplate, response.Items[0].Template);
        }

        [Test]
        public async void TestGetItemByPathForTwoItemsWithTheSamePathExist()
        // for this scenario we should created two the same items with path /sitecore/content/T E S T/i t e m
        {
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(ItemWithSpacesPath);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual(ItemWithSpacesName, response.Items[0].DisplayName);
            Assert.AreEqual(ItemWithSpacesPath, response.Items[0].Path);
            Assert.AreEqual(SampleitemTemplate, response.Items[0].Template);
        }

        [Test]
        public async void TestGetItemByNotExistentPath()
      {
            string PathNotExistent = "/not/existent/path";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(PathNotExistent);

            Assert.AreEqual(0, response.TotalCount);
            Assert.AreEqual(0, response.ResultCount);
            Assert.AreEqual(0, response.Items.Count);
        }

        [Test]
        public async void TestGetItemByPathWithInternationalName()
        {
            string ItemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(ItemInterationalPath);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
            Assert.AreEqual(ItemInterationalPath, response.Items[0].Path);
            Assert.AreEqual(SampleitemTemplate, response.Items[0].Template);
        }

        [Test]
        public async void TestGetItemByInternationalPath()
        {
            string ItemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮/ではまた明日";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(ItemInterationalPath);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual("ではまた明日", response.Items[0].DisplayName);
            Assert.AreEqual(ItemInterationalPath, response.Items[0].Path);
            Assert.AreEqual(SampleitemTemplate, response.Items[0].Template);
        }
        [Test]
        public async void TestGetItemsByQuery()
        {
            string query = "/sitecore/content/HOME/AllowED_PARent/*";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(query);

            Assert.AreEqual(2, response.TotalCount );
            Assert.AreEqual(2, response.ResultCount);
            Assert.AreEqual(2, response.Items.Count);
        }

        [Test]
        public async void TestGetItemByInternationalQuery()
        {
            string queryInternational = "/sitecore/content/HOME//*[@title='宇都宮']";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(queryInternational);

            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.ResultCount);
            Assert.AreEqual(1, response.Items.Count);

            Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
        }

        [Test]
        public async void TestGetItemByInvalidQuery()
        {
            string queryInvalid = "/sitecore/content/HOME/AllowED_PARent//*[@@templatekey123='sample item']";
            ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(queryInvalid);

            Assert.AreEqual(0, response.TotalCount);
            Assert.AreEqual(0, response.ResultCount);
            Assert.AreEqual(0, response.Items.Count);
        }
        [TearDown]
        public void TearDown()
        {
            this.sessionWithAnonymousAccess = null;
        }
    }
}