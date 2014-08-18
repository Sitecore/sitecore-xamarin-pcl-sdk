namespace Sitecore.MobileSDK.API.Fields
{
  /// <summary>
  /// The IField interface represents a Sitecore item’s field.
  /// It provides getters for the field’s properties.
  /// </summary>
  public interface IField
  {
    /// <summary>
    /// Returns field's GUID.
    /// </summary>
    string FieldId { get; }

    /// <summary>
    /// Returns field's name.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Returns field's type.
    /// </summary>
    string Type { get; }
    
    /// <summary>
    /// Returns field's raw value.
    /// </summary>
    string RawValue { get; }
  }
}

