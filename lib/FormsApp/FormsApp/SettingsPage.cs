namespace FormsApp
{
    using Xamarin.Forms;

    public class SettingsPage : ContentPage
    {
        public SettingsPage(Config sessionConfig)
        {
             var instanceUrlField = new Entry
            {
                Placeholder = "Instance URL"
            };

            var useButton = new Button
            {
                Text = "Use",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("77D065")
            };

            this.Title = "Settings";
            this.Content = new StackLayout
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
                        Text = "Settings"
                    },
                    instanceUrlField,
                    new Label
                    {
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
            };

            this.BindingContext = sessionConfig;
            instanceUrlField.SetBinding(Entry.TextProperty, "InstanceUrl");
        }
    }
}
