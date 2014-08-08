namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System.Collections.Generic;

  public class CreateItemParameters
  {
    public CreateItemParameters(string itemName, string itemTemplate, IDictionary<string, string> fieldsRawValuesByName)
    {
      this.ItemName = itemName;
      this.ItemTemplate = itemTemplate;
      this.FieldsRawValuesByName = fieldsRawValuesByName;
    }

    public CreateItemParameters ShallowCopy()
    {
      return new CreateItemParameters(this.ItemName, this.ItemTemplate, this.FieldsRawValuesByName);
    }

    public string ItemName { get; private set; }
    public string ItemTemplate { get; private set; }
    public IDictionary<string, string> FieldsRawValuesByName { get; private set; }
  }
}

