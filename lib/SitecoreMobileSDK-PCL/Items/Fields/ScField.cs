using Newtonsoft.Json;

namespace Sitecore.MobileSDK.Items.Fields
{
  using Sitecore.MobileSDK.API.Fields;

  public class ScField : IField
  {
    [JsonProperty]
    public string FieldId { get; private set; }
    [JsonProperty]
    public string Name { get; private set; }
    [JsonProperty]
    public string Type { get; private set; }
    [JsonProperty]
    public string RawValue { get; private set; }

    [JsonConstructor]
    public ScField(string FieldId, string Name, string Type, string RawValue)
    {
      this.FieldId = FieldId;
      this.Name = Name;
      this.Type = Type;
      this.RawValue = RawValue;
    }
  }
}

