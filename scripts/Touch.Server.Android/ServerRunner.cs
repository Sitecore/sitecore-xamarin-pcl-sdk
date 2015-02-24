namespace Touch.Server.Android
{
  using System;
  using System.Net;
  using System.Threading;

  class ServerRunner
  {
    private static readonly CommandLineOptions Options = new CommandLineOptions();
    private static SimpleConnectionListener _connectionListener;
    private static AdbActivtiyLaunchCommand _adbActivtiyLaunchCommand;

    public static int Main(string[] args)
    {
      var returnCode = -1;

      if (CommandLine.Parser.Default.ParseArguments(args, Options))
      {
        if (!string.IsNullOrWhiteSpace(Options.Activity))
        {
          returnCode = StartAdbAndListener();
        }
        else
        {
          StartListener();
          returnCode = _connectionListener.ExitCode;
        }
      }
      else
      {
        Console.WriteLine("Failed to parse options");
      }

      return returnCode;
    }

    private static void StartListener()
    {
      _connectionListener = PrepareListener();
      _connectionListener.Initialize();
      _connectionListener.Start();
    }

    private static int StartAdbAndListener()
    {
      var handles = new ManualResetEvent[2];

      var listenerHandle = new ManualResetEvent(false);
      Action listenerAction = () =>
      {
        try
        {
          StartListener();
        }
        finally
        {
          listenerHandle.Set();
        }
      };

      handles[0] = listenerHandle;
      ThreadPool.QueueUserWorkItem(x => listenerAction());

      var adbHandle = new ManualResetEvent(false);
      Action adbAction = () =>
      {
        try
        {
          _adbActivtiyLaunchCommand = new AdbActivtiyLaunchCommand(Options.Activity, Options.AdbPath);
          _adbActivtiyLaunchCommand.Execute();
        }
        finally
        {
          adbHandle.Set();
        }
      };
      handles[1] = adbHandle;
      ThreadPool.QueueUserWorkItem(x => adbAction());

      WaitHandle.WaitAll(handles);

      if (_connectionListener != null && _adbActivtiyLaunchCommand != null)
      {
        if (_connectionListener.ExitCode == 0 && _adbActivtiyLaunchCommand.ExitCode == 0)
        {
          return 0;
        }
      }

      return -1;
    }

    public static void LogMessage(string message)
    {
      if (Options != null && Options.Verbose)
      {
        Console.WriteLine(message);
      }
    }

    public static void LogMessage(string message, params object[] objects)
    {
      if (Options != null && Options.Verbose)
      {
        Console.WriteLine(message, objects);
      }
    }

    private static SimpleConnectionListener PrepareListener()
    {
      var listener = new SimpleConnectionListener();

      IPAddress ip;
      if (String.IsNullOrEmpty(Options.IpAddress) || !IPAddress.TryParse(Options.IpAddress, out ip))
      {
        LogMessage("Failed to parse ip address. Using : " + IPAddress.Any);
        listener.Address = IPAddress.Any;
      }
      else
      {
        listener.Address = ip;
      }

      ushort p;
      if (UInt16.TryParse(Options.Port, out p))
      {
        listener.Port = p;
      }
      else
      {
        Console.WriteLine("Failed to parse port. Will use dafult.");
      }

      listener.LogPath = Options.LogFilePath ?? ".";
      listener.LogFile = Options.LogFileName;
      listener.AutoExit = Options.AutoExit;

      return listener;
    }
  }
}
