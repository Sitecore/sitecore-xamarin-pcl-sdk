namespace MobileSDKAndroidTests
{
  using System.IO;

  using System.Reflection;
  using Android.App;
  using Android.OS;
  using Xamarin.Android.NUnitLite;

  using System;
  using Android.Util;

  [Activity(Label = "MobileSDK-Android-Unit-Tests", MainLauncher = true)]
  public class MainActivity : ConfigurableTestActivity
  {
    private const string TouchServerHost = "10.45.110.119";
    private const int TouchServerPort = 55555;

    protected override bool IsAutomated
    {
      get
      {
        return true;
      }
    }

    protected override TextWriter TargetTextWriter
    {
      get
      {
        var message = string.Format("[{0}] Sending results to {1}:{2}", DateTime.Now, TouchServerHost, TouchServerPort);

        Log.Debug(this.GetType().Name, message);
        try
        {
          return new TcpTextWriter(hostName: TouchServerHost, port: TouchServerPort);
        }
        catch (System.Exception exception)
        {
          var debugMessage = string.Format("Failed to connect to {0}:{1}.\n{2}", TouchServerHost, TouchServerPort,
            exception);

          Log.Debug(GetType().Name, debugMessage);
          throw;
        }
      }
    }
  }
}

