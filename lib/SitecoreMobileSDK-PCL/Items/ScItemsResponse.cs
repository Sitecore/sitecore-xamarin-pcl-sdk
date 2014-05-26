namespace Sitecore.MobileSDK.Items
{
	using System.Collections.Generic;

	public class ScItemsResponse
	{
		public int TotalCount { get; set; }

		public int ResultCount { get; set; }

		public List<ScItem> Items { get; set; }

		public ScItemsResponse (int totalCount, int resultCount, List<ScItem> items)
		{
			this.TotalCount = totalCount;
			this.ResultCount = resultCount;
			this.Items = items;
		}
	}
}
