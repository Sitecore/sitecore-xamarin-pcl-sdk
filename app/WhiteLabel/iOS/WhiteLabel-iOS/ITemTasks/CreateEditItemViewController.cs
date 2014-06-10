
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

		partial void OnCreateItemButtonTapped (MonoTouch.Foundation.NSObject sender)
		{
			this.saveButton.Enabled = true;
			AlertHelper.ShowLocalizedNotImlementedAlert();
		}

		partial void OnSaveItemButtonTapped (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowLocalizedNotImlementedAlert();
		}
	}
}

