using System;

namespace Sitecore.MobileSDK
{
	public class ItemSource
	{
		public string Database { get; private set; }

		public string Language { get; private set; }

		public int Version { get; private set; }

		public ItemSource (string database, string language, int version)
		{
			this.Database = database;
			this.Language = language;
			this.Version = version;
		}
	}
}

