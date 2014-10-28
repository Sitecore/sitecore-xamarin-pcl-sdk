namespace WhiteLabelAndroid.Activities.Read
{
  using System;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Sitecore.MobileSDK.API;

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
      this.ItemFieldLabel.Text = this.GetString(Resource.String.text_query_label);

      this.ItemFieldEditText.Hint = this.GetString(Resource.String.hint_query);

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

        using (var session = this.Prefs.Session)
        {
          var response = await session.ReadItemAsync(request);
          
          if (response.ResultCount == 0)
          {
            DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, Resource.String.text_no_item);
          }
          else
          {
            this.PopulateItemsList(response);
          }
        }

        this.SetProgressBarIndeterminateVisibility(false);
      }
      catch (Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}