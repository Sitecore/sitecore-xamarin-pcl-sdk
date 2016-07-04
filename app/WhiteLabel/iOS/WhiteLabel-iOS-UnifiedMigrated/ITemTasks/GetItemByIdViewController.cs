
namespace WhiteLabeliOS
{
  using System;
  using System.Linq;

  using UIKit;
  using Foundation;

  using WhiteLabeliOS.FieldsTableView;

  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;


  public partial class GetItemByIdViewController : BaseTaskTableViewController
  {
    public GetItemByIdViewController(IntPtr handle) : base(handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("getItemById", null);
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.TableView = this.FieldsTableView;

      this.itemIdTextField.ShouldReturn = this.HideKeyboard;

      this.itemIdTextField.Text = "110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9";

      fieldNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type field name", null);
      itemIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type item ID", null);

      string getItemButtonTitle = NSBundle.MainBundle.LocalizedString("Get Item", null);
      getItemButton.SetTitle(getItemButtonTitle, UIControlState.Normal);
    }

    partial void OnGetItemButtonTouched(Foundation.NSObject sender)
    {
      if (String.IsNullOrEmpty(itemIdTextField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type item Id");
      }
      else
      {
        this.HideKeyboardForAllFields();
        this.SendRequest();
      }
    }

    private void HideKeyboardForAllFields()
    {
      this.HideKeyboard(this.itemIdTextField);
      this.HideKeyboard(this.fieldNameTextField);
    }

    partial void OnChildrenButtonTouched (Foundation.NSObject sender)
    {
      if (String.IsNullOrEmpty(itemIdTextField.Text))
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type item Id");
      }
      else
      {
        this.HideKeyboardForAllFields();
        this.SendChildrenRequest();
      }
    }

    private async void SendRequest()
    {
      try
      {
        using (ISitecoreSSCSession session = this.instanceSettings.GetSession())
        {
          this.ShowLoader();

          var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(itemIdTextField.Text)
                                             .AddFieldsToRead(this.fieldNameTextField.Text)
                                             .IcludeStanderdTemplateFields(true)
                                             .Build();

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
          this.HideLoader();
          this.FieldsTableView.ReloadData();
        });
      }
    }

    private async void SendChildrenRequest()
    {
      try
      {
        using (ISitecoreSSCSession session = this.instanceSettings.GetSession())
        {
          var request = ItemSSCRequestBuilder.ReadChildrenRequestWithId (itemIdTextField.Text)
                                             .AddFieldsToRead("bla")
                                             .Build();

          this.ShowLoader();

          ScItemsResponse response = await session.ReadChildrenAsync(request);

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
          this.HideLoader();
          this.FieldsTableView.ReloadData();
        });
      }
    }


  }
}

