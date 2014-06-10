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
		MonoTouch.UIKit.UITextField itemPathField { get; set; }

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (itemPathField != null) {
				itemPathField.Dispose ();
				itemPathField = null;
			}
		}
	}
}
