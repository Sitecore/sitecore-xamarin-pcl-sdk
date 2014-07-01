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
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByIdActivity : Activity, AdapterView.IOnItemClickListener
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
      this.prefs = Prefs.From(this);

      this.Title = this.GetString(Resource.String.text_get_item_by_id);

      this.SetContentView(Resource.Layout.SimpleItemLayout);

      this.InitViews();
    }

    private void InitViews()
    {
      this.payloadRadioGroup = this.FindViewById<RadioGroup>(Resource.Id.group_payload_type);

      this.itemNameTextView = this.FindViewById<TextView>(Resource.Id.item_name);

      this.fieldsListView = this.FindViewById<ListView>(Resource.Id.fields_list);
      this.fieldsListView.OnItemClickListener = this;

      var label = this.FindViewById<TextView>(Resource.Id.label);
      label.Text = "Type Item Id:";

      var itemIdField = this.FindViewById<EditText>(Resource.Id.field_item);
      itemIdField.Hint = "Item Id";

      var getItemChildrenButton = this.FindViewById<Button>(Resource.Id.button_get_children);
      getItemChildrenButton.Visibility = ViewStates.Visible;

      var getItemButton = this.FindViewById<Button>(Resource.Id.button_get_item);
      getItemButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(itemIdField.Text))
        {
          DialogHelper.ShowSimpleDialog(this, "Error", "Item Id cannot be empty");
          return;
        }

        this.PerformGetItemRequest(itemIdField.Text);
      };
    }

    private PayloadType GetSelectedPayload()
    {
      switch (this.payloadRadioGroup.CheckedRadioButtonId)
      {
        case Resource.Id.payload_min:
          return PayloadType.Min;
        case Resource.Id.payload_content:
          return PayloadType.Content;
        case Resource.Id.payload_full:
          return PayloadType.Full;
        default: return PayloadType.Min;
      }
    }

    private async void PerformGetItemRequest(string id)
    {
      try
      {
        ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(id).Payload(this.GetSelectedPayload()).Build();

        ScItemsResponse response = await session.ReadItemAsync(request);

        if (response.ResultCount > 0)
        {
          this.item = response.Items[0];
          this.itemNameTextView.Text = this.item.DisplayName;

          this.PopulateList();
        }
        else
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, "Item doesn't exist");
        }
      }
      catch (Exception exception)
      {
        DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, "Erorr :" + exception.Message);
      }
    }

    private void PopulateList()
    {
      var items = new string[this.item.Fields.Count];
      foreach (IField field in this.item.Fields)
      {
        items[this.item.Fields.IndexOf(field)] = field.Name;
      }

      this.fieldsListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
    }
  }
}