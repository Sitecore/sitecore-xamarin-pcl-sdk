namespace FormsApp
{
    using Xamarin.Forms;

    public class App
    {
        public static Page GetMainPage()
        {
            var mainNav = new NavigationPage(new MainPage());
            return mainNav;
        }
    }
}
