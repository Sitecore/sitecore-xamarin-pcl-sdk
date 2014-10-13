namespace WhiteLabelAndroid.SubActivities
{
  using System;
  using System.Linq;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Widget;
  using Sitecore.MobileSDK.API;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByIdActivity : BaseReadItemActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      this.Title = this.GetString(Resource.String.text_get_item_by_id);
      this.InitViews();
    }

    private void InitViews()
    {
      var label = this.FindViewById<TextView>(Resource.Id.label);
      label.Text = GetString(Resource.String.text_id_label);

      var itemIdField = this.FindViewById<EditText>(Resource.Id.field_item);
      itemIdField.Hint = GetString(Resource.String.hint_item_id);

      var getItemsButton = this.FindViewById<Button>(Resource.Id.button_get_item);
      getItemsButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(itemIdField.Text))
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_error, Resource.String.text_empty_id);
          return;
        }

        this.HideKeyboard(itemIdField);
        this.PerformGetItemRequest(itemIdField.Text);
      };
    }

    private async void PerformGetItemRequest(string id)
    {
      try
      {
        var builder = ItemWebApiRequestBuilder.ReadItemsRequestWithId(id)
          .Payload(this.GetSelectedPayload());

        var scopes = this.GetSelectedScopes();

        if (scopes.Any())
        {
          builder.AddScope(scopes);
        }

        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          var response = await session.ReadItemAsync(builder.Build());

          this.SetProgressBarIndeterminateVisibility(false);
          if (response.ResultCount == 0)
          {
            DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, Resource.String.text_no_item);
          }
          else
          {
            this.PopulateItemsList(response);
          }
        }
      }
      catch (Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);
        var title = GetString(Resource.String.text_item_received);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}