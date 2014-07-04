namespace WhiteLabelAndroid
{
  using Android.App;
  using Android.Content;

  public class DialogHelper
  {
    public static void ShowSimpleDialog(Context context, int titleId, int messageId)
    {
      var titleString = context.GetString(titleId);
      var message = context.GetString(messageId);
      ShowSimpleDialog(context, titleString, message);
    }

    public static void ShowSimpleDialog(Context context, string title, string message)
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