namespace WhiteLabelAndroid.SubActivities
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;

    [Activity(Label = "GetItemByIdActivity")]
    public class GetItemByIdActivity : Activity
    {
        private EditText itemIdField;
        private Prefs prefs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.prefs = Prefs.From(this);

            this.SetContentView(Resource.Layout.SimpleItemLayout);

            var label = this.FindViewById<TextView>(Resource.Id.label);
            label.Text = "Type Item Id:";

            this.itemIdField = this.FindViewById<EditText>(Resource.Id.field_item_id);
            this.itemIdField.Hint = "Item Id";

            var getItemButton = this.FindViewById<Button>(Resource.Id.button_get_item);

            getItemButton.Click += (sender, args) =>
            {
                if (string.IsNullOrEmpty(itemIdField.Text))
                {
                    return;
                }
                
                this.PerformGetItemRequest(itemIdField.Text);
            };
        }

        private async void PerformGetItemRequest(string id)
        {
            ScApiSession session = new ScApiSession(this.PrepareSessionConfig(), this.PrepareItemSource());

            ItemWebApiRequestBuilder requestBuilder = new ItemWebApiRequestBuilder();
            var request = requestBuilder.RequestWithId(id).Build();

            ScItemsResponse response = await session.ReadItemByIdAsync(request);

            if (response.ResultCount > 0)
            {
                Toast.MakeText(this, "Display name : " + response.Items[0].DisplayName, ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "No items with this Id", ToastLength.Long).Show();
            }
        }

        private SessionConfig PrepareSessionConfig()
        {
            return new SessionConfig(this.prefs.GetInstanceUrl(), this.prefs.GetLogin(), this.prefs.GetPassword(), this.prefs.GetSite());
        }

        private ItemSource PrepareItemSource()
        {
            return new ItemSource(this.prefs.GetDatabase(), "en");
        }
    }
}