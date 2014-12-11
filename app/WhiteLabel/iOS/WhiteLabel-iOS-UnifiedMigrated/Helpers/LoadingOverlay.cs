using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace WhiteLabeliOS
{
	public class LoadingOverlay : UIView {

		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;

    public LoadingOverlay (CGRect frame, string text) : base (frame)
		{
			BackgroundColor = UIColor.Black;
			Alpha = 0.75f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

      nfloat labelHeight = 22;
      nfloat labelWidth = Frame.Width - 20;

      nfloat centerX = Frame.Width / 2;
      nfloat centerY = Frame.Height / 2;

			activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
      activitySpinner.Frame = new CGRect(
				centerX - (activitySpinner.Frame.Width / 2) ,
				centerY - activitySpinner.Frame.Height - 20 ,
				activitySpinner.Frame.Width ,
				activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview (activitySpinner);
			activitySpinner.StartAnimating ();

      loadingLabel = new UILabel(new CGRect (
				centerX - (labelWidth / 2),
				centerY + 20 ,
				labelWidth ,
				labelHeight
			));
			loadingLabel.BackgroundColor = UIColor.Clear;
			loadingLabel.TextColor = UIColor.White;
			loadingLabel.Text = text;
			loadingLabel.TextAlignment = UITextAlignment.Center;
			loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview (loadingLabel);
		}
			
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview(); }
			);
		}
	};
}

