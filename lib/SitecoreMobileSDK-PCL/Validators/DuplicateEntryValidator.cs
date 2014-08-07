﻿namespace Sitecore.MobileSDK.Validators
{
  using System;
  using System.Collections.Generic;

  public class DuplicateEntryValidator
  {
    private DuplicateEntryValidator()
    {
    }

    public static bool IsDuplicatedFieldsInTheList(IEnumerable<string> fields)
    {
      if (null == fields)
      {
        return false;
      }

      var uniqueFields = new HashSet<string>();
      foreach (string singleField in fields)
      {
        bool isSingleFieldInvalid = String.IsNullOrWhiteSpace(singleField);
        if (isSingleFieldInvalid)
        {
          return true;
        }

        string lowercaseSingleField = singleField.ToLowerInvariant();
        bool isDuplicateFound = uniqueFields.Contains(lowercaseSingleField);

        if (isDuplicateFound)
        {
          return true;
        }

        uniqueFields.Add(lowercaseSingleField);
      }

      return false;
    }
  }
}

