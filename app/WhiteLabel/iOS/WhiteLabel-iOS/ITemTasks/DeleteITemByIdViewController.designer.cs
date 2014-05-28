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
	[Register ("DeleteITemByIdViewController")]
	partial class DeleteITemByIdViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField itemIdField { get; set; }

		[Action ("deleteItem:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void deleteItem (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (itemIdField != null) {
				itemIdField.Dispose ();
				itemIdField = null;
			}
		}
	}
}
