
namespace WhiteLabeliOS
{
  using System;
  using System.Linq;

  #if __UNIFIED__
  using UIKit;
  using Foundation;
  #else
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;
  #endif


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

      this.itemIdTextField.Text = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";

      fieldNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type field name", null);
      itemIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type item ID", null);

      string getItemButtonTitle = NSBundle.MainBundle.LocalizedString("Get Item", null);
      getItemButton.SetTitle(getItemButtonTitle, UIControlState.Normal);
    }

    partial void OnGetItemButtonTouched(NSObject sender)
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

    partial void OnPayloadValueChanged(UISegmentedControl sender)
    {
      switch (sender.SelectedSegment)
      {
        case 0:
        {
          this.currentPayloadType = PayloadType.Full;
          break;
        }
        case 1:
        {
          this.currentPayloadType = PayloadType.Content;
          break;
        }
        case 2:
        {
          this.currentPayloadType = PayloadType.Min;
          break;
        }
      }
    }

    partial void OnButtonChangeState(UIButton sender)
    {
      sender.Selected = !sender.Selected;
    }

    private async void SendRequest()
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          var builder = ItemWebApiRequestBuilder.ReadItemsRequestWithId(itemIdTextField.Text)
            .Payload(this.currentPayloadType)
            .AddFieldsToRead(this.fieldNameTextField.Text);

          if (this.parentScopeButton.Selected)
          {
            builder = builder.AddScope(ScopeType.Parent);
          }
          if (this.selfScopeButton.Selected)
          {
            builder = builder.AddScope(ScopeType.Self);
          }
          if (this.childrenScopeButton.Selected)
          {
            builder = builder.AddScope(ScopeType.Children);
          }

          var request = builder.Build();

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
          this.HideLoader();
          this.FieldsTableView.ReloadData();
        });
      }
    }
  }
}

