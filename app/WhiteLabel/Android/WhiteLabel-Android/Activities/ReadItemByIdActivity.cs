namespace WhiteLabelAndroid.Activities
{
  using System;
  using System.Linq;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
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
      this.itemFieldLabel.Text = this.GetString(Resource.String.text_id_label);
      this.ItemFieldEditText.Hint = this.GetString(Resource.String.hint_item_id);

      this.getItemsButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(this.ItemFieldEditText.Text))
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_error, Resource.String.text_empty_id);
          return;
        }

        this.HideKeyboard(this.ItemFieldEditText);
        this.PerformGetItemRequest(this.ItemFieldEditText.Text);
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

        if (!string.IsNullOrWhiteSpace(this.fieldNamEditText.Text))
        {
          builder.AddFieldsToRead(this.fieldNamEditText.Text);
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
        var title = this.GetString(Resource.String.text_item_received);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}