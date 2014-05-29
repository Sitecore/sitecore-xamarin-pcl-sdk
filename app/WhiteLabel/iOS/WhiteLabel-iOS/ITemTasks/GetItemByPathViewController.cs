using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class GetItemByPathViewController : BaseTaskViewController
	{

		public GetItemByPathViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("getItemByPath", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.itemPathField.ShouldReturn = this.HideKeyboard;
		}

		partial void getItem (MonoTouch.UIKit.UIButton sender)
		{
			AlertHelper.ShowAlertWithOkOption("Alert", "Not implemented yet");
		}
	}
}

