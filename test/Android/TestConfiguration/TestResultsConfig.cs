using Android.Util;

namespace MobileSDKAndroidTests
{
  public class TestResultsConfig
  {
    private static readonly string Tag = typeof(TestResultsConfig).Name;
    public const bool IsRemote = false;
    public const bool IsAutomated = true;

    public const string RemoteServerIp = "10.38.10.175";
    public const int RemoteServerPort = 8888;

    public const string TestsResultsFormat = "plain_text";

    public static void PrintConfig()
    {
      Log.Info(Tag, "IsRemote : {0}", IsRemote);
      Log.Info(Tag, "IsAutomated : {0}", IsAutomated);
      Log.Info(Tag, "RemoteServerIp : {0}", RemoteServerIp);
      Log.Info(Tag, "RemoteServerPort : {0}", RemoteServerPort);
      Log.Info(Tag, "TestsResultsFormat : {0}", TestsResultsFormat);
    }
  }
}
