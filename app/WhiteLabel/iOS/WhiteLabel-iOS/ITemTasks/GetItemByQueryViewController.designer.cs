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
	[Register ("GetItemByQueryViewController")]
	partial class GetItemByQueryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField queryTextField { get; set; }

		[Action ("getItem:")]
		partial void getItem (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (queryTextField != null) {
				queryTextField.Dispose ();
				queryTextField = null;
			}
		}
	}
}
