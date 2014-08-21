namespace Sitecore.MobileSDK.API.Request.Parameters
{
  /// <summary>
  /// Enum represents Sitecore Payload parameter.
  /// </summary>
  public enum PayloadType
  {
    /// <summary>
    /// no fields are returned in the service response.
    /// </summary>
    Min,

    /// <summary>
    /// only content fields are returned in the service response.
    /// </summary>
    Content,

    /// <summary>
    /// all the item fields, including content and standard fields, are returned in the service response.
    /// </summary>
    Full,

    /// <summary>
    /// The default is <seealso cref="Min"/>
    /// </summary>
    Default = Min
  }
}