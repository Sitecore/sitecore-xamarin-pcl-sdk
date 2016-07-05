using Sitecore.MobileSDK.API.Session;
using Sitecore.MobileSDK.API;
using System.IO;

namespace WhiteLabeliOS
{
  using System;

  using Foundation;
  using UIKit;

  public partial class RenderingHtmlViewController : BaseTaskViewController
	{
		public RenderingHtmlViewController (IntPtr handle) : base (handle)
		{
      Title = NSBundle.MainBundle.LocalizedString("getRenderingHtml", null);
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad ();

      string getRenderingButtonTitle = NSBundle.MainBundle.LocalizedString("Get rendering html", null);
      getRenderingButton.SetTitle(getRenderingButtonTitle, UIControlState.Normal);

      sourceIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Source id", null);
      renderingIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Rendering id", null);
    }

    partial void OnGetRenderingTouch (Foundation.NSObject sender)
    {
      if (String.IsNullOrEmpty(sourceIdTextField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type source Id");
      }
      else if (String.IsNullOrEmpty(renderingIdTextField.Text))
        {
          AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type rendering Id");
        }
        else
        {
          this.HideKeyboardForAllFields();
        }
    }

    private void HideKeyboardForAllFields()
    {
      this.HideKeyboard(this.renderingIdTextField);
      this.HideKeyboard(this.sourceIdTextField);
    }



	}
}
