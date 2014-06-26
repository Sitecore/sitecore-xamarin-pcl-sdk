using System;

namespace Sitecore.MobileSDK
{
	public class MediaItemPathValidator
	{
		public MediaItemPathValidator ()
		{
		}

		public static void ValidateMediaItemPath(string itemPath)
		{
			if ( string.IsNullOrWhiteSpace(itemPath) )
			{
				throw new ArgumentNullException ("Media Item path cannot be null or empty");
			}
			else if (!(itemPath.StartsWith("/") || itemPath.StartsWith("~")))
			{
				throw new ArgumentException("Media Item path should begin with '/' or '~'");
			}
		}
	}
}

