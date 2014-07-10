namespace WhiteLabelAndroid.SubActivities
{
  using System;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Items.Fields;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByPathActivtiy : BaseActivity, AdapterView.IOnItemClickListener
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
      this.Title = this.GetString(Resource.String.text_get_item_by_path);

      this.InitViews();
    }

    private void InitViews()
    {
      this.payloadRadioGroup = this.FindViewById<RadioGroup>(Resource.Id.group_payload_type);

      this.itemNameTextView = this.FindViewById<TextView>(Resource.Id.item_name);

      this.fieldsListView = this.FindViewById<ListView>(Resource.Id.fields_list);
      this.fieldsListView.OnItemClickListener = this;

      var label = this.FindViewById<TextView>(Resource.Id.label);
      label.Text = GetString(Resource.String.text_path_label);

      var itemPathField = this.FindViewById<EditText>(Resource.Id.field_item);
      itemPathField.Hint = GetString(Resource.String.hint_item_path);

      var getItemButton = this.FindViewById<Button>(Resource.Id.button_get_item);
      getItemButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(itemPathField.Text))
        {
          DialogHelper.ShowSimpleDialog(this, GetString(Resource.String.text_error), GetString(Resource.String.text_empty_path));
          return;
        }

        this.HideKeyboard(itemPathField);
        this.PerformGetItemRequest(itemPathField.Text);
      };

      var getItemChildrenButton = this.FindViewById<Button>(Resource.Id.button_get_children);
      getItemChildrenButton.Visibility = ViewStates.Gone;
    }

    private async void PerformGetItemRequest(string path)
    {
      try
      {
        ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(path).Payload(this.GetSelectedPayload(this.payloadRadioGroup)).Build();
        this.SetProgressBarIndeterminateVisibility(true);

        ScItemsResponse response = await session.ReadItemAsync(request);

        this.SetProgressBarIndeterminateVisibility(false);
        if (response.ResultCount > 0)
        {
          this.item = response.Items[0];
          this.itemNameTextView.Text = this.item.DisplayName;

          this.PopulateFieldsList(this.fieldsListView, item.Fields);
        }
        else
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received,
            Resource.String.text_no_item);
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