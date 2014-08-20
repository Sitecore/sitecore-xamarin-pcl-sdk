namespace Sitecore.MobileSDK.API.Exceptions
{
  using System;

  /// <summary>
  /// The exception that is thrown when TCP or HTTP protocol physical connection errors happen.
  /// </summary>
  public class LoadDataFromNetworkException : SitecoreMobileSdkException
  {
    /// <summary>
    /// Initializes a new instance of the <seealso cref="LoadDataFromNetworkException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for this exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public LoadDataFromNetworkException(string message, Exception inner = null)
      : base(message, inner)
    {
    }
  }
}

