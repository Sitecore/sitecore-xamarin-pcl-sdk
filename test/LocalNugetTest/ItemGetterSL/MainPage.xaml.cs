using System;

namespace ItemGetterSL
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var endpoint = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:722", "admin", "b",
                    "/sitecore/shell");
                var defaultSource = ItemSource.DefaultSource();
                var session = new ScApiSession(endpoint, defaultSource);

                var request =
                    ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
                        .Payload(PayloadType.Content)
                        .Build();

                ScItemsResponse items = await session.ReadItemAsync(request);
                string fieldText = items.Items[0].FieldWithName("Text").RawValue;

                this.FieldValueTextBox.Text = fieldText;
            }
            catch (Exception ex)
            {
                this.FieldValueTextBox.Text = ex.Message;
            }
        }
    }
}
