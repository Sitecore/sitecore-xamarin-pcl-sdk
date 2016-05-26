namespace Sitecore.MobileSDK.API.Exceptions
{
  using System;

  /// <summary>
  /// The exception that is thrown when error response is returned by the server.
  /// </summary>
  public class SSCJsonErrorException : SitecoreMobileSdkException
  {
    /// <summary>
    /// Gets a <see cref="SSCJsonStatusMessage"/> that describes the current exception.
    /// </summary>
    public SSCJsonStatusMessage Response { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SSCJsonErrorException"/> class.
    /// </summary>
    /// <param name="response">The status message that contains the reason of the server error response.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public SSCJsonErrorException(SSCJsonStatusMessage response, Exception inner = null)
      : base(response.Message, inner)
    {
      this.Response = response;
    }
  }
}