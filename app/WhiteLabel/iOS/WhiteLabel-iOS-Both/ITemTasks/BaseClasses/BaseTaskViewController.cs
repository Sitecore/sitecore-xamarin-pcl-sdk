﻿
namespace WhiteLabeliOS
{
	using System;
	using System.Drawing;

  #if __UNIFIED__
  using UIKit;
  using Foundation;
  #else
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;
  #endif

  using Sitecore.MobileSDK.API.Request.Parameters;

	public partial class BaseTaskViewController : UIViewController
	{
		public InstanceSettings instanceSettings {get; set;}
		private LoadingOverlay loadingOverlay;
    protected PayloadType currentPayloadType = PayloadSelectorViewHelper.DEFAULT_PAYLOAD;

		public BaseTaskViewController(IntPtr handle) : base (handle)
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
			if (this.loadingOverlay != null)
			{
				this.loadingOverlay.Hide ();
			}
		}
	}
}

