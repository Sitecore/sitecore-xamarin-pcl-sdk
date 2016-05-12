namespace Sitecore.MobileSDK.Items
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;
  using Sitecore.MobileSDK.API.Items;

  public class ScItem : ISitecoreItem
  {
    private const string DisplayNameKey = "DisplayName";
    private const string HasChildrenKey = "HasChildren";
    private const string ItemIDKey      = "ItemID";
    private const string ItemPathKey    = "ItemPath";
    private const string TemplateIDKey  = "TemplateID";

    #region Class variables;

    public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

    public IItemSource Source { get; private set; }

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

    #region Standard Fields;

    public string DisplayName { 
      get
      { 
        return this.FieldWithName(DisplayNameKey).RawValue;
      }
    }

    public bool HasChildren { 
      get
      { 
        return  Convert.ToBoolean(this.FieldWithName(HasChildrenKey).RawValue);
      }
    }

    public string Id { 
      get
      { 
        return this.FieldWithName(ItemIDKey).RawValue;
      }
    }

    public string Path { 
      get
      { 
        return this.FieldWithName(ItemPathKey).RawValue;
      }
    }

    public string TemplateId { 
      get
      { 
        return this.FieldWithName(TemplateIDKey).RawValue;
      }
    }

    #endregion Standard Fields;

    private ScItem()
    {
    }

    public ScItem(
    IItemSource source,
    Dictionary<string, IField> fieldsByName)
    {
      this.Source = source;
      this.FieldsByName = fieldsByName;

      int fieldsCount = fieldsByName.Count;
      IField[] fields = new IField[fieldsCount];
      fieldsByName.Values.CopyTo(fields, 0);
      this.Fields = fields;
    }
  }
}
