using NUnit.Framework;
using System;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;


namespace Sitecore.MobileSdkUnitTest
{
	using Sitecore.MobileSDK.MediaItems;
	using Sitecore.MobileSDK.UrlBuilder.Rest;

	[TestFixture ()]
	public class ResourseUrlBuilderTest
	{
		MediaItemUrlBuilder builder;

		[SetUp]
		public void SetUp()
		{
			IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();

			SessionConfig sessionConfig = new SessionConfig ("http://test.host", "a", "b");

			ItemSource itemSource = ItemSource.DefaultSource ();
			builder = new MediaItemUrlBuilder (restGrammar, sessionConfig, itemSource);
		}

		[TearDown]
		public void TearDown()
		{
			this.builder = null;
		}

		[Test ()]
		public void AbsolutePathTest ()
		{
			string result = builder.BuildUrlStringForPath ("/sitecore/media library/1.png", null);
			string expected = "http://test.host/~/media/1.png.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void RelativePathTest ()
		{
			string result = builder.BuildUrlStringForPath ("/mediaXYZ/1.png", null);
			string expected = "http://test.host/~/media/mediaXYZ/1.png.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void RelativePathAndExtensionTest ()
		{
			string result = builder.BuildUrlStringForPath ("/mediaXYZ/1.png.ashx", null);
			string expected = "http://test.host/~/media/mediaXYZ/1.png.ashx.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void PathContaignMediaHookTest ()
		{
			string result = builder.BuildUrlStringForPath ("~/media/1.png", null);
			string expected = "http://test.host/~/media/1.png.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void PathContaignMediaHookAndExtensionTest ()
		{
			string result = builder.BuildUrlStringForPath ("~/media/1.png.ashx", null);
			string expected = "http://test.host/~/media/1.png.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void PathProperlyEscapedTest ()
		{
			string result = builder.BuildUrlStringForPath ("~/media/Images/test image", null);
			string expected = "http://test.host/~/media/Images/test%20image.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void ResourceNameIsCaseSensitiveTest ()
		{
			string result = builder.BuildUrlStringForPath ("~/media/Images/SoMe ImAGe", null);
			string expected = "http://test.host/~/media/Images/SoMe%20ImAGe.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void EmptyDownloadOptionsTest ()
		{
			DownloadMediaOptions options = new DownloadMediaOptions ();

			string result = builder.BuildUrlStringForPath ("~/media/1.png", options);
			string expected = "http://test.host/~/media/1.png.ashx";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void CorrectDownloadOptionsTest ()
		{
			DownloadMediaOptions options = new DownloadMediaOptions ();
			options.SetWidth (100);
			options.SetBackgroundColor ("white");
			options.SetDisplayAsThumbnail (true);

			string result = builder.BuildUrlStringForPath ("~/media/1.png", options);
			string expected = "http://test.host/~/media/1.png.ashx?w=100&bc=white&thn=1&db=web&la=en";

			Assert.AreEqual(expected, result);
		}

		[Test ()]
		public void CorrectDownloadOptionsWithNullValuesTest ()
		{
			DownloadMediaOptions options = new DownloadMediaOptions ();
			options.SetWidth (100);
			options.SetBackgroundColor ("white");
			options.SetBackgroundColor (null);

			options.SetDisplayAsThumbnail (true);
			options.SetDisplayAsThumbnail (false);

			string result = builder.BuildUrlStringForPath ("~/media/1.png", options);
			string expected = "http://test.host/~/media/1.png.ashx?w=100&db=web&la=en";

			Assert.AreEqual(expected, result);
		}
	}
}

