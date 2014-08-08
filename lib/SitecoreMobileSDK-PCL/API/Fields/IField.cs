namespace Sitecore.MobileSDK.API.Fields
{
  public interface IField
  {
    string FieldId { get; }
    string Name { get; }
    string Type { get; }
    string RawValue { get; }
  }
}

