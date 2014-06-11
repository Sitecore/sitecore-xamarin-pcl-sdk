namespace WhiteLabelAndroid.SubActivities
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;

    [Activity(Label = "GetItemByPathActivtiy")]
    public class GetItemByPathActivtiy : Activity
    {
        private Prefs prefs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.prefs = Prefs.From(this);

            this.SetContentView(Resource.Layout.SimpleItemLayout);

            var label = this.FindViewById<TextView>(Resource.Id.label);
            label.Text = "Type Item Path:";

            var itemIdField = this.FindViewById<EditText>(Resource.Id.field_item);
            itemIdField.Hint = "Item Path";

            var getItemButton = this.FindViewById<Button>(Resource.Id.button_get_item);
            getItemButton.Click += (sender, args) =>
            {
                if (string.IsNullOrEmpty(itemIdField.Text))
                {
                    Toast.MakeText(this, "Item path cannot be mepty", ToastLength.Short).Show();
                    return;
                }

                this.PerformGetItemRequest(itemIdField.Text);
            };
        }

        private async void PerformGetItemRequest(string path)
        {
            ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

            ItemWebApiRequestBuilder requestBuilder = new ItemWebApiRequestBuilder();
            var request = requestBuilder.RequestWithPath(path).Build();

            ScItemsResponse response = await session.ReadItemByPathAsync(request);

            if (response.ResultCount > 0)
            {
                Toast.MakeText(this, "Display name : " + response.Items[0].DisplayName, ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "No items with this path", ToastLength.Long).Show();
            }
        }
    }
}