namespace ItemGetterSL
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Sitecore.MobileSDK.API;
    using Sitecore.MobileSDK.API.Items;
    using Sitecore.MobileSDK.API.Request.Parameters;


    public partial class MainPage : UserControl
    {
        class InsecureDemoCredentials : IWebApiCredentials
        {
            public string Username
            {
                get
                {
                    return "admin";
                }
            }

            public string Password
            {
                get
                {
                    return "b";
                }
            }

            public IWebApiCredentials CredentialsShallowCopy()
            {
                return this;
            }

            public void Dispose()
            {
                //IDLE
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var demoCredentials = new InsecureDemoCredentials())
            {
                try
                {
                    var session =
                        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://mobiledev1ua1.dk.sitecore.net:7220")
                                                    .Credentials(demoCredentials)
                                                    .Site("/sitecore/shell")
                                                    .DefaultDatabase("web")
                                                    .DefaultLanguage("en")
                                                    .MediaLibraryRoot("/sitecore/media library")
                                                    .MediaPrefix("~/media/")
                                                    .DefaultMediaResourceExtension("ashx")
                                                    .BuildReadonlySession();
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
}
