namespace WhiteLabelAndroid.SubActivities
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;

    [Activity(Label = "GetItemByQueryActivtiy")]
    public class GetItemByQueryActivtiy : Activity
    {
        private Prefs prefs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.prefs = Prefs.From(this);

            this.SetContentView(Resource.Layout.SimpleItemLayout);

            var label = this.FindViewById<TextView>(Resource.Id.label);
            label.Text = "Type Sitecore Query:";

            var itemIdField = this.FindViewById<EditText>(Resource.Id.field_item);
            itemIdField.Hint = "Query";

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

        private async void PerformGetItemRequest(string query)
        {
            ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

            ItemWebApiRequestBuilder requestBuilder = new ItemWebApiRequestBuilder();
            var request = requestBuilder.RequestWithSitecoreQuery(query).Build();

            ScItemsResponse response = await session.ReadItemByQueryAsync(request);

            if (response.ResultCount > 0)
            {
                Toast.MakeText(this, "Display name : " + response.Items[0].DisplayName, ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "No items with this query", ToastLength.Long).Show();
            }
        }
    }
}