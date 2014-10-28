namespace WhiteLabelAndroid.Activities
{
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;

  [Activity]
  public class DeleteItemActivity : Activity
  {
    private Prefs prefs;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);

      SetContentView(Resource.Layout.activity_delete_item);
      this.Title = GetString(Resource.String.text_delete_item);

      this.prefs = Prefs.From(this);

      var deleteItemByIdButton = FindViewById<Button>(Resource.Id.button_delete_by_id);
      deleteItemByIdButton.Click += (sender, args) => this.DeleteItemById();

      var deleteItemByPathButton = FindViewById<Button>(Resource.Id.button_delete_by_path);
      deleteItemByPathButton.Click += (sender, args) => this.DeleteItemByPath();

      var deleteItemByQueryButton = FindViewById<Button>(Resource.Id.button_delete_by_query);
      deleteItemByQueryButton.Click += (sender, args) => this.DeleteItemByQuery();
    }

    private async void DeleteItemById()
    {
      var itemIdField = this.FindViewById<EditText>(Resource.Id.field_item_id);
      var itemId = itemIdField.Text;

      if (string.IsNullOrWhiteSpace(itemId))
      {
        Toast.MakeText(this, "Please enter item ID", ToastLength.Long).Show();
        return;
      }

      try
      {
        var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId(itemId).Build();

        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          var response = await session.DeleteItemAsync(request);

          this.SetProgressBarIndeterminateVisibility(false);
          this.ShowResult(response);
        }
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }

    private async void DeleteItemByPath()
    {
      var itemPathField = this.FindViewById<EditText>(Resource.Id.field_item_path);
      var itempath = itemPathField.Text;

      if (string.IsNullOrWhiteSpace(itempath))
      {
        Toast.MakeText(this, "Please enter item path", ToastLength.Long).Show();
        return;
      }

      try
      {
        var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(itempath).Build();

        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          var response = await session.DeleteItemAsync(request);

          this.SetProgressBarIndeterminateVisibility(false);
          this.ShowResult(response);
        }
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }

    private async void DeleteItemByQuery()
    {
      var itemQueryField = this.FindViewById<EditText>(Resource.Id.field_item_query);
      var itemQuery = itemQueryField.Text;

      if (string.IsNullOrWhiteSpace(itemQuery))
      {
        Toast.MakeText(this, "Please enter query", ToastLength.Long).Show();
        return;
      }

      try
      {
        var request = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(itemQuery).Build();

        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          var response = await session.DeleteItemAsync(request);

          this.SetProgressBarIndeterminateVisibility(false);

          this.ShowResult(response);
        }
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }

    private void ShowResult(ScDeleteItemsResponse response)
    {
      if (response.Count == 0)
      {
        DialogHelper.ShowSimpleDialog(this, "Failed", "No items deleted");
      }
      else
      {
        DialogHelper.ShowSimpleDialog(this, "Item deleted", "Item successsfully deleted");
      }
    }
  }
}