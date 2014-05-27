using System;
using Sitecore.MobileSDK.Items;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Sitecore.MobileSDK
{
	public class ScItemsParser
	{
		private ScItemsParser ()
		{
		}

		public static ScItemsResponse Parse (string response)
		{
			if (string.IsNullOrEmpty (response))
			{
				throw new ArgumentException ("response cannot null or empty");
			}

			JObject jsonResponse = JObject.Parse (response);

			int totalCount = ParseOrFail<int> (jsonResponse, "$.result.totalCount");
			int resultCount = ParseOrFail<int> (jsonResponse, "$.result.resultCount");

			var responseItems = jsonResponse.SelectToken ("$.result.items");
			var items = new List<ScItem> ();

			foreach (JObject singleItem in responseItems)
			{
				ScItem resultItem = new ScItem ();

				resultItem.DisplayName = (string)singleItem.GetValue ("DisplayName");
				resultItem.Database = (string)singleItem.GetValue ("Database");
				resultItem.HasChildren = (bool)singleItem.GetValue ("HasChildren");
				resultItem.Id = (string)singleItem.GetValue ("ID");
				resultItem.Language = (string)singleItem.GetValue ("Language");
				resultItem.LongId = (string)singleItem.GetValue ("LongID");
				resultItem.Path = (string)singleItem.GetValue ("Path");
				resultItem.Template = (string)singleItem.GetValue ("Template");
				resultItem.Version = (int)singleItem.GetValue ("Version");

				items.Add (resultItem);
			}

			return new ScItemsResponse (totalCount, resultCount, items);
		}

		private static T ParseOrFail<T>(JObject json, string path)
		{

			JToken objectToken = json.SelectToken (path);
			if (null == objectToken)
			{
				throw new JsonException ("Invalid json");
			}

			return objectToken.Value<T> ();
		}
	}
}

