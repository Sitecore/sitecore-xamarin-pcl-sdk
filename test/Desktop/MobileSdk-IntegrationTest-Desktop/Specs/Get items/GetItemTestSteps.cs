using System.Configuration;
using NUnit.Framework;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.SessionSettings;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MobileSdk_IntegrationTest_Desktop
{
    [Binding]
    public class GetItemTestSteps
    {
        [Given(@"I have logged in ""(.*)""")]
        public void GivenIHaveLoggedIn(string instance)
        {
            ScenarioContext.Current["InstanceUrl"] = instance;
        }

        [Given(@"I have logged in authenticated instance")]
        public void GivenIHaveLoggedInAuthenticatedInstance()
        {
            ScenarioContext.Current["InstanceUrl"] = ConfigurationManager.AppSettings["authenticatedInstanceURL"];
        }


        [Given(@"I have choosed user")]
        public void GivenIHaveChoosedUser(Table credentials)
        {
            var userCredentials = credentials.CreateInstance<UserTable>();

            SessionConfig config = new SessionConfig(ScenarioContext.Current.Get<string>("InstanceUrl"), userCredentials.Username, userCredentials.Password);
            ScenarioContext.Current["ApiSession"] = new ScApiSession(config, ItemSource.DefaultSource());
        }


        [When(@"I send request to get item by id ""(.*)""")]
        public void WhenISendRequestToGetItemById(string itemId)
        {
            var apiSession = ScenarioContext.Current.Get<ScApiSession>("ApiSession");
            string id = ConfigurationManager.AppSettings[itemId];
            ScenarioContext.Current["Response"] = apiSession.ReadItemByIdAsync(id).Result;
        }

        [When(@"I send request to get item by path ""(.*)""")]
        public void WhenISendRequestToGetItemByPath(string itemPath)
        {
            var apiSession = ScenarioContext.Current.Get<ScApiSession>("ApiSession");
            string path = ConfigurationManager.AppSettings[itemPath];
            ScenarioContext.Current["Response"] = apiSession.ReadItemByPathAsync(path).Result;
        }

        [Then(@"I've got (.*) items in response")]
        public void ThenIVeGotItemsInResponse(int count)
        {
            var response = ScenarioContext.Current.Get<ScItemsResponse>("Response");
            Assert.AreEqual(count, response.TotalCount);
            Assert.AreEqual(count, response.Items.Count);
        }


        [Then(@"This is Home item")]
        public void ThenThisIsHomeItem()
        {
            var response = ScenarioContext.Current.Get<ScItemsResponse>("Response");
            var item = response.Items[0];
            Assert.AreEqual("Home", item.DisplayName);
            Assert.AreEqual(ConfigurationManager.AppSettings["HomeItemId"], item.Id);
            Assert.AreEqual("Sample/Sample Item", item.Template);
        }

        [Then(@"This is ""(.*)"" item with path ""(.*)"" and template ""(.*)""")]
        public void ThenThisIsItemWithPathAndTemplate(string itemName, string itemPath, string itemTemplate)
        {
            var response = ScenarioContext.Current.Get<ScItemsResponse>("Response");
            var item = response.Items[0];
            Assert.AreEqual(itemName, item.DisplayName);
            Assert.AreEqual(ConfigurationManager.AppSettings[itemPath], item.Path);
            Assert.AreEqual(itemTemplate, item.Template);
        }

        public class UserTable
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }
    }
}
