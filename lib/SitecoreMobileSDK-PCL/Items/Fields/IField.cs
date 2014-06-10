using System;

namespace Sitecore.MobileSDK
{
	public interface IField
	{
		string FieldId 	{ get; }
		string Name 	{ get; }
		string Type 	{ get; }
		string RawValue { get; }

		//TODO: @igk ???
		//ScApiSession session { get; }
		//ItemSource Source { get; }
		//async Task ReadFieldValue

	}
}

