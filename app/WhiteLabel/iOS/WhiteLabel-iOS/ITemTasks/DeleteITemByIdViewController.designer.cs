// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("DeleteITemByIdViewController")]
	partial class DeleteITemByIdViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField itemIdField { get; set; }

		[Action ("OnDeleteItemButtonTouched:")]
		partial void OnDeleteItemButtonTouched (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (itemIdField != null) {
				itemIdField.Dispose ();
				itemIdField = null;
			}
		}
	}
}
