using System;
using System.Configuration;
using System.Threading;
using NUnit.Framework;

using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;
using TechTalk.SpecFlow;

namespace MobileSdk_IntegrationTest_Desktop
{
    [Binding]
    public class GetItemTestSteps
    {
        private readonly string _instanceUrl = ConfigurationManager.AppSettings["anonymousInstanceURL"];
        private const string HomeItemId = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";

        [Given(@"I logged in as sitecore admin user")]
        public void GivenILoggedInAsSitecoreAdminUser()
        {
            SessionConfig config = new SessionConfig(_instanceUrl, "sitecore\\admin", "b");
            ScenarioContext.Current["ApiSession"] = new ScApiSession(config, ItemSource.DefaultSource());
        }

        [When(@"I send request to get Home item by ID")]
        public async void WhenISendRequestToGetHomeItemById()
        {
            
            var apiSession = ScenarioContext.Current.Get<ScApiSession>("ApiSession");
            ScenarioContext.Current["Response"] = await apiSession.GetItemById(HomeItemId);
        }

        [Then(@"I've got one item in response")]
        public void ThenIVeGotOneItemInResponse()
        {
            Thread.Sleep(1000); //how can we avoid delays?!!! does specflow support async operations?
            var response = ScenarioContext.Current.Get<ScItemsResponse>("Response");
            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.Items.Count);
            ScenarioContext.Current["Item"] = response.Items[0];
        }

        [Then(@"This is Home item")]
        public void ThenThisIsHomeItem()
        {
            var item = ScenarioContext.Current.Get<ScItem>("Item");
            Assert.AreEqual("Home", item.DisplayName);
            Assert.AreEqual(HomeItemId, item.Id);
            Assert.AreEqual("Sample/Sample Item", item.Template);
        }

    }
}
