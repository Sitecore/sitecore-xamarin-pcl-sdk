namespace Sitecore.MobileSDK.Items
{
    using System.Collections.Generic;

    public class ScItem
    {
        #region Class verialbles;
        public const string RootItemId = "{11111111-1111-1111-1111-111111111111}";

		public string Database { get; set; }

		public string DisplayName { get; set; }

		public bool HasChildren { get; set; }

		public string Id { get; set; }

		public string Language { get; set; }

		public string LongId { get; set; }

		public string Path { get; set; }

		public string Template { get; set; }

		public int Version { get; set; }

//		public List<ScField> mFields;

        #endregion Class verialbles;
    }
}
