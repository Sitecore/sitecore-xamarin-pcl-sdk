namespace Sitecore.MobileSDK.API.Exceptions
{
  public class WebApiJsonStatusMessage
  {
    public int StatusCode { get; private set; }

    public string Message { get; private set; }

    public WebApiJsonStatusMessage(int statusCode, string message)
    {
      this.StatusCode = statusCode;
      this.Message = message;
    }

    private WebApiJsonStatusMessage()
    {
    }
  }
}