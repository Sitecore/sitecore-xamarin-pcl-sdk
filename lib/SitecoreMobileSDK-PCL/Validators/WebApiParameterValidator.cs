namespace Sitecore.MobileSDK.Validators
{
  using System;

  internal class WebApiParameterValidator
  {
    private WebApiParameterValidator()
    {
    }

    public static void ValidateWriteOnceDestinationWithErrorMessage(object writeOnceDestination, string errorMessage)
    {
      if (null != writeOnceDestination)
      {
        throw new InvalidOperationException(errorMessage);
      }
    }

    public static void ValidateParameterAndThrowErrorWithMessage(string parameterValue, string errorMessage)
    {
      if (!WebApiParameterValidator.IsParameterValid(parameterValue))
      {
        throw new ArgumentException(errorMessage);
      }
    }

    public static bool IsParameterValid(string parameterValue)
    {
      return !string.IsNullOrWhiteSpace(parameterValue);
    }
  }
}

