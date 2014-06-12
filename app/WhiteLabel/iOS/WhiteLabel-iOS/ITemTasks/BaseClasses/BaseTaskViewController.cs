

namespace WhiteLabeliOS
{
	using System;
	using System.Drawing;

	using MonoTouch.Foundation;
	using MonoTouch.UIKit;
	using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

	public partial class BaseTaskViewController : UIViewController
	{
		public InstanceSettings instanceSettings {get; set;}
		private LoadingOverlay loadingOverlay;
		protected PayloadType currentPayloadType = PayloadType.Full;

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
			this.loadingOverlay = new LoadingOverlay (this.View.Bounds, NSBundle.MainBundle.LocalizedString ("Loading Data", null));
			View.Add (loadingOverlay);
		}

		public void HideLoader()
		{
			this.loadingOverlay.Hide ();
		}
	}
}

