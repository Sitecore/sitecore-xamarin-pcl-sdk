namespace Sitecore.MobileSDK.Validators
{
  using System;

	public class MediaPathValidator
	{
		public MediaPathValidator ()
		{
		}

		public static void ValidateMediaPath(string itemPath)
		{
			if ( string.IsNullOrWhiteSpace(itemPath) )
			{
        throw new ArgumentException("Media path cannot be null or empty");
			}
			else if (!(itemPath.StartsWith("/") || itemPath.StartsWith("~")))
			{
				throw new ArgumentException("Media path should begin with '/' or '~'");
			}
		}
	}
}

