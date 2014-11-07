
namespace Touch.Server.Android
{
  using CommandLine;
  using CommandLine.Text;

  class CommandLineOptions
  {
    [Option("ip", HelpText = "IP address to listen (default: Any)")]
    public string IpAddress { get; set; }

    [Option("port", HelpText = "TCP port to listen (default: Any)")]
    public string Port { get; set; }

    [Option("logpath", HelpText = "Path to save the log files (default: .)")]
    public string LogFilePath { get; set; }

    [Option("logfile", HelpText = "Filename to save the log to (default: automatically generated)")]
    public string LogFileName { get; set; }

    [Option("autoexit", DefaultValue = false, HelpText = "Exit the server once a test run has completed (default: false)")]
    public bool AutoExit { get; set; }

    [Option("activity", HelpText = "Fully qualified activity name that will be launched after server starts.(exmaple : " +
                                   "package_name/namespace.MainActivity)")]
    public string Activity { get; set; }

    [Option("adbPath", HelpText = "Path to adb.exe (default: will use adb.exe from environment)")]
    public string AdbPath { get; set; }

    [Option('v', null, HelpText = "Print details during execution.")]
    public bool Verbose { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      return HelpText.AutoBuild(this);
    }
  }
}
