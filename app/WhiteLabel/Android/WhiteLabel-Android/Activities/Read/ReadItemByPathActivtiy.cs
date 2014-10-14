namespace WhiteLabelAndroid.Activities.Read
{
  using System;
  using System.Linq;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Sitecore.MobileSDK.API;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByPathActivtiy : BaseReadItemActivity
  {

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      this.Title = this.GetString(Resource.String.text_get_item_by_path);

      this.InitViews();
    }

    private void InitViews()
    {
      this.ItemFieldLabel.Text = this.GetString(Resource.String.text_path_label);
      this.ItemFieldEditText.Hint = this.GetString(Resource.String.hint_item_path);

      this.GetItemsButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(this.ItemFieldEditText.Text))
        {
          DialogHelper.ShowSimpleDialog(this, this.GetString(Resource.String.text_error), this.GetString(Resource.String.text_empty_path));
          return;
        }

        this.HideKeyboard();
        this.PerformGetItemRequest(this.ItemFieldEditText.Text);
      };
    }

    private async void PerformGetItemRequest(string path)
    {
      try
      {
        var builder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(path)
          .Payload(this.GetSelectedPayload());

        var scopes = this.GetSelectedScopes();
        if (scopes.Any())
        {
          builder.AddScope(scopes);
        }

        if (!string.IsNullOrWhiteSpace(this.FieldNamEditText.Text))
        {
          builder.AddFieldsToRead(this.FieldNamEditText.Text);
        }

        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.Prefs.Session)
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