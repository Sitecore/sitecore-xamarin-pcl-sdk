namespace Sitecore.MobileSDK.Validators
{
  using System;

  public class BaseValidator
  {
    public static void ThrowNullOrEmptyParameterException(string source)
    {
      throw new ArgumentException(source + " : " + "The input cannot be null or empty.");
    }

    public static void ThrowParameterSetTwiceException(string source)
    {
      throw new InvalidOperationException(source + " : " + "Property cannot be assigned twice.");
    }
  }
}
