using NUnit.Framework;
using System;
using Sitecore.MobileSDK.SessionSettings;


namespace Sitecore.MobileSdkUnitTest
{
	using Sitecore.MobileSDK.MediaItems;
	using Sitecore.MobileSDK.UrlBuilder.Rest;

	[TestFixture ()]
	public class ResourseUrlBuilderTest
	{
		ResourceUrlBuilder builder;

		[SetUp]
		public void SetUp()
		{
			IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();

			SessionConfigPOD sessionConfig = new SessionConfigPOD();
			sessionConfig.ItemWebApiVersion = "v1";
			sessionConfig.InstanceUrl = "http://test.host";
			sessionConfig.Site = null;

			builder = new ResourceUrlBuilder (restGrammar, sessionConfig, null);
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
	}
}

