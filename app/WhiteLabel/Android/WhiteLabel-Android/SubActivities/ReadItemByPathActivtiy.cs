namespace WhiteLabelAndroid.SubActivities
{
    using System;
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;

    [Activity]
    public class ReadItemByPathActivtiy : Activity
    {
        private Prefs prefs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.prefs = Prefs.From(this);

            this.Title = this.GetString(Resource.String.text_get_item_by_path);

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
            try
            {
                ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

                ItemWebApiRequestBuilder requestBuilder = new ItemWebApiRequestBuilder();
                var request = requestBuilder.RequestWithPath(path).Build();

                ScItemsResponse response = await session.ReadItemByPathAsync(request);

                var message = response.ResultCount > 0 ? string.Format("item title is \"{0}\"", response.Items[0].DisplayName) : "Item doesn't exist";

                DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, message);
            }
            catch (Exception exception)
            {
                DialogHelper.ShowSimpleDialog(this, Resource.String.text_item_received, "Erorr :" + exception.Message);
            }
        }
    }
}