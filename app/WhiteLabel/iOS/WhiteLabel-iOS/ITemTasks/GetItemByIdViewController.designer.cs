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
    MonoTouch.UIKit.UIButton parentScopeButton { get; set; }

    [Outlet]
    MonoTouch.UIKit.UIButton selfScopeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton childrenScopeButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldNameTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView FieldsTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton getChildrenButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton getItemButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField itemIdTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl PayloadSelectionView { get; set; }

		[Action ("OnButtonChangeState:")]
		partial void OnButtonChangeState (MonoTouch.UIKit.UIButton sender);

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

			if (getChildrenButton != null) {
				getChildrenButton.Dispose ();
				getChildrenButton = null;
			}

			if (getItemButton != null) {
				getItemButton.Dispose ();
				getItemButton = null;
			}

			if (itemIdTextField != null) {
				itemIdTextField.Dispose ();
				itemIdTextField = null;
			}

      if (PayloadSelectionView != null)
      {
        PayloadSelectionView.Dispose ();
        PayloadSelectionView = null;
      }

      if (parentScopeButton != null) {
				parentScopeButton.Dispose ();
				parentScopeButton = null;
			}

			if (selfScopeButton != null) {
				selfScopeButton.Dispose ();
				selfScopeButton = null;
			}

			if (childrenScopeButton != null) {
				childrenScopeButton.Dispose ();
				childrenScopeButton = null;
			}

	  }
  }
}
