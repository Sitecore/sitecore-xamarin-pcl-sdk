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
	[Register ("GetItemByIdViewController")]
	partial class GetItemByIdViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField itemIdTextField { get; set; }

		[Action ("getChildren:")]
		partial void getChildren (MonoTouch.Foundation.NSObject sender);

		[Action ("getItem:")]
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
