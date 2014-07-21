
namespace Sitecore.MobileSDK.Validators
{
  using System;

  public class ItemPathValidator
  {
    private ItemPathValidator ()
    {
    }

    public static void ValidateItemPath(string itemPath)
    {
      if ( string.IsNullOrWhiteSpace(itemPath) )
      {
        throw new ArgumentException("Item path cannot be null or empty");
      }
      else if (!itemPath.StartsWith("/"))
      {
        throw new ArgumentException("Item path should begin with '/'");
      }
    }
  }
}

