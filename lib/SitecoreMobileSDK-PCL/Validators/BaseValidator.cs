namespace Sitecore.MobileSDK.Validators
{
  using System;

  public class BaseValidator
  {

    public static void CheckNullAndThrow(Object obj, string source)
    {
      if (obj == null)
      {
        throw new ArgumentNullException(source + " : " + "The input cannot be null.");
      }
    }

    public static void CheckForTwiceSetAndThrow(Object obj, string source)
    {
      if (obj != null)
      {
        throw new ArgumentNullException(source + " : " + "Property cannot be assigned twice.");
      }
    }

    public static void CheckForNullAndEmptyOrThrow(string str, string source)
    {
      CheckNullAndThrow(str, source);

      if (string.IsNullOrEmpty(str))
      {
        throw new ArgumentException(source + " : " + "The input cannot be empty.");
      }
    }

    public static void CheckForNullEmptyAndWhiteSpaceOrThrow(string str, string source)
    {
      CheckNullAndThrow(str, source);

      if (string.IsNullOrWhiteSpace(str))
      {
        throw new ArgumentException(source + " : " + "The input cannot be empty.");
      }
    }

    //    public static void ThrowNullOrEmptyParameterException(string source)
    //    {
    //      throw new ArgumentException(source + " : " + "The input cannot be null or empty.");
    //    }
    //
    //    public static void ThrowParameterSetTwiceException(string source)
    //    {
    //      throw new InvalidOperationException(source + " : " + "Property cannot be assigned twice.");
    //    }
  }
}
