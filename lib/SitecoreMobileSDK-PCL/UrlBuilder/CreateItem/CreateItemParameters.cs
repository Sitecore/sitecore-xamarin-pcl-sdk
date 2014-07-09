
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using System.Collections.Generic;

  public class CreateItemParameters
  {
    public CreateItemParameters (string itemName, string itemTemplate, Dictionary<string, string> fieldsRawValuesByName)
    {
      this.ItemName = itemName;
      this.ItemTemplate = itemTemplate;
      this.FieldsRawValuesByName = fieldsRawValuesByName;
    }

    public string ItemName{ get; private set; }
    public string ItemTemplate{ get; private set; }
    public Dictionary<string, string> FieldsRawValuesByName{ get; private set; }
  }
}

