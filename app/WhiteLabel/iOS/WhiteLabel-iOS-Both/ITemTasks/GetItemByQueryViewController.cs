﻿namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;

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


  public partial class GetItemByQueryViewController : BaseTaskTableViewController
  {
    public GetItemByQueryViewController(IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("getItemByQuery", null);
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      this.TableView = this.ItemsTableView;

      queryTextField.Text = "/Sitecore/Content/Home/*";


      string getChildrenButtonTitle = NSBundle.MainBundle.LocalizedString("Get Item", null);
      getItemButton.SetTitle(getChildrenButtonTitle, UIControlState.Normal);

      nameLabel.Text = NSBundle.MainBundle.LocalizedString("Type query", null);
    }

    partial void OnGetItemButtonTouched(NSObject sender)
    {
      if (String.IsNullOrEmpty(queryTextField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type query");
      }
      else
      {
        this.HideKeyboard(this.queryTextField);
        this.SendRequest();
      }
    }

    private async void SendRequest()
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(queryTextField.Text)
            .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.ReadItemAsync(request);

          this.HideLoader();
          if (response.ResultCount > 0)
          {
            this.ShowItemsList(response);
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
          }
        }
      }
      catch(Exception e) 
      {
        this.HideLoader();
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
    }
  }
}

