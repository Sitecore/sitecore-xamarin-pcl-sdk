namespace Sitecore.MobileSDK.Items
{
	using System.Collections.Generic;
	using Sitecore.MobileSDK.Items.Fields;

	public class ScItem
	{
		#region Class variables;

		public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

		public ItemSource Source { get; private set; }

		public string DisplayName { get; private set; }

		public bool HasChildren { get; private set; }

		public string Id { get; private set; }

		public string LongId { get; private set; }

		public string Path { get; private set; }

		public string Template { get; private set; }

		public List<IField> Fields { get; private set; }

		#endregion Class variables;

		private ScItem ()
		{
		}

		public ScItem (ItemSource source, string displayName, bool hasChildren, string id, string longId, string path, string template, List<IField> fields)
		{
			this.Source = source;
			this.DisplayName = displayName;
			this.HasChildren = hasChildren;
			this.Id = id;
			this.LongId = longId;
			this.Path = path;
			this.Template = template;
			this.Fields = fields;
		}
	}
}
