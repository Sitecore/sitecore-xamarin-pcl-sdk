namespace Sitecore.MobileSDK.API.Fields
{
  /// <summary>
  /// The IField interface represents a single field of the Sitecore Item.
  /// Fields have the content that will actually be presented to the user.
  /// 
  /// Fields are readonly and immutable. If you need to modify the content you should send a new request using the session.
  /// </summary>
  public interface IField
  {
    /// <summary>
    /// Returns field's name.
    ///
    /// Field's name is case insensitive.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Returns field's raw value.
    /// </summary>
    string RawValue { get; }
  }
}

