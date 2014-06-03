namespace FormsApp
{
    using Xamarin.Forms;

    public class MainPage : ContentPage
    {
        private static Config config = new Config();

        public MainPage()
        {
            this.Title = "Main";
            this.Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Text = "Main"
                    }
            }
            };

            // BUG: Android doesn't support the icon being null
            var tbi = new ToolbarItem("+", "ic_lock_power_off", () =>
                {
                    var settings = new SettingsPage(config);
                    Navigation.PushModalAsync(settings);
                }, 0, 0);

            this.ToolbarItems.Add(tbi);
        }
    }
}
