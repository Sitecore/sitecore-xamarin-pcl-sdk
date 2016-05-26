namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;

  using Foundation;
  using UIKit;

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

      queryTextField.Text = "06DC2442-78F6-4BD1-8C8D-A3251BDAA5F2";

      string getChildrenButtonTitle = NSBundle.MainBundle.LocalizedString("Run Stored Query", null);
      getItemButton.SetTitle(getChildrenButtonTitle, UIControlState.Normal);

      nameLabel.Text = NSBundle.MainBundle.LocalizedString("Type query or search id:", null);
    }

    partial void OnGetItemButtonTouched(Foundation.NSObject sender)
    {
      if (String.IsNullOrEmpty(queryTextField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type query id");
      }
      else
      {
        this.HideKeyboard(this.queryTextField);
        this.SendQueryRequest();
      }
    }

    partial void OnStoredSearchButtonTouched (Foundation.NSObject sender)
    {
      if (String.IsNullOrEmpty(queryTextField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type search id");
      }
      else
      {
        this.HideKeyboard(this.queryTextField);
        this.SendSearchRequest();
      }
    }

    private async void SendQueryRequest()
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.StoredQuerryRequest(queryTextField.Text)
            .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.RunStoredQuerryAsync(request);

          this.HideLoader();
          if (response.ResultCount > 0)
          {
            this.ShowItemsList(response);
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "No item found");
          }
        }
      }
      catch(Exception e) 
      {
        this.HideLoader();
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
    }

    private async void SendSearchRequest()
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.StoredSearchRequest(queryTextField.Text)
            .Term(this.termTextField.Text)
            .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.RunStoredSearchAsync(request);

          this.HideLoader();
          if (response.ResultCount > 0)
          {
            this.ShowItemsList(response);
          }
          else
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Message", "No item found");
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

