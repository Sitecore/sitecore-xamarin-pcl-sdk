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
	[Register ("GetItemByIdViewController")]
	partial class GetItemByIdViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField itemIdTextField { get; set; }

		[Action ("getItem:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void getItem (MonoTouch.UIKit.UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (itemIdTextField != null) {
				itemIdTextField.Dispose ();
				itemIdTextField = null;
			}
		}
	}
}
