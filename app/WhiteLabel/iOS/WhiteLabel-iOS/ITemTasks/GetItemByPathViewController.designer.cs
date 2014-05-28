// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("GetItemByPathViewController")]
	partial class GetItemByPathViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField itemPathField { get; set; }

		[Action ("getItem:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void getItem (MonoTouch.UIKit.UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (itemPathField != null) {
				itemPathField.Dispose ();
				itemPathField = null;
			}
		}
	}
}
