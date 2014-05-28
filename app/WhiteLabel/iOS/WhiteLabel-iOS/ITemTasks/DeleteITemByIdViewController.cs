
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

		partial void deleteItem (UIButton sender)
		{
			AlertHelper.ShowErrorAlertWithOkOption("Alert", "Not implemented yet");
		}
	}
}

