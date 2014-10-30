namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;


  #if __UNIFIED__
  using UIKit;
  using Foundation;
  using CoreGraphics;
  #else
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;

  using nint = global::System.Int32;
  using nfloat = global::System.Single;

  using CGRect = global::System.Drawing.RectangleF;
  using CGSize = global::System.Drawing.SizeF;
  using CGPoint = global::System.Drawing.PointF;
  #endif


	public class LoadingOverlay : UIView {

		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;

    public LoadingOverlay(CGRect frame, string text) : base (frame)
		{
			BackgroundColor = UIColor.Black;
			Alpha = 0.75f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			nfloat labelHeight = 22;
			nfloat labelWidth = Frame.Width - 20;

			nfloat centerX = Frame.Width / 2;
			nfloat centerY = Frame.Height / 2;

			activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
      activitySpinner.Frame = new CGRect (
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

