

namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;


    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;


    [TestFixture]
    public class ItemSourceMergerTest
    {
        [Test]
        public void TestMergerRequiresDefaultValues()
        {
            Assert.Throws<ArgumentNullException>( () => new ItemSourceFieldMerger(null) );
        }

        [Test]
        public void TestDatabaseAndLanguageMustBeSetOnDefaultSource()
        {
            IItemSource noDatabase = new ItemSourcePOD (null, "en", "1");
            IItemSource noLanguage = new ItemSourcePOD ("master", null, "1");
            IItemSource noVersion = new ItemSourcePOD ("master", "en", null);


            Assert.Throws<ArgumentNullException>( () => new ItemSourceFieldMerger(noDatabase) );
            Assert.Throws<ArgumentNullException>( () => new ItemSourceFieldMerger(noLanguage) );
            Assert.DoesNotThrow( () => new ItemSourceFieldMerger(noVersion) );
        }

        [Test]
        public void TestMergerReturnsDefaultSourceForNilInput()
        {
            ItemSource defaultSource = ItemSource.DefaultSource ();

            var merger = new ItemSourceFieldMerger (defaultSource);
            IItemSource result = merger.FillItemSourceGaps (null);

            Assert.AreSame (defaultSource, result);
        }


        [Test]
        public void TestUserFieldsHaveHigherPriority()
        {
            var defaultSource = new ItemSourcePOD ("master", "en", "1");
            var userSource = new ItemSourcePOD ("web", "ua", "42");

            var merger = new ItemSourceFieldMerger (defaultSource);
            IItemSource result = merger.FillItemSourceGaps (userSource);

            Assert.AreEqual (userSource, result);
            Assert.AreNotSame (userSource, result);
        }

        [Test]
        public void TestNullUserFieldsAreAutocompleted()
        {
            var defaultSource = new ItemSourcePOD ("master", "en", "1");
            var userSource = new ItemSourcePOD (null, null, null);

            var merger = new ItemSourceFieldMerger (defaultSource);
            IItemSource result = merger.FillItemSourceGaps (userSource);

            Assert.AreEqual (defaultSource, result);
            Assert.AreNotSame (defaultSource, result);
        }
    }
}

