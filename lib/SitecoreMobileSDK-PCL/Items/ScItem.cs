namespace Sitecore.MobileSDK.Items
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;
  using Sitecore.MobileSDK.API.Items;

  public class ScItem : ISitecoreItem
  {
    #region Class variables;

    public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

    public IItemSource Source { get; private set; }

    public string DisplayName { get; private set; }

    public bool HasChildren { get; private set; }

    public string Id { get; private set; }

    public string LongId { get; private set; }

    public string Path { get; private set; }

    public string Template { get; private set; }

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
    IItemSource source,
    string displayName,
    bool hasChildren,
    string id,
    string longId,
    string path,
    string template,
    Dictionary<string, IField> fieldsByName)
    {
      this.Source = source;
      this.DisplayName = displayName;
      this.HasChildren = hasChildren;
      this.Id = id;
      this.LongId = longId;
      this.Path = path;
      this.Template = template;
      this.FieldsByName = fieldsByName;

      int fieldsCount = fieldsByName.Count;
      IField[] fields = new IField[fieldsCount];
      fieldsByName.Values.CopyTo(fields, 0);
      this.Fields = fields;
    }
  }
}
