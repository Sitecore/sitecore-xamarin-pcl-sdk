namespace Touch.Server.Android
{
  using System.Diagnostics;

  class AdbCommand
  {
    public string AdbExeFile { get; set; }
    public string AdbCommandValue { get; set; }
    public int ExitCode { get; set; }

    public AdbCommand(string adbCommandValue, string adbExeFile)
    {
      this.AdbCommandValue = adbCommandValue;
      this.AdbExeFile = adbExeFile;
      this.ExitCode = -1;
    }

    public void Execute()
    {
      var info = new ProcessStartInfo
      {
        FileName = this.AdbExeFile,
        UseShellExecute = true,
        Arguments = this.AdbCommandValue,
      };

      using (var proc = new Process())
      {
        proc.StartInfo = info;

        proc.Start();
        proc.WaitForExit();
        
        this.ExitCode = proc.ExitCode;
      }
    }
  }
}
