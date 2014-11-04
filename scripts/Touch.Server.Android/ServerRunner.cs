namespace Touch.Server
{
  using System;
  using System.Net;

  class ServerRunner
  {
    private static readonly CommandLineOptions options = new CommandLineOptions();

    public static int Main(string[] args)
    {
      if (CommandLine.Parser.Default.ParseArguments(args, options))
      {
        var listener = PrepareListener();
        listener.Initialize();
        listener.Start();

        //        TODO: call methods
      }
      else
      {
        Console.WriteLine("Failed to parse oprions");
        return 0;
      }

      //
      //      var p = new Process
      //      {
      //        StartInfo =
      //        {
      ////          adb shell am start -a android.intent.action.MAIN -n MobileSDK_Android_Unit_Tests.MobileSDK_Android_Unit_Tests/mobilesdkandroidunittests.MainActivity
      //          FileName = "adb.exe",
      //          Arguments = "shell am start -a android.intent.action.MAIN -n MobileSDK_Android_Unit_Tests.MobileSDK_Android_Unit_Tests/mobilesdkandroidunittests.MainActivity",
      //          UseShellExecute = false,
      //          RedirectStandardOutput = true
      //        }
      //      };
      //      p.Start();
      //
      //      string output = p.StandardOutput.ReadToEnd();
      //      p.WaitForExit();
      //
      //      Console.WriteLine("Output:");
      //      Console.WriteLine(output);

      return 1;
    }

    public static void LogMessage(string message)
    {
      if (options != null && options.Verbose)
      {
        Console.WriteLine(message);
      }
    }

    public static void LogMessage(string message, params object[] objects)
    {
      if (options != null && options.Verbose)
      {
        Console.WriteLine(message, objects);
      }
    }

    private static SimpleConnectionListener PrepareListener()
    {
      var listener = new SimpleConnectionListener();

      IPAddress ip;
      if (String.IsNullOrEmpty(options.IpAddress) || !IPAddress.TryParse(options.IpAddress, out ip))
      {
        LogMessage("Failed to parse ip addres. Using : " + IPAddress.Any);
        listener.Address = IPAddress.Any;
      }
      else
      {
        listener.Address = ip;
      }

      ushort p;
      if (UInt16.TryParse(options.Port, out p))
      {
        LogMessage(string.Format("Unable to parse port : {0}", options.Port));
        listener.Port = p;
      }

      listener.LogPath = options.LogFilePath ?? ".";
      listener.LogFile = options.LogFileName;
      listener.AutoExit = options.AutoExit;

      return listener;
    }
  }
}
