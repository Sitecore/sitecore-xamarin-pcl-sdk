namespace WhiteLabelAndroid.SubActivities
{
  using System;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using WhiteLabelAndroid.Activities;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByQueryActivtiy : BaseReadItemActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      this.Title = this.GetString(Resource.String.text_get_item_by_query);

      this.InitViews();
    }

    private void InitViews()
    {
      this.ItemFieldLabel.Text = GetString(Resource.String.text_query_label);

      this.ItemFieldEditText.Hint = GetString(Resource.String.hint_query);

      this.ScopeContainer.Visibility = ViewStates.Gone;
      this.PayloadContainer.Visibility = ViewStates.Gone;
      this.FieldNameContainer.Visibility = ViewStates.Gone;

      this.GetItemsButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(this.ItemFieldEditText.Text))
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_error,
            Resource.String.text_empty_query);
          return;
        }

        this.HideKeyboard();
        this.PerformGetItemRequest(this.ItemFieldEditText.Text);
      };
    }

    private async void PerformGetItemRequest(string query)
    {
      try
      {
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(query).Build();
        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          var response = await session.ReadItemAsync(request);

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