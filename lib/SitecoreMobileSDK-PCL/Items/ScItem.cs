using Newtonsoft.Json;
using Sitecore.MobileSDK.Items.Fields;

namespace Sitecore.MobileSDK.Items
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;
  using Sitecore.MobileSDK.API.Items;

  public class ScItem : ISitecoreItem
  {
    #region Class variables;

    public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

    [JsonProperty]
    public IItemSource Source { get; private set; }
    [JsonProperty]
    public string DisplayName { get; private set; }
    [JsonProperty]
    public bool HasChildren { get; private set; }
    [JsonProperty]
    public string Id { get; private set; }
    [JsonProperty]
    public string LongId { get; private set; }
    [JsonProperty]
    public string Path { get; private set; }
    [JsonProperty]
    public string Template { get; private set; }
    [JsonIgnore]
    public IEnumerable<IField> Fields { get; private set; }

    public IField this[string caseInsensitiveFieldName] 
    { 
      get
      {
        return this.FieldWithName(caseInsensitiveFieldName);
      }
    }

    public int FieldsCount 
    { 
      get
      {
        return this.FieldsByName.Count;
      }
    }

    [JsonProperty]
    private Dictionary<string, IField> FieldsByName { get; set; }

    public IField FieldWithName(string caseInsensitiveFieldName)
    {
      string lowercaseName = caseInsensitiveFieldName.ToLowerInvariant();
      return this.FieldsByName[lowercaseName];
    }

    #endregion Class variables;

    private ScItem()
    {
    }
      
    public ScItem(
      IItemSource Source,
      string DisplayName,
      bool HasChildren,
      string Id,
      string LongId,
      string Path,
      string Template,
      Dictionary<string, IField> FieldsByName)
    {
      this.Source = Source;
      this.DisplayName = DisplayName;
      this.HasChildren = HasChildren;
      this.Id = Id;
      this.LongId = LongId;
      this.Path = Path;
      this.Template = Template;
      this.FieldsByName = FieldsByName;

      int fieldsCount = FieldsByName.Count;
      IField[] fields = new IField[fieldsCount];
      FieldsByName.Values.CopyTo(fields, 0);
      this.Fields = fields;
    }

    [JsonConstructor]
    public ScItem(
      ItemSource Source,
      string DisplayName,
      bool HasChildren,
      string Id,
      string LongId,
      string Path,
      string Template,
      Dictionary<string, ScField> FieldsByName)
    {
      this.Source = Source;
      this.DisplayName = DisplayName;
      this.HasChildren = HasChildren;
      this.Id = Id;
      this.LongId = LongId;
      this.Path = Path;
      this.Template = Template;

      this.FieldsByName = new Dictionary<string, IField>(); 
      foreach (KeyValuePair<string, ScField> keyValuePair in FieldsByName)
      {
        this.FieldsByName.Add(keyValuePair.Key, (IField)keyValuePair.Value);
      }

//      this.FieldsByName = FieldsByName.ToDictionary(item => item.Key, item => (IField)item.Value);

      int fieldsCount = FieldsByName.Count;
      IField[] fields = new IField[fieldsCount];
      this.FieldsByName.Values.CopyTo(fields, 0);
      this.Fields = fields;
    }
  }
}
