using Sitecore.MobileSDK.API;
using Sitecore.MobileSDK.API.Items;

namespace WhiteLabelAndroid.SubActivities
{
  using System;
  using System.Linq;
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class ReadItemByPathActivtiy : BaseReadItemActivity, AdapterView.IOnItemClickListener
  {
    private ListView fieldsListView;
    private TextView itemNameTextView;
    private RadioGroup payloadRadioGroup;

    private ISitecoreItem item;

    void AdapterView.IOnItemClickListener.OnItemClick(AdapterView parent, View view, int position, long id)
    {
      DialogHelper.ShowSimpleDialog(this, this.item.Fields.ElementAt(position).Name,
        this.item.Fields.ElementAt(position).RawValue);
    }

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      this.prefs = Prefs.From(this);
      this.Title = this.GetString(Resource.String.text_get_item_by_path);

      this.InitViews();
    }

    private void InitViews()
    {
      this.payloadRadioGroup = this.FindViewById<RadioGroup>(Resource.Id.group_payload_type);

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
    }

    private async void PerformGetItemRequest(string path)
    {
      try
      {
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(path)
          .Payload(this.GetSelectedPayload())
          .Build();
        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          ScItemsResponse response = await session.ReadItemAsync(request);

          this.SetProgressBarIndeterminateVisibility(false);
          if (response.ResultCount > 0)
          {
            this.item = response[0];
            this.itemNameTextView.Text = this.item.DisplayName;

            this.PopulateItemsList(response);
          }
          else
          {
            DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received,
              Resource.String.text_no_item);
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