namespace MobileSDKAndroidTests
{
  using System;
  using System.IO;
  using System.Net.Sockets;
  using System.Text;

  public class TcpTextWriter : TextWriter
  {
    private readonly StreamWriter writer;

    private readonly TcpClient client;
    
    public override Encoding Encoding
    {
      get
      {
        return Encoding.UTF8;
      }
    }

    public string HostName { get; private set; }

    public int Port { get; private set; }

    public TcpTextWriter(string hostName, int port)
    {
      if (hostName == null)
      {
        throw new ArgumentNullException("hostName");
      }
      if (port < 0 || port > 65535)
      {
        throw new ArgumentException("port");
      }
      this.HostName = hostName;
      this.Port = port;
      this.client = new TcpClient(hostName, port);
      this.writer = new StreamWriter(this.client.GetStream());
    }

    public override void Close()
    {
      this.writer.Close();
    }

    protected override void Dispose(bool disposing)
    {
      this.writer.Dispose();
    }

    public override void Flush()
    {
      this.writer.Flush();
    }

    public override void Write(char[] buffer, int index, int count)
    {
      this.writer.Write(buffer, index, count);
    }

    public override void Write(string value)
    {
      this.writer.Write(value);
    }

    public override void Write(char value)
    {
      this.writer.Write(value);
    }

    public override void Write(char[] buffer)
    {
      this.writer.Write(buffer);
    }

    public override void WriteLine()
    {
      this.writer.WriteLine();
      this.writer.Flush();
    }
  }
}