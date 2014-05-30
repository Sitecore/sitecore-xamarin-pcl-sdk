
namespace Sitecore.MobileSdkUnitTest
{
    using NUnit.Framework;

    using System;
    using System.Diagnostics;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;



    [TestFixture]
    public class ItemSourceTest
    {
        [Test]
        public void TestApiSessionConstructorRequiresDefaultSource()
        {
            SessionConfig config = new SessionConfig ("localhost", "alex.fergusson", "man u is a champion");

            TestDelegate initSessionAction = () =>
            {
                ScApiSession result = new ScApiSession (config, null);
                Debug.WriteLine( result );
            };

            Assert.Throws<ArgumentNullException>(initSessionAction);
        }


        [Test]
        public void TestApiSessionConstructorRequiresConfig()
        {
            ItemSource defaultSource = ItemSource.DefaultSource ();

            TestDelegate initSessionAction = () =>
            {
                ScApiSession result = new ScApiSession (null, defaultSource);
                Debug.WriteLine( result );
            };

            Assert.Throws<ArgumentNullException>(initSessionAction);
        }

        [Test]
        public void TestItemSourceRequiresDatabase()
        {
            Assert.Throws<ArgumentNullException> (() => new ItemSource (null, "en", "1"));
        }

        [Test]
        public void TestItemSourceRequiresLanguage()
        {
            Assert.Throws<ArgumentNullException> (() => new ItemSource ("master", null, "1"));
        }

        [Test]
        public void TestLanguageIsOptionalForItemSource()
        {
            Assert.DoesNotThrow (() => new ItemSource ("core", "da", null));
        }

        [Test]
        public void TestDefaultSource()
        {
            ItemSource defaultSource = ItemSource.DefaultSource ();

            Assert.AreEqual (defaultSource.Database, "web");
            Assert.AreEqual (defaultSource.Language, "en");

        }
    }
}

