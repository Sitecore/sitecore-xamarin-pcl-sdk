namespace Sitecore.MobileSDK.Items.Fields
{
  using System;
  using System.Threading;
  using System.Collections.Generic;
  using System.Linq;

  using Newtonsoft.Json.Linq;
  using Sitecore.MobileSDK.API.Fields;

  public class ScFieldsParser
  {
    public ScFieldsParser()
    {
    }

    public static List<IField> ParseFieldsData(JObject fieldsData, CancellationToken cancelToken)
    { 
      if (fieldsData == null)
      {
        throw new ArgumentNullException();
      }

      var fields = new List<IField>();

      foreach (var field in fieldsData)
      {
        string key = field.Key;
        string value = field.Value.ToString();

        ScField newField = new ScField(key, value);
        fields.Add(newField);
      }

      return fields;
    }
  }
}

