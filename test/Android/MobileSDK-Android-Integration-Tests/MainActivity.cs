namespace MobileSDKAndroidTests
{
  using System;
  using System.IO;
  using Android.App;
  using Android.Util;
  using MobileSDKAndroidIntegrationTests;

  [Activity(Label = "MobileSDK-Android-Tests", MainLauncher = true)]
  public class MainActivity : ConfigurableTestActivity
  {
    private const string TouchServerHost = "10.38.10.175";
    private const int TouchServerPort = 8888;

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

