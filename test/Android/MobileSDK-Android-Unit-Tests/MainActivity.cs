namespace MobileSDKAndroidTests
{
  using Android.App;

  [Activity(Label = "MobileSDK-Android-Unit-Tests", MainLauncher = true)]
  public class MainActivity : ConfigurableTestActivity
  {
    protected override bool IsAutomated
    {
      get
      {
        return true;
      }
    }
  }
}

