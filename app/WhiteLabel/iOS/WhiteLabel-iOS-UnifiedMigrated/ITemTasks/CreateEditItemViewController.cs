﻿namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using System.Linq;

  using Foundation;
  using UIKit;

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
      this.pathField.Placeholder = NSBundle.MainBundle.LocalizedString("type parent item path", null);
      this.textField.Placeholder = NSBundle.MainBundle.LocalizedString("type text field value", null);
      this.titleField.Placeholder = NSBundle.MainBundle.LocalizedString("type title field value", null);

      string createButtonTitle = NSBundle.MainBundle.LocalizedString("create", null);
      this.createButton.SetTitle(createButtonTitle, UIControlState.Normal);

      this.pathField.Text = "/sitecore/content/Home";
		}

		partial void OnCreateItemButtonTapped(Foundation.NSObject sender)
		{
      this.SendRequest();
		}

    private async void SendRequest()
    {
      try
      {
        using ( ISitecoreSSCSession session = this.instanceSettings.GetSession() )
        {
          var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.pathField.Text)
            .ItemTemplatePath("76036f5e-cbce-46d1-af0a-4143f9b557aa")
            .ItemName(this.nameField.Text)
            .AddFieldsRawValuesByNameToSet("Title", titleField.Text)
            .AddFieldsRawValuesByNameToSet("Text", textField.Text)
            .Build();
           
          this.ShowLoader();

          var response = await session.CreateItemAsync(request);
          if (response.Created)
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "The item created successfully");
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item was not created");
          }
        }
      }
      catch(Exception e) 
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Message", "The item created successfully");
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

