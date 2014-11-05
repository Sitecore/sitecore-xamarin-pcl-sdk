namespace Touch.Server.Android
{
  using System.Diagnostics;

  class AdbCommand
  {
    private const string AdbExeName = "adb.exe";

    public string AdbExeFile { get; set; }
    public string AdbCommandValue { get; set; }
    public int ExitCode { get; set; }

    public AdbCommand(string adbCommandValue, string adbExeFile)
    {
      this.AdbCommandValue = adbCommandValue;
      this.AdbExeFile = this.PrepareAdbExeFilePath(adbExeFile);
      this.ExitCode = -1;
    }

    private string PrepareAdbExeFilePath(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
      {
        return AdbExeName;
      }

      if (path.Contains("adb"))
      {
        return path;
      }

      if (!path.EndsWith("\\"))
      {
        path += "\\";
      }

      return path + AdbExeName;
    }

    public void Execute()
    {
      ServerRunner.LogMessage("Executing adb command\n Command : {0}\n AdbExeFilePath : {1}", this.AdbCommandValue, this.AdbExeFile);

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
        
        ServerRunner.LogMessage("Finished with adb command (return code : {0})", proc.ExitCode);
        this.ExitCode = proc.ExitCode;
      }
    }
  }
}
