using System;
using System.Configuration;
using System.Xml;
using NUnit.Framework;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;
using TechTalk.SpecFlow;

namespace MobileSdk_IntegrationTest_Desktop.Specs.Get_items
{
    [Binding]
    public class GetPublicKeyTestSteps
    {
        [Given(@"I set incorrect instance Url")]
        public void GivenISetIncorrectInstanceUrl()
        {
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dddk.sitecore.net", "sitecore\\admin", "b");
            ScenarioContext.Current["ApiSession"] = new ScApiSession(config, ItemSource.DefaultSource());
        }

        [When(@"I try to get an item")]
        public void WhenITryToGetAnItem()
        {
            ScApiSession session = ScenarioContext.Current.Get<ScApiSession>("ApiSession");
            TestDelegate action = () =>
            {
                var response = session.GetItemById("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}").Result;
            };
            ScenarioContext.Current["Action"] = action;
        }


        [Then(@"I've got an AggregateException error")]
        public void ThenIVeGotAnXmlError()
        {
            //what exception type should be here?
            var exception = Assert.Throws<AggregateException>(ScenarioContext.Current.Get<TestDelegate>("Action"), "we should get xml error here");
            StringAssert.Contains("Cannot connect to the specified URL", exception.Message);
        }
    }
}
