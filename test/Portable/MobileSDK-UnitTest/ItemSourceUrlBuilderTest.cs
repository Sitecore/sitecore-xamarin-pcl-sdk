

namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;

    using Sitecore.MobileSDK.UrlBuilder;



    [TestFixture]
    public class ItemSourceUrlBuilderTest
    {
        [Test]
        public void UrlBuilderRejectsNilSource()
        {
            Assert.Throws<ArgumentNullException> (() => new ItemSourceUrlBuilder (null));
        }
    }
}

