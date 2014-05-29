
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
			
			this.pathField.ShouldReturn = this.HideKeyboard;
			this.textField.ShouldReturn = this.HideKeyboard;
			this.titleField.ShouldReturn = this.HideKeyboard;
		}

		partial void createITemTouched (MonoTouch.Foundation.NSObject sender)
		{
			this.saveButton.Enabled = true;
			AlertHelper.ShowAlertWithOkOption("Alert", "Not implemented yet");
		}

		partial void saveItemTouched (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowAlertWithOkOption("Alert", "Not implemented yet");
		}
	}
}

