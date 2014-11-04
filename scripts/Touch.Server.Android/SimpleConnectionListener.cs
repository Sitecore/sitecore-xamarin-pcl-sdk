namespace Touch.Server
{
  using System;
  using System.IO;
  using System.Net;
  using System.Net.Sockets;
  using System.Threading;

  class SimpleConnectionListener
  {
    static byte[] buffer = new byte[16 * 1024];

    TcpListener server;
    ManualResetEvent stopped = new ManualResetEvent(false);

    public IPAddress Address { get; set; }
    public int Port { get; set; }
    public string LogPath { get; set; }
    public string LogFile { get; set; }
    public bool AutoExit { get; set; }

    public void Cancel()
    {
      try
      {
        // wait a second just in case more data arrives.
        if (!stopped.WaitOne(TimeSpan.FromSeconds(1)))
          server.Stop();
      }
      catch
      {
        // We might have stopped already, so just swallow any exceptions.
      }
    }

    public void Initialize()
    {
      ServerRunner.LogMessage("User input for endpoint: {0}:{1}", Address, Port);

      server = new TcpListener(Address, Port);
      server.Start();

      if (Port == 0)
      {
        Port = ((IPEndPoint)server.LocalEndpoint).Port;
      }

      ServerRunner.LogMessage("Touch Server listening on: {0}:{1}", Address, Port);
    }

    public int Start()
    {
      bool processed;

      try
      {

        do
        {
          using (TcpClient client = server.AcceptTcpClient())
          {
            processed = Processing(client);
          }
        } while (!AutoExit || !processed);
      }
      catch (Exception e)
      {
        Console.WriteLine("[{0}] : {1}", DateTime.Now, e);
        return 1;
      }
      finally
      {
        try
        {
          server.Stop();
        }
        finally
        {
          stopped.Set();
        }
      }

      return 0;
    }

    public bool Processing(TcpClient client)
	{
		string logfile = Path.Combine (LogPath, LogFile ?? DateTime.UtcNow.Ticks.ToString () + ".log");
		string remote = client.Client.RemoteEndPoint.ToString ();
		Console.WriteLine ("Connection from {0} saving logs to {1}", remote, logfile);

		using (FileStream fs = File.OpenWrite (logfile)) {
			// a few extra bits of data only available from this side
			string header = String.Format ("[Local Date/Time:\t{1}]{0}[Remote Address:\t{2}]{0}", 
				Environment.NewLine, DateTime.Now, remote);
			Console.WriteLine(header);

/*
			byte[] array = Encoding.UTF8.GetBytes (header);
			fs.Write (array, 0, array.Length);
			fs.Flush ();
*/
			// now simply copy what we receive
			int i;
			int total = 0;
			NetworkStream stream = client.GetStream ();
			
      while ((i = stream.Read (buffer, 0, buffer.Length)) != 0) {
				fs.Write (buffer, 0, i);
				fs.Flush ();
				total += i;
			}
			
			if (total < 16) {
				// This wasn't a test run, but a connection from the app (on device) to find
				// the ip address we're reachable on.
				return false;
			}
		}
		
		return true;
	}
  }
}
