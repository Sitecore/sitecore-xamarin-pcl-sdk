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
		MonoTouch.UIKit.UITextField fieldNameTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView FieldsTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton getItemButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField ItemPathField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl PayloadSelectionView { get; set; }

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender);

		[Action ("OnPayloadValueChanged:")]
		partial void OnPayloadValueChanged (MonoTouch.UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (fieldNameTextField != null) {
				fieldNameTextField.Dispose ();
				fieldNameTextField = null;
			}

			if (FieldsTableView != null) {
				FieldsTableView.Dispose ();
				FieldsTableView = null;
			}

			if (getItemButton != null) {
				getItemButton.Dispose ();
				getItemButton = null;
			}

			if (ItemPathField != null) {
				ItemPathField.Dispose ();
				ItemPathField = null;
			}

			if (PayloadSelectionView != null) {
				PayloadSelectionView.Dispose ();
				PayloadSelectionView = null;
			}
		}
	}
}
