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
    private string CreatedItemPath;

    public CreateITemByPathViewController(IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("createItemByPath", null);
    }
      
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.nameField.ShouldReturn = this.HideKeyboard;
      this.pathField.ShouldReturn = this.HideKeyboard;
      this.textField.ShouldReturn = this.HideKeyboard;
      this.titleField.ShouldReturn = this.HideKeyboard;

      this.nameField.Placeholder = NSBundle.MainBundle.LocalizedString("type item name", null);
      this.pathField.Placeholder = NSBundle.MainBundle.LocalizedString("type parent item path", null);
      this.textField.Placeholder = NSBundle.MainBundle.LocalizedString("type text field value", null);
      this.titleField.Placeholder = NSBundle.MainBundle.LocalizedString("type title field value", null);

      string createButtonTitle = NSBundle.MainBundle.LocalizedString("create", null);
      this.createButton.SetTitle(createButtonTitle, UIControlState.Normal);

      string updateButtonTitle = NSBundle.MainBundle.LocalizedString("Update created item", null);
      this.updateButton.SetTitle(updateButtonTitle, UIControlState.Normal);
    }

    partial void OnCreateItemButtonTapped(UIKit.UIButton sender)
    {
      this.SendRequest();
    }

    partial void OnUpdateItemButtonTapped(UIKit.UIButton sender)
    {
      if (null == this.CreatedItemPath)
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Create item at first");
      }
      else
      {
        this.SendUpdateRequest();
      }
    }

    private async void SendUpdateRequest()
    {
      try
      {
        using (var session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(this.CreatedItemPath)
            .AddFieldsRawValuesByNameToSet("Title", titleField.Text)
            .AddFieldsRawValuesByNameToSet("Text", textField.Text)
            .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.UpdateItemAsync(request);
          if (response.Any())
          {
            ISitecoreItem item = response[0];
            AlertHelper.ShowLocalizedAlertWithOkOption("The item created successfully", "Item path: " + item.Path);
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
          }
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

    private async void SendRequest()
    {
      try
      {
        using (var session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.CreateItemRequestWithParentPath(this.pathField.Text)
            .ItemTemplatePath("Sample/Sample Item")
            .ItemName(this.nameField.Text)
            .AddFieldsRawValuesByNameToSet("Title", titleField.Text)
            .AddFieldsRawValuesByNameToSet("Text", textField.Text)
            .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.CreateItemAsync(request);
          if (response.Any())
          {
            ISitecoreItem item = response[0];
            this.CreatedItemPath = item.Path;
            AlertHelper.ShowLocalizedAlertWithOkOption("The item created successfully", "Item path: " + item.Path);
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
          }
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

