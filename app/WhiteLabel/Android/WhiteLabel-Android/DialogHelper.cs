namespace WhiteLabelAndroid
{
    using Android.App;
    using Android.Content;

    public class DialogHelper
    {
        public static void ShowSimpleDialog(Context context, int title, string message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetTitle(title);
            builder.SetMessage(message);

            AlertDialog dialog = builder.Create();
            dialog.SetButton("OK", (sender, args) => dialog.Dismiss());
            dialog.Show();
        }
    }
}