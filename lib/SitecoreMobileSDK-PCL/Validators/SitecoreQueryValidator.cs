﻿namespace Sitecore.MobileSDK.Validators
{
  using System;

  public class SitecoreQueryValidator
  {
    private SitecoreQueryValidator ()
    {
    }

    public static void ValidateSitecoreQuery(string sitecoreQuery)
    {
      if ( string.IsNullOrWhiteSpace(sitecoreQuery) )
      {
        throw new ArgumentNullException ("SitecoreQuery cannot be null");
      }
    }
  }
}

