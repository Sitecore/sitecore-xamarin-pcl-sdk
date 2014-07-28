namespace Sitecore.MobileSDK.Validators
{
  using System;

  public class BaseValidator
  {
    public const string NULL_OR_EMPTY_VARIABLE_MESSAGE_TEMPLATE = "{0} : The input cannot be null or empty.";
    public const string VARIABLE_SET_TWICE_MESSAGE_TEMPLATE = "{0} : Property cannot be assigned twice.";

    public static void ThrowNullOrEmptyParameterException(string source)
    {
      throw new ArgumentException(string.Format(NULL_OR_EMPTY_VARIABLE_MESSAGE_TEMPLATE, source));
    }

    public static void ThrowParameterSetTwiceException(string source)
    {
      throw new InvalidOperationException(string.Format(NULL_OR_EMPTY_VARIABLE_MESSAGE_TEMPLATE, source));
    }
  }
}
