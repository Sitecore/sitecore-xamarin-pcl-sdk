using System;
using System.Configuration;
using System.Xml;
using NUnit.Framework;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.SessionSettings;
using TechTalk.SpecFlow;

namespace MobileSdk_IntegrationTest_Desktop.Specs.Get_items
{
    using MobileSDKUnitTest.Mock;

    [Binding]
    public class GetPublicKeyTestSteps
    {
        [When(@"I try to get an item by id ""(.*)""")]
        public void WhenITryToGetAnItemById(string itemId)
        {
            var session = ScenarioContext.Current.Get<ScApiSession>("ApiSession");
            var request = new MockGetItemsByIdParameters
            {
                ItemId = ConfigurationManager.AppSettings[itemId]
            };
            TestDelegate action = async () =>
            {
                ScItemsResponse scItemsResponse = await session.ReadItemByIdAsync(request);
            };
            ScenarioContext.Current["Action"] = action;
        }

        [Then(@"I've got an ""(.*)"" error")]
        public void ThenIVeGotAnError(string type)
        {
            var action = ScenarioContext.Current.Get<TestDelegate>("Action");
            var exception = Assert.Throws<ArgumentNullException>(action, " it work ?");
            ScenarioContext.Current["ExceptionMessage"] = exception.Message;
        }

        [Then(@"the error message contains ""(.*)""")]
        public void ThenTheErrorMessageIs(string message)
        {
            Assert.True(ScenarioContext.Current.Get<string>("ExceptionMessage").Contains(message));
        }

        [Given(@"I have tried to connect as admin user")]
        public void GivenIHaveTriedToConnectAsAdminUser()
        {
            string errorMessage = "";
            try
            {
                SessionConfig config = new SessionConfig(ScenarioContext.Current.Get<string>("InstanceUrl"), "admin", "b");
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
            ScenarioContext.Current["ExceptionMessage"] = errorMessage;
        }


    }
}
