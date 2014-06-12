

namespace Sitecore.MobileSDK.Items
{
	using System.Collections.Generic;
    using Sitecore.MobileSDK.Items.Fields;


    public class ScItem : ISitecoreItem
	{
		#region Class variables;

		public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

        public IItemSource Source { get; private set; }

		public string DisplayName { get; private set; }

		public bool HasChildren { get; private set; }

		public string Id { get; private set; }

		public string LongId { get; private set; }

		public string Path { get; private set; }

		public string Template { get; private set; }

        public IList<IField> Fields { get; private set; }

		#endregion Class variables;

		private ScItem ()
		{
		}

        public ScItem (IItemSource source, string displayName, bool hasChildren, string id, string longId, string path, string template, IList<IField> fields)
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
