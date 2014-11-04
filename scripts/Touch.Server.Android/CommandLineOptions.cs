
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

    [Option("adbCommand", HelpText = "Adb coomand to run with server simultaneously")]
    public string AdbCommand { get; set; }

    [Option('v', null, HelpText = "Print details during execution.")]
    public bool Verbose { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      return HelpText.AutoBuild(this);
    }
  }
}
