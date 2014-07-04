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
  using Sitecore.MobileSDK.Items.Fields;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByIdActivity : BaseActivity, AdapterView.IOnItemClickListener
  {
    private ListView fieldsListView;
    private TextView itemNameTextView;
    private RadioGroup payloadRadioGroup;

    private ISitecoreItem item;
    private Prefs prefs;

    void AdapterView.IOnItemClickListener.OnItemClick(AdapterView parent, View view, int position, long id)
    {
      DialogHelper.ShowSimpleDialog(this, this.item.Fields[position].Name, this.item.Fields[position].RawValue);
    }

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.SimpleItemLayout);

      this.prefs = Prefs.From(this);
      this.Title = this.GetString(Resource.String.text_get_item_by_id);

      this.InitViews();
    }

    private void InitViews()
    {
      this.payloadRadioGroup = this.FindViewById<RadioGroup>(Resource.Id.group_payload_type);

      this.itemNameTextView = this.FindViewById<TextView>(Resource.Id.item_name);

      this.fieldsListView = this.FindViewById<ListView>(Resource.Id.fields_list);
      this.fieldsListView.OnItemClickListener = this;

      var label = this.FindViewById<TextView>(Resource.Id.label);
      label.Text = GetString(Resource.String.text_id_label);

      var itemIdField = this.FindViewById<EditText>(Resource.Id.field_item);
      itemIdField.Hint = GetString(Resource.String.hint_item_id);

      var getItemChildrenButton = this.FindViewById<Button>(Resource.Id.button_get_children);
      getItemChildrenButton.Visibility = ViewStates.Visible;

      var getItemButton = this.FindViewById<Button>(Resource.Id.button_get_item);
      getItemButton.Click += (sender, args) =>
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
        ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(id).Payload(this.GetSelectedPayload(this.payloadRadioGroup)).Build();
        this.SetProgressBarIndeterminateVisibility(true);

        ScItemsResponse response = await session.ReadItemAsync(request);

        this.SetProgressBarIndeterminateVisibility(false);
        if (response.ResultCount > 0)
        {
          this.item = response.Items[0];
          this.itemNameTextView.Text = this.item.DisplayName;

          this.PopulateFieldsList(fieldsListView, item.Fields);
        }
        else
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, Resource.String.text_no_item);
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