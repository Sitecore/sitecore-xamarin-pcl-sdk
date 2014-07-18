

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;


  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.Items;


  [TestFixture]
  public class ItemSourceMergerTest
  {
    [Test]
    public void TestItemSourceMergerCopiesInputStruct()
    {
      IItemSource source = new ItemSourcePOD("master", "da", "100500" );
      var merger = new ItemSourceFieldMerger(source);

      Assert.AreNotSame(source, merger.DefaultSource);
      Assert.AreEqual(source, merger.DefaultSource);
    }

    [Test]
    public void TestItemSourceMergerDefaultValuesAreOptional()
    {
      var result = new ItemSourceFieldMerger(null);
      Assert.IsNotNull(result);
    }

    [Test]
    public void TestDatabaseAndLanguageAreOptionalForDefaultSource()
    {
      IItemSource noDatabase = new ItemSourcePOD(null    , "en", "1" );
      IItemSource noLanguage = new ItemSourcePOD("master", null, "1" );
      IItemSource noVersion  = new ItemSourcePOD("master", "en", null);


      Assert.DoesNotThrow( () => new ItemSourceFieldMerger(noDatabase) );
      Assert.DoesNotThrow( () => new ItemSourceFieldMerger(noLanguage) );
      Assert.DoesNotThrow( () => new ItemSourceFieldMerger(noVersion ) );
    }

    [Test]
    public void TestMergerReturnsDefaultSourceCopyForNilInput()
    {
      ItemSource defaultSource = ItemSource.DefaultSource();

      var merger = new ItemSourceFieldMerger(defaultSource);
      IItemSource result = merger.FillItemSourceGaps(null);

      Assert.AreNotSame(defaultSource, result);
      Assert.AreEqual(defaultSource, result);
    }


    [Test]
    public void TestMergerReturnsInputSourceCopyForNilDefault()
    {
      ItemSource defaultSource = ItemSource.DefaultSource();

      var merger = new ItemSourceFieldMerger(null);
      IItemSource result = merger.FillItemSourceGaps(defaultSource);

      Assert.AreNotSame(defaultSource, result);
      Assert.AreEqual(defaultSource, result);
    }

    [Test]
    public void TestMergerReturnsNullIfBothInputAndDefaultAreNull()
    {
      var merger = new ItemSourceFieldMerger(null);
      IItemSource result = merger.FillItemSourceGaps(null);

      Assert.IsNull(result);
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

