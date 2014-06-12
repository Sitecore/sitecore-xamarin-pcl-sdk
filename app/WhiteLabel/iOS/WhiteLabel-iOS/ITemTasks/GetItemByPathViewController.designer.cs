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
	[Register ("GetItemByPathViewController")]
	partial class GetItemByPathViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView FieldsTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField ItemPathField { get; set; }

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ItemPathField != null) {
				ItemPathField.Dispose ();
				ItemPathField = null;
			}

			if (FieldsTableView != null) {
				FieldsTableView.Dispose ();
				FieldsTableView = null;
			}
		}
	}
}
