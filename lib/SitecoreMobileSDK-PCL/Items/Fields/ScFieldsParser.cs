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

      IList<string> propertyNames = fieldsData.Properties().Select(p => p.Name).ToList();


      foreach (string fieldId in propertyNames)
      {
        cancelToken.ThrowIfCancellationRequested();

        JObject fieldData = (JObject)fieldsData.GetValue(fieldId);
        var name = (string)fieldData.GetValue("Name");
        var type = (string)fieldData.GetValue("Type");
        var value = (string)fieldData.GetValue("Value");

        ScField newField = new ScField(fieldId, name, type, value);
        fields.Add(newField);
      }

      return fields;
    }
  }
}

