
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class CreateEditItemViewController : BaseTaskViewController
	{
		public CreateEditItemViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("createEditItem", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void createITemTouched (MonoTouch.Foundation.NSObject sender)
		{
			this.saveButton.Enabled = true;
			AlertHelper.ShowErrorAlertWithOkOption("Alert", "Not implemented yet");
		}

		partial void saveItemTouched (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowErrorAlertWithOkOption("Alert", "Not implemented yet");
		}
	}
}

