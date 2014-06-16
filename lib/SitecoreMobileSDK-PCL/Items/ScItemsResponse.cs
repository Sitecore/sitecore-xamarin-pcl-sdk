namespace Sitecore.MobileSDK.Items
{
	using System.Collections.Generic;

	public class ScItemsResponse
	{
		public int TotalCount { get; private set; }

		public int ResultCount { get; private set; }

        public List<ISitecoreItem> Items { get; private set; }

        public ScItemsResponse (int totalCount, int resultCount, List<ISitecoreItem> items)
		{
			this.TotalCount = totalCount;
			this.ResultCount = resultCount;
			this.Items = items;
		}
	}
}