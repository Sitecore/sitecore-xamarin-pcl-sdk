namespace Sitecore.MobileSDK.API.Request.Parameters
{
  /// <summary>
  /// Enum represents scope parameter that specifies the set of items that you are working with.
  /// </summary>
  public enum ScopeType
  {
    /// <summary>
    /// self - for self.    /// </summary>
    Self,

    /// <summary>
    /// children - for children.
    /// </summary>
    Children,
    
    /// <summary>
    /// parent - for parent.
    /// </summary>
    Parent,

    /// <summary>
    /// The default is <see cref="Self"/>
    /// </summary>
    Default = Self
  }
}

