using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class GetItemByIdViewController : BaseTaskViewController
	{
		public GetItemByIdViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("getItemById", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void getItem (MonoTouch.UIKit.UIButton sender)
		{
			AlertHelper.ShowErrorAlertWithOkOption("Error", "not implemented yet");
		}

	}
}

