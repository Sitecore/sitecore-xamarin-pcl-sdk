namespace FormsApp
{
    using Xamarin.Forms;

    public class App
    {
        public static Page GetMainPage()
        {
            var useButton = new Button
            {
                Text = "Use",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };

            var settingsPage = new ContentPage
            {
                Title = "Settings",
                Content = new StackLayout
                {
                    Padding = 10,
                    Spacing = 20,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    Children = 
                    {
                        new Label
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            Text  = "Settings"
                        },
                        new Entry
                        {
                            Placeholder = "Instance URL",
                            Text = "http://mobiledev1ua1.dk.sitecore.net:722/"
                        },
                        new Label
                        {
                          Text  = ""
                        },
                        new Entry
                        {
                            Placeholder = "Username",
                            Text = "extranet\\creatorex"
                        },
                        new Entry
                        {
                            Placeholder = "Password", 
                            IsPassword = true,
                            Text = "creatorex"
                        },
                        new Entry
                        {
                            Placeholder = "Site"
                        },
                        new Entry
                        {
                            Placeholder = "Database",
                            Text = "web"
                        },
                        useButton
                    }
                }
            };

            return settingsPage;
        }
    }
}



