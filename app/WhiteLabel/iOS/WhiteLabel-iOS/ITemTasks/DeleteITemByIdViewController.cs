
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class DeleteITemByIdViewController : BaseTaskViewController
	{
		public DeleteITemByIdViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("deleteItemById", null);

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.itemIdField.ShouldReturn = this.HideKeyboard;
		}

		partial void deleteItem (UIButton sender)
		{
			AlertHelper.ShowAlertWithOkOption("Alert", "Not implemented yet");
		}
	}
}

