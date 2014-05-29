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

        [Given(@"I logged in as sitecore admin user")]
        public void GivenILoggedInAsSitecoreAdminUser()
        {
            SessionConfig config = new SessionConfig(_instanceUrl, "sitecore\\admin", "b");
            ScenarioContext.Current["ApiSession"] = new ScApiSession(config, ItemSource.DefaultSource());
        }

        [When(@"I send request to get Home item by ID")]
        public async void WhenISendRequestToGetHomeItemById()
        {
            const string homeItemId = "{3D6658D8-A0BF-4E75-B3E2-D050FABCF4E1}";
            var apiSession = ScenarioContext.Current.Get<ScApiSession>("ApiSession");
            ScenarioContext.Current["Response"] = await apiSession.GetItemById(homeItemId);
        }

        [Then(@"I've got the Home item in response")]
        public void ThenIVeGotTheHomeItemInResponse()
        {
            Thread.Sleep(1000); //how can we avoid delays?!!! does specflow support async operations?
            var response = ScenarioContext.Current.Get<ScItemsResponse>("Response");
            Assert.AreEqual(1, response.TotalCount);
            Assert.AreEqual(1, response.Items.Count);
        }
    }
}
