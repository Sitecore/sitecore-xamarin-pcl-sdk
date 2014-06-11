﻿using NUnit.Framework;


namespace Sitecore.MobileSdkUnitTest
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.Fields;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	[TestFixture ()]
	public class FieldsParserTest
	{
		[Test]
		public void TestParseNullData ()
		{
			JObject fieldsData = null;
			TestDelegate action = () =>  ScFieldsParser.ParseFieldsData (fieldsData, CancellationToken.None);;
			Assert.Throws<ArgumentException>(action, "cannot parse null response");
		}

		[Test]
		public void TestParseEmptyData ()
		{
			JObject fieldsData = JObject.Parse("{}");
			List<IField> fields = ScFieldsParser.ParseFieldsData (fieldsData, CancellationToken.None);;
			Assert.True (fields.Count == 0);
		}

		[Test]
		public void TestParseCorrectData ()
		{
			JObject fieldsData = JObject.Parse("{\n\"{75577384-3C97-45DA-A847-81B00500E250}\":{\n\"Name\":\"Title\",\n\"Type\":\"text\",\n\"Value\":\"RichText\"\n},\n\"{A60ACD61-A6DB-4182-8329-C957982CEC74}\":{\n\"Name\":\"Text\",\n\"Type\":\"Rich Text\",\n\"Value\":\"\\u003cp\\u003eWelcome to Sitecore\\u003c/p\\u003e\\r\\n\\r\\n\\u003cp\\u003e This is an image with full media path \\\"http://mobiledev1ua1.dk.sitecore.net:89/~/media/4F20B519D5654472B01891CB6103C667.ashx\\\" \\u003cbr\\u003e\\r\\n\\r\\n\\u003c/p\\u003e\\r\\n\\r\\n\\u003cp\\u003eAnd this one has a short media path \\\"~/media/4F20B519D5654472B01891CB6103C667.ashx\\\" \\u003cbr\\u003e\\r\\n\\r\\n\\u003c/p\\u003e\\r\\n\\r\\n\\u003cp\\u003eThey must look the same\\u003c/p\\u003e\"\n}\n}");;
			List<IField> fields = ScFieldsParser.ParseFieldsData (fieldsData, CancellationToken.None);;
			Assert.True (fields.Count == 2);
			ScField field1 = (ScField)fields[0];
			Assert.AreEqual("{75577384-3C97-45DA-A847-81B00500E250}", field1.FieldId);
			ScField field2 = (ScField)fields[1];
			Assert.AreEqual(field2.FieldId, "{A60ACD61-A6DB-4182-8329-C957982CEC74}");
		}

		public void TestParseBrokenData ()
		{
			JObject fieldsData = JObject.Parse ("{}");
			List<IField> fields = ScFieldsParser.ParseFieldsData (fieldsData, CancellationToken.None);;
			Assert.True (fields.Count == 2);
			ScField field1 = (ScField)fields[0];
			Assert.AreEqual(field1.FieldId, "{75577384-3C97-45DA-A847-81B00500E250}");
			ScField field2 = (ScField)fields[1];
			Assert.AreEqual(field2.FieldId, "{A60ACD61-A6DB-4182-8329-C957982CEC74}");
		}

		[Test]
		[ExpectedException(typeof(OperationCanceledException))]
		public async void TestCancellationCausesOperationCanceledException()
		{
			var cancel = new CancellationTokenSource ();

			Task<List<IField>> action = Task.Factory.StartNew( ()=> 
			{
				int millisecondTimeout = 10;
				Thread.Sleep(millisecondTimeout);    
				JObject fieldsData = JObject.Parse("{\n\"{75577384-3C97-45DA-A847-81B00500E250}\":{\n\"Name\":\"Title\",\n\"Type\":\"text\",\n\"Value\":\"RichText\"\n},\n\"{A60ACD61-A6DB-4182-8329-C957982CEC74}\":{\n\"Name\":\"Text\",\n\"Type\":\"Rich Text\",\n\"Value\":\"\\u003cp\\u003eWelcome to Sitecore\\u003c/p\\u003e\\r\\n\\r\\n\\u003cp\\u003e This is an image with full media path \\\"http://mobiledev1ua1.dk.sitecore.net:89/~/media/4F20B519D5654472B01891CB6103C667.ashx\\\" \\u003cbr\\u003e\\r\\n\\r\\n\\u003c/p\\u003e\\r\\n\\r\\n\\u003cp\\u003eAnd this one has a short media path \\\"~/media/4F20B519D5654472B01891CB6103C667.ashx\\\" \\u003cbr\\u003e\\r\\n\\r\\n\\u003c/p\\u003e\\r\\n\\r\\n\\u003cp\\u003eThey must look the same\\u003c/p\\u003e\"\n}\n}");;
				return ScFieldsParser.ParseFieldsData(fieldsData, cancel.Token);
			});
			cancel.Cancel();
			await action;

			Assert.Fail ("OperationCanceledException not thrown");
		}
	}
}

