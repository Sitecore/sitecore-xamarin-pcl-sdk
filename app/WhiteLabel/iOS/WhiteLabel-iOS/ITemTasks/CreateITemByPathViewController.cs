
namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using System.Linq;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;

  public partial class CreateITemByPathViewController : BaseTaskViewController
  {
    public CreateITemByPathViewController (IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString ("createItemByPath", null);
    }
      
    public override void ViewDidLoad ()
    {
      base.ViewDidLoad ();

      this.nameField.ShouldReturn = this.HideKeyboard;
      this.pathField.ShouldReturn = this.HideKeyboard;
      this.textField.ShouldReturn = this.HideKeyboard;
      this.titleField.ShouldReturn = this.HideKeyboard;
    }

    partial void OnCreateItemButtonTapped (MonoTouch.UIKit.UIButton sender)
    {
      this.SendRequest();
    }

    partial void OnUpdateItemButtonTapped (MonoTouch.UIKit.UIButton sender)
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
        var session = this.instanceSettings.GetSession();

        var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(this.CreatedItemPath)
          .Database("web")
          .AddFieldsRawValuesByName("Title", titleField.Text)
          .AddFieldsRawValuesByName("Text", textField.Text)
          .Build();

        this.ShowLoader();

        ScItemsResponse response = await session.UpdateItemAsync(request);
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

    private async void SendRequest()
    {
      try
      {
        var session = this.instanceSettings.GetSession();

        var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.pathField.Text)
          .ItemTemplatePath("Sample/Sample Item")
          .ItemName(this.nameField.Text)
          .Database("web")
          .AddFieldsRawValuesByName("Title", titleField.Text)
          .AddFieldsRawValuesByName("Text", textField.Text)
          .Build();

        this.ShowLoader();

        ScItemsResponse response = await session.CreateItemAsync(request);
        if (response.Items.Any())
        {
          ISitecoreItem item = response.Items[0];
          this.CreatedItemPath = item.Path;
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

    private string CreatedItemPath;

  }
}

