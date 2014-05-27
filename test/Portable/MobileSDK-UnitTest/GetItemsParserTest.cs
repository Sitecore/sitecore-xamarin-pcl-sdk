using NUnit.Framework;
using System;
using Sitecore.MobileSDK;
using System.Net.Http;
using Sitecore.MobileSDK.Items;
using Newtonsoft.Json;

namespace MobileSDKTestAndroid
{
	[TestFixture]
	public class GetItemsParserTest
	{
		[SetUp]
		public void Setup ()
		{
		}

		[TearDown]
		public void TearDown ()
		{
		}

		[Test]
		public void TestParseValidResponse ()
		{
			string rawResponse = "{\"statusCode\":200,\"result\":{\"totalCount\":1,\"resultCount\":1,\"items\":[{\"Category\":\"Content\",\"Database\":\"web\",\"DisplayName\":\"Home\",\"HasChildren\":true,\"Icon\":\"/temp/IconCache/Network/16x16/home.png\",\"ID\":\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\",\"Language\":\"en\",\"LongID\":\"/{11111111-1111-1111-1111-111111111111}/{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}/{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\",\"MediaUrl\":\"/~/icon/Network/48x48/home.png.aspx\",\"Name\":\"Home\",\"Path\":\"/sitecore/content/Home\",\"Template\":\"Sample/Sample Item\",\"TemplateId\":\"{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}\",\"TemplateName\":\"Sample Item\",\"Url\":\"~/link.aspx?_id=110D559FDEA542EA9C1C8A5DF7E70EF9\\u0026amp;_z=z\",\"Version\":1,\"Fields\":{\"{75577384-3C97-45DA-A847-81B00500E250}\":{\"Name\":\"Title\",\"Type\":\"text\",\"Value\":\"Sitecore master\"},\"{A60ACD61-A6DB-4182-8329-C957982CEC74}\":{\"Name\":\"Text\",\"Type\":\"Rich Text\",\"Value\":\"\\u003cdiv\\u003eWelcome to Sitecore!\\u003c/div\\u003e\\n\\u003cdiv\\u003e\\u003cbr /\\u003e\\n\\u003c/div\\u003e\\n\\u003ca href=\\\"~/link.aspx?_id=A2EE64D5BD7A4567A27E708440CAA9CD\\u0026amp;_z=z\\\"\\u003eAccelerometer\\u003c/a\\u003e\"}}}]}}";
			ScItemsResponse response = ScItemsParser.Parse (rawResponse);
			Assert.AreEqual (1, response.Items.Count);

			ScItem item1 = response.Items [0];

			Assert.AreEqual ("Home", item1.DisplayName);
			Assert.AreEqual ("web", item1.Database);
			Assert.AreEqual (true, item1.HasChildren);
			Assert.AreEqual ("en", item1.Language);
			Assert.AreEqual ("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}", item1.Id);
			Assert.AreEqual ("/{11111111-1111-1111-1111-111111111111}/{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}/{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}", item1.LongId);
			Assert.AreEqual ("/sitecore/content/Home", item1.Path);
			Assert.AreEqual ("Sample/Sample Item", item1.Template);
			Assert.AreEqual (1, item1.Version);
		}

		[Test]
		public void TestParseEmptyResponse ()
		{
			TestDelegate action = () => ScItemsParser.Parse ("");
			Assert.Throws<ArgumentException> (action, "cannot parse empty response");
		}

		[Test]
		public void TestParseNullResponse ()
		{
			TestDelegate action = () => ScItemsParser.Parse (null);
			Assert.Throws<ArgumentException> (action, "cannot parse null response");
		}

		[Test]
		public void TestParseResponseWithEmptyItems ()
		{
			string rawResponse = "{\n  \"statusCode\": 200,\n  \"result\": {\n    \"totalCount\": 0,\n    \"resultCount\": 0,\n    \"items\": []\n  }\n}";
			ScItemsResponse response = ScItemsParser.Parse (rawResponse);

			Assert.AreEqual (0, response.TotalCount);
			Assert.AreEqual (0, response.ResultCount);
			Assert.AreEqual (0, response.Items.Count);
		}

		[Test]
		public void TestParseInvalidResponse ()
		{
			string rawResponse = "{\n  \"statusCode\": 200,\n  \"result\": {\n    \"Invalidtotaldount\": 0,\n    \"InvalidresultCount\": 0,\n    \"items\": []\n  }\n}";
			TestDelegate action = () => ScItemsParser.Parse (rawResponse);
			Assert.Throws<ArgumentException> (action, "JsonException should be here");
		}
	}
}

