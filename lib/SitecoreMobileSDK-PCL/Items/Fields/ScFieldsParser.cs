using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sitecore.MobileSDK
{
	public class ScFieldsParser
	{
		public ScFieldsParser ()
		{
		}

		public static List<IField> ParseFieldsData(JObject fieldsData)
		{
			var fields = new List<IField>();

			IList<string> propertyNames = fieldsData.Properties().Select(p => p.Name).ToList();


			foreach (string fieldId in propertyNames)
			{
				//TODP: @igk make canceling
				JObject fieldData = (JObject)fieldsData.GetValue(fieldId);
				var name = (string)fieldData.GetValue("Name");
				var type = (string)fieldData.GetValue("Type");
				var value = (string)fieldData.GetValue("Value");

				ScField newField = new ScField (fieldId, name, type, value);
				fields.Add(newField);
			}

			return fields;
		}
	}
}

