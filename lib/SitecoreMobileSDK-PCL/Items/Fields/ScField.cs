using System;

namespace Sitecore.MobileSDK.Items.Fields
{
	public class ScField : IField
	{
		public string FieldId 	{ get; private set; }
		public string Name 		{ get; private set; }
		public string Type 		{ get; private set; }
		public string RawValue 	{ get; private set; }

		public ScField (string fieldId, string name, string type, string rawValue)
		{
			this.FieldId = fieldId;
			this.Name = name;
			this.Type = type;
			this.RawValue = rawValue;
		}
	}
}

