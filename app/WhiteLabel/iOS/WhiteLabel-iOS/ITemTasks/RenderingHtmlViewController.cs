using Sitecore.MobileSDK.API.Session;
using Sitecore.MobileSDK.API;
using System.IO;

namespace WhiteLabeliOS
{
  using System;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  public partial class RenderingHtmlViewController : BaseTaskViewController
	{
		public RenderingHtmlViewController (IntPtr handle) : base (handle)
		{
      Title = NSBundle.MainBundle.LocalizedString("getRenderingHtml", null);
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad ();

      string getRenderingButtonTitle = NSBundle.MainBundle.LocalizedString("GET_RENDERING_BUTTON_TITLE", null);
      getRenderingButton.SetTitle(getRenderingButtonTitle, UIControlState.Normal);

      sourceIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString("SOURCE_ID_PLACEHOLDER", null);
      renderingIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString("RENDERING_ID_PLACEHOLDER", null);
    }

    partial void OnGetRenderingTouch (MonoTouch.Foundation.NSObject sender)
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
          this.SendRequest();
        }
    }

    private void HideKeyboardForAllFields()
    {
      this.HideKeyboard(this.renderingIdTextField);
      this.HideKeyboard(this.sourceIdTextField);
    }

    private async void SendRequest()
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceRenderingId(sourceIdTextField.Text, renderingIdTextField.Text)
            .Build();

          this.ShowLoader();

          Stream response = await session.ReadRenderingHtmlAsync(request);

          response.Position = 0;
          string htmlText = "";
          using (StreamReader reader = new StreamReader(response))
          {
            htmlText = reader.ReadToEnd();
          }

          this.resultWebView.LoadHtmlString(htmlText, null);
        }
      }
      catch(Exception e) 
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
      finally
      {
        BeginInvokeOnMainThread(delegate
        {
          this.HideLoader();
        });
      }
    }

	}
}
