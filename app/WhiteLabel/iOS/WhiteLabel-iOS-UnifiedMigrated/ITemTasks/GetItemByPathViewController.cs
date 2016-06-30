namespace WhiteLabeliOS
{
  using System;
  using System.Linq;
  using System.Drawing;

  using Foundation;
  using UIKit;

  using WhiteLabeliOS.FieldsTableView;

  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;

  public partial class GetItemByPathViewController : BaseTaskTableViewController
  {

    public GetItemByPathViewController(IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("getItemByPath", null);
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      this.TableView = this.FieldsTableView;

      this.ItemPathField.Text = "/sitecore/content/Home";

      this.ItemPathField.ShouldReturn = this.HideKeyboard;

      this.ItemPathField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type item Path", null);
      this.fieldNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type field name", null);

      string getItemButtonTitle = NSBundle.MainBundle.LocalizedString ("Get Item", null);
      getItemButton.SetTitle (getItemButtonTitle, UIControlState.Normal);
    }

    partial void OnGetItemButtonTouched (Foundation.NSObject sender)
    {
      if (String.IsNullOrEmpty(this.ItemPathField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type item path");
      }
      else
      {
        this.HideKeyboard(this.ItemPathField);
        this.HideKeyboard(this.fieldNameTextField);

        this.SendRequest();
      }
    }

    partial void OnPayloadValueChanged (UIKit.UISegmentedControl sender)
    {
      
    }

    partial void OnButtonChangeState (UIKit.UIButton sender)
    {
      sender.Selected = !sender.Selected;
    }

    private async void SendRequest()
    {
      try
      {
        using (ISitecoreSSCSession session = this.instanceSettings.GetSession())
        {
          var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.ItemPathField.Text)
            .AddFieldsToRead(this.fieldNameTextField.Text)
            .Build();
          
          this.ShowLoader();

          ScItemsResponse response = await session.ReadItemAsync(request);

          if (response.Any())
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
        this.CleanupTableViewBindings();
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
      finally
      {
        BeginInvokeOnMainThread(delegate
        {
          this.FieldsTableView.ReloadData();
          this.HideLoader();
        });
      }
    }

  }

}

