using System;

namespace Sitecore.MobileSDK
{
	public class ItemsSource
	{
		public string Database { get; private set; }

		public string Language { get; private set; }

		public int Version { get; private set; }

		public ItemsSource (string database, string language, int version)
		{
			this.Database = database;
			this.Language = language;
			this.Version = version;
		}
	}
}

