namespace WhiteLabelAndroid.SubActivities
{
    using System;
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;

    [Activity]
    public class ReadItemByIdActivity : Activity
    {
        private Prefs prefs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.prefs = Prefs.From(this);

            this.Title = this.GetString(Resource.String.text_get_item_by_id);

            this.SetContentView(Resource.Layout.SimpleItemLayout);

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
                    Toast.MakeText(this, "Item Id cannot be mepty", ToastLength.Short).Show();
                    return;
                }

                this.PerformGetItemRequest(itemIdField.Text);
            };
        }

        private async void PerformGetItemRequest(string id)
        {
            try
            {
                ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

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
            catch (Exception exception)
            {
                Toast.MakeText(this, "Erorr :" + exception.Message, ToastLength.Long).Show();
            }
        }
    }
}