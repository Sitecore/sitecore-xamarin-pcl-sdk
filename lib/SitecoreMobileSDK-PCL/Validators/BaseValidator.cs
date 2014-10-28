﻿namespace Sitecore.MobileSDK.Validators
{
  using System;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class BaseValidator
  {
    public static void CheckNullAndThrow(Object obj, string source)
    {
      if (obj == null)
      {
        throw new ArgumentNullException(source);
      }
    }

    public static void CheckForTwiceSetAndThrow(Object obj, string source)
    {
      if (obj != null)
      {
        throw new InvalidOperationException(source + " : " + "Property cannot be assigned twice.");
      }
    }

    public static void AssertPositiveNumber(int? optionalNumber, string source)
    {
      BaseValidator.CheckNullAndThrow(optionalNumber, source);
      int number = optionalNumber.Value;

      if (number <= 0)
      {
        throw new ArgumentException(source + " : " + "Positive number expected");
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

    public static void CheckMediaOptionsOrThrow(IDownloadMediaOptions options, string source)
    {
      if (!MediaOptionsValidator.IsValidMediaOptions(options))
      {
        throw new ArgumentException(source + " : is not valid");
      }
    }
  }
}
