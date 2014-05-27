


namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;
    using Sitecore.MobileSDK;
    using System.Diagnostics;

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
            SessionConfig config = new SessionConfig ("localhost", "alex.fergusson", "man u is a champion");
            ItemSource defaultSource = ItemSource.DefaultSource ();

            TestDelegate initSessionAction = () =>
            {
                ScApiSession result = new ScApiSession (null, defaultSource);
                Debug.WriteLine( result );
            };

            Assert.Throws<ArgumentNullException>(initSessionAction);
        }
    }
}

