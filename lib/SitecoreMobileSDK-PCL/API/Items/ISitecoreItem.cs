namespace Sitecore.MobileSDK.API.Items
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;

  public interface ISitecoreItem
  {
    IItemSource Source { get; }

    string DisplayName { get; }

    bool HasChildren { get; }

    string Id { get; }

    string LongId { get; }

    string Path { get; }

    string Template { get; }

    int FieldsCount { get; }
    IField this[string index] { get; }

    IEnumerable<IField> Fields{ get; }

    IField FieldWithName(string caseInsensitiveFieldName);
  }
}

