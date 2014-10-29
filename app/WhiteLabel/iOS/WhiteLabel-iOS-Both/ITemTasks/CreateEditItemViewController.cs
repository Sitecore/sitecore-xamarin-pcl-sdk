﻿namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using System.Linq;

  #if __UNIFIED__
  using UIKit;
  using Foundation;
  #else
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;
  #endif

  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;

	public partial class CreateEditItemViewController : BaseTaskViewController
	{
    private string CreatedItemId;

		public CreateEditItemViewController(IntPtr handle) : base(handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("createEditItem", null);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
      this.nameField.ShouldReturn = this.HideKeyboard;
			this.pathField.ShouldReturn = this.HideKeyboard;
			this.textField.ShouldReturn = this.HideKeyboard;
			this.titleField.ShouldReturn = this.HideKeyboard;

      this.nameField.Placeholder = NSBundle.MainBundle.LocalizedString("type item name", null);
      this.pathField.Placeholder = NSBundle.MainBundle.LocalizedString("type parent item id", null);
      this.textField.Placeholder = NSBundle.MainBundle.LocalizedString("type text field value", null);
      this.titleField.Placeholder = NSBundle.MainBundle.LocalizedString("type title field value", null);

      string createButtonTitle = NSBundle.MainBundle.LocalizedString("create", null);
      this.createButton.SetTitle(createButtonTitle, UIControlState.Normal);

      string updateButtonTitle = NSBundle.MainBundle.LocalizedString("Update created item", null);
      this.updateButton.SetTitle(updateButtonTitle, UIControlState.Normal);
		}

		partial void OnCreateItemButtonTapped(MonoTouch.Foundation.NSObject sender)
		{
      this.SendRequest();
		}

    partial void OnUpdateItemButtonTapped(MonoTouch.Foundation.NSObject sender)
    {
      if (null == this.CreatedItemId)
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
        using ( var session = this.instanceSettings.GetSession() )
        {
          var request = ItemWebApiRequestBuilder.UpdateItemRequestWithId(this.CreatedItemId)
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
        using ( ISitecoreWebApiSession session = this.instanceSettings.GetSession() )
        {
          var request = ItemWebApiRequestBuilder.CreateItemRequestWithParentId(this.pathField.Text)
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
            this.CreatedItemId = item.Id;
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

