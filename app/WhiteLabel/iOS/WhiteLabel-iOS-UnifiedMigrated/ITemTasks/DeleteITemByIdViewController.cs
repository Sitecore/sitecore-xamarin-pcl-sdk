
namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;

  using Foundation;
  using UIKit;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Items;

	public partial class DeleteITemByIdViewController : BaseTaskViewController
	{
		public DeleteITemByIdViewController(IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("deleteItemById", null);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

      this.itemIdField.ShouldReturn = this.HideKeyboard;
      this.itemPathField.ShouldReturn = this.HideKeyboard;
      this.itemQueryField.ShouldReturn = this.HideKeyboard;

      this.itemIdField.Placeholder = NSBundle.MainBundle.LocalizedString("Type item ID", null);
      this.itemPathField.Placeholder = NSBundle.MainBundle.LocalizedString("Type item Path", null);
      this.itemQueryField.Placeholder = NSBundle.MainBundle.LocalizedString("Type query", null);

      string deleteByIdButtonTitle = NSBundle.MainBundle.LocalizedString("Delete item by Id", null);
      this.deleteByIdButton.SetTitle(deleteByIdButtonTitle, UIControlState.Normal);

      string deleteByPathButtonTitle = NSBundle.MainBundle.LocalizedString("Delete item by Path", null);
      this.deleteByPathButton.SetTitle(deleteByPathButtonTitle, UIControlState.Normal);

      string deleteByQueryButtonTitle = NSBundle.MainBundle.LocalizedString("Delete item by Query", null);
      this.deleteByQueryButton.SetTitle(deleteByQueryButtonTitle, UIControlState.Normal);
		}
      
    partial void OnDeleteItemByIdButtonTouched(UIKit.UIButton sender)
    {
      this.SendDeleteByIdRequest();
    }

    partial void OnDeleteItemByPathButtonTouched(UIKit.UIButton sender)
    {
      this.SendDeleteByPathRequest();
    }

    partial void OnDeleteItemByqueryButtonTouched(UIKit.UIButton sender)
    {
      this.SendDeleteByQueryRequest();
    }

    private async void SendDeleteByIdRequest()
    {
      try
      {
          using (var session = this.instanceSettings.GetSession())
          {

          var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId(this.itemIdField.Text)
            .Build();

          this.ShowLoader();

          ScDeleteItemsResponse response = await session.DeleteItemAsync(request);

          this.ProceedResponce(response);
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

    private async void SendDeleteByPathRequest()
    {
      try
      {
        using (var session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(this.itemPathField.Text)
            .Build();

          this.ShowLoader();

          ScDeleteItemsResponse response = await session.DeleteItemAsync(request);

          this.ProceedResponce(response);
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

    private async void SendDeleteByQueryRequest()
    {
      try
      {
        using (var session = this.instanceSettings.GetSession())
        {
          var request = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(this.itemQueryField.Text)
            .Build();

          this.ShowLoader();

          ScDeleteItemsResponse response = await session.DeleteItemAsync(request);

          this.ProceedResponce(response);
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

    private void ProceedResponce(ScDeleteItemsResponse response)
    {
      if (response.Count > 0)
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Message", "The item deleted successfully");
      }
      else
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
      }
    }

	}
}

