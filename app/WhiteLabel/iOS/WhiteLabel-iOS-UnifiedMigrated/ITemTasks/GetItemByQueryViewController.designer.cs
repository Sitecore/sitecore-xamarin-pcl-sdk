// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("GetItemByQueryViewController")]
	partial class GetItemByQueryViewController
	{
		[Outlet]
		UIKit.UIButton getItemButton { get; set; }

		[Outlet]
		UIKit.UITableView ItemsTableView { get; set; }

		[Outlet]
		UIKit.UILabel nameLabel { get; set; }

		[Outlet]
		UIKit.UITextField queryTextField { get; set; }

		[Outlet]
		UIKit.UITextField termTextField { get; set; }

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (Foundation.NSObject sender);

		[Action ("OnStoredSearchButtonTouched:")]
		partial void OnStoredSearchButtonTouched (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (getItemButton != null) {
				getItemButton.Dispose ();
				getItemButton = null;
			}

			if (ItemsTableView != null) {
				ItemsTableView.Dispose ();
				ItemsTableView = null;
			}

			if (nameLabel != null) {
				nameLabel.Dispose ();
				nameLabel = null;
			}

			if (queryTextField != null) {
				queryTextField.Dispose ();
				queryTextField = null;
			}

			if (termTextField != null) {
				termTextField.Dispose ();
				termTextField = null;
			}
		}
	}
}
