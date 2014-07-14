
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
			
      this.nameField.ShouldReturn = this.HideKeyboard;
			this.pathField.ShouldReturn = this.HideKeyboard;
			this.textField.ShouldReturn = this.HideKeyboard;
			this.titleField.ShouldReturn = this.HideKeyboard;
		}

		partial void OnCreateItemButtonTapped (MonoTouch.Foundation.NSObject sender)
		{
      this.SendRequest();
		}

    private async void SendRequest()
    {
      try
      {
        ScApiSession session = this.instanceSettings.GetSession();

        var request = ItemWebApiRequestBuilder.CreateItemRequestWithId(this.pathField.Text)
          .Database("web")
          .ItemTemplate("Sample/Sample Item")
          .ItemName(this.nameField.Text)
          .AddFieldsRawValuesByName("Title", titleField.Text)
          .AddFieldsRawValuesByName("Text", textField.Text)
          .Build();

        this.ShowLoader();

        ScItemsResponse response = await session.CreateItemAsync(request);
        if (response.Items.Any())
        {
          ISitecoreItem item = response.Items[0];
          AlertHelper.ShowLocalizedAlertWithOkOption("The item created successfully", "Item path: " + item.Path);
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

