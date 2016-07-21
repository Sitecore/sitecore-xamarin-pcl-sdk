namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using System.Linq;

  using Foundation;
  using UIKit;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;

  public partial class CreateITemByPathViewController : BaseTaskViewController
  {

    public CreateITemByPathViewController(IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("updateItem", null);
    }
      
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.pathField.ShouldReturn = this.HideKeyboard;
      this.textField.ShouldReturn = this.HideKeyboard;
      this.titleField.ShouldReturn = this.HideKeyboard;

      this.pathField.Placeholder = NSBundle.MainBundle.LocalizedString("type item id", null);
      this.textField.Placeholder = NSBundle.MainBundle.LocalizedString("type text field value", null);
      this.titleField.Placeholder = NSBundle.MainBundle.LocalizedString("type title field value", null);

      string updateButtonTitle = NSBundle.MainBundle.LocalizedString("Update created item", null);
      this.updateButton.SetTitle(updateButtonTitle, UIControlState.Normal);
    }

    partial void OnUpdateItemButtonTapped(UIKit.UIButton sender)
    {
      this.SendUpdateRequest();
    }

    private async void SendUpdateRequest()
    {
      try
      {
        using (var session = this.instanceSettings.GetSession())
        {
          var request = ItemSSCRequestBuilder.UpdateItemRequestWithId(this.pathField.Text)
            .AddFieldsRawValuesByNameToSet("Title", this.titleField.Text)
            .AddFieldsRawValuesByNameToSet("Text", this.textField.Text)
            .Database("master")
            .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.UpdateItemAsync(request);
          if (response != null)
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "The item updated successfully");
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
          }
        }
      }
      catch(Exception e) 
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Message", "The item updated successfully");
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

