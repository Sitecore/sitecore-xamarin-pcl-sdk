namespace Sitecore.MobileSDK.Validators
{
  internal class WebApiParameterValidator
  {
    private WebApiParameterValidator()
    {
    }

    public static void ValidateWriteOnceDestinationWithErrorMessage(object writeOnceDestination, string source)
    {
      if (null != writeOnceDestination)
      {
        BaseValidator.ThrowParameterSetTwiceException(source);
      }
    }

    public static void ValidateParameterAndThrowErrorWithMessage(string parameterValue, string source)
    {
      if (!IsParameterValid(parameterValue))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(source);
      }
    }

    public static bool IsParameterValid(string parameterValue)
    {
      return !string.IsNullOrWhiteSpace(parameterValue);
    }
  }
}

