
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;
using System.Linq;

namespace WhiteLabeliOS
{
	public partial class CreateEditItemViewController : BaseTaskViewController
	{
		public CreateEditItemViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("createEditItem", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.pathField.ShouldReturn = this.HideKeyboard;
			this.textField.ShouldReturn = this.HideKeyboard;
			this.titleField.ShouldReturn = this.HideKeyboard;
		}

		partial void OnCreateItemButtonTapped (MonoTouch.Foundation.NSObject sender)
		{
			this.saveButton.Enabled = true;
			//AlertHelper.ShowLocalizedNotImlementedAlert();
      this.SendRequest();
		}

		partial void OnSaveItemButtonTapped (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowLocalizedNotImlementedAlert();
		}

    private async void SendRequest()
    {
      try
      {
        ScApiSession session = this.instanceSettings.GetSession();

        var request = ItemWebApiRequestBuilder.CreateItemRequestWithId(pathField.Text)
          .ItemTemplate("Sample/Sample Item")
          .Build();

        this.ShowLoader();

        ScItemsResponse response = await session.CreateItemAsync(request);
        if (response.Items.Any())
        {
          AlertHelper.ShowLocalizedAlertWithOkOption("Message", "OK");
        }
        else
        {
          AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
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

