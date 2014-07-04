namespace WhiteLabelAndroid.SubActivities
{
  using System;
  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Views.InputMethods;
  using Android.Widget;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByQueryActivtiy : Activity
  {
    private Prefs prefs;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.SimpleItemLayout);

      this.prefs = Prefs.From(this);
      this.Title = this.GetString(Resource.String.text_get_item_by_query);

      var label = this.FindViewById<TextView>(Resource.Id.label);
      label.Text = GetString(Resource.String.text_query_label);

      var queryField = this.FindViewById<EditText>(Resource.Id.field_item);
      queryField.Hint = GetString(Resource.String.hint_query);

      var payloadGroup = this.FindViewById<RadioGroup>(Resource.Id.group_payload_type);
      payloadGroup.Visibility = ViewStates.Gone;

      var getItemButton = this.FindViewById<Button>(Resource.Id.button_get_item);
      getItemButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(queryField.Text))
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_error,
            Resource.String.text_empty_query);
          return;
        }

        this.HideKeyboard(queryField);
        this.PerformGetItemRequest(queryField.Text);
      };
    }

    private void HideKeyboard(View view)
    {
      var inputMethodManager = this.GetSystemService(Context.InputMethodService) as InputMethodManager;
      inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
    }

    private async void PerformGetItemRequest(string query)
    {
      try
      {
        ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(query).Build();
        this.SetProgressBarIndeterminateVisibility(true);

        ScItemsResponse response = await session.ReadItemAsync(request);

        this.SetProgressBarIndeterminateVisibility(false);
        var message = response.ResultCount > 0 ?
          this.GetString(Resource.String.text_num_of_items_received, response.Items.Count)
          : GetString(Resource.String.text_no_item);

        DialogHelper.ShowSimpleDialog(this, GetString(Resource.String.text_item_received), message);
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