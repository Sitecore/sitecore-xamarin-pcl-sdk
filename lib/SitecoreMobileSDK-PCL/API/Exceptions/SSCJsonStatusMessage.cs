namespace Sitecore.MobileSDK.API.Exceptions
{
  /// <summary>
  /// Class that represents Sitecore CMS server error response.
  /// 
  /// <example>
  ///   {"statusCode":401,"error":{"message":"Access to site is not granted."}}
  /// </example>
  /// 
  /// </summary>
  public class SSCJsonStatusMessage
  {
    /// <summary>
    ///  Gets status code of response.
    /// </summary>
    public int StatusCode { get; private set; }

    /// <summary>
    ///  Gets error message of response.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="SSCJsonStatusMessage"/> class.
    /// </summary>
    /// <param name="statusCode">status code of the response</param>
    /// <param name="message">error message of the response</param>
    public SSCJsonStatusMessage(int statusCode, string message)
    {
      this.StatusCode = statusCode;
      this.Message = message;
    }
  }
}