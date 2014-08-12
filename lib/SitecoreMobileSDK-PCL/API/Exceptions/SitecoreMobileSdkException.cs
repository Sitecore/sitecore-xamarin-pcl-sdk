namespace Sitecore.MobileSDK.API.Exceptions
{
  using System;

  /// <summary>
  /// Base exception that represents errors that occur during Sitecore Mobile SDK execution.
  /// </summary>
  public class SitecoreMobileSdkException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SitecoreMobileSdkException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public SitecoreMobileSdkException(string message, Exception inner = null)
      : base(message, inner)
    {
    }
  }
}

