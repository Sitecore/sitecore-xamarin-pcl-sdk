using System;

namespace Sitecore.MobileSDK
{
	public class MediaPathValidator
	{
		public MediaPathValidator ()
		{
		}

		public static void ValidateMediaPath(string itemPath)
		{
			if ( string.IsNullOrWhiteSpace(itemPath) )
			{
				throw new ArgumentNullException ("Media path cannot be null or empty");
			}
			else if (!(itemPath.StartsWith("/") || itemPath.StartsWith("~")))
			{
				throw new ArgumentException("Media path should begin with '/' or '~'");
			}
		}
	}
}

