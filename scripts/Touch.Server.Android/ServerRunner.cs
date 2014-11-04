namespace Touch.Server
{
  using System;
  using System.Net;
  using System.Threading;

  class ServerRunner
  {
    private static readonly CommandLineOptions Options = new CommandLineOptions();
    private static SimpleConnectionListener _connectionListener;
    private static AdbCommand _adbCommand;

    public static int Main(string[] args)
    {
      var returnCode = -1;

      if (CommandLine.Parser.Default.ParseArguments(args, Options))
      {
        if (!string.IsNullOrWhiteSpace(Options.AdbCommand))
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
          _adbCommand = new AdbCommand(Options.AdbCommand, "adb.exe");
          _adbCommand.Execute();
        }
        finally
        {
          adbHandle.Set();
        }
      };
      handles[1] = adbHandle;
      ThreadPool.QueueUserWorkItem(x => adbAction());

      WaitHandle.WaitAll(handles);

      if (_connectionListener != null && _adbCommand != null)
      {
        if (_connectionListener.ExitCode == 1 && _adbCommand.ExitCode == 1)
        {
          return 1;
        }
      }

      return 0;
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
        LogMessage("Failed to parse ip addres. Using : " + IPAddress.Any);
        listener.Address = IPAddress.Any;
      }
      else
      {
        listener.Address = ip;
      }

      ushort p;
      if (UInt16.TryParse(Options.Port, out p))
      {
        LogMessage(string.Format("Unable to parse port : {0}", Options.Port));
        listener.Port = p;
      }

      listener.LogPath = Options.LogFilePath ?? ".";
      listener.LogFile = Options.LogFileName;
      listener.AutoExit = Options.AutoExit;

      return listener;
    }
  }
}
