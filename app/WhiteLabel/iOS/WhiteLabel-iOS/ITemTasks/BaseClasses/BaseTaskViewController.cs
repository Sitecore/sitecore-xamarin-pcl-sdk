
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class BaseTaskViewController : UIViewController
	{
		public InstanceSettings instanceSettings {get; set;}
		private LoadingOverlay loadingOverlay;

		public BaseTaskViewController (IntPtr handle) : base (handle)
		{

		}

		public bool HideKeyboard(MonoTouch.UIKit.UITextField sender)
		{
			sender.ResignFirstResponder();
			return true;
		}

		public void ShowLoader()
		{
			loadingOverlay = new LoadingOverlay (this.View.Bounds, NSBundle.MainBundle.LocalizedString ("Loading Data", null));
			View.Add (loadingOverlay);
		}

		public void HideLoader()
		{
			loadingOverlay.Hide ();
		}
	}
}

