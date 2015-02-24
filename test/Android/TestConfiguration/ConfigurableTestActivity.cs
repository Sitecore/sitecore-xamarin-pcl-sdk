namespace MobileSDKAndroidTests
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using Android.OS;
  using Android.Util;
  using NUnit.Framework.Internal;
  using NUnitLite.Runner;
  using Xamarin.Android.NUnitLite;

  public abstract class ConfigurableTestActivity : TestSuiteActivity
  {
    private readonly string tag = typeof(ConfigurableTestActivity).Name;
    private DateTime testsStartTime;

    protected virtual bool IsAutomated
    {
      get
      {
        return TestResultsConfig.IsAutomated;
      }
    }

    protected TextWriter TextWriter
    {
      get
      {
        if (TestResultsConfig.IsRemote && TestResultsConfig.TestsResultsFormat.Equals("plain_text"))
        {
          return this.NetworkWriter;
        }
        else
        {
          return Console.Out;
        }
      }
    }

    protected Assembly TestsAssembly
    {
      get
      {
        return Assembly.GetExecutingAssembly();
      }
    }

    protected override void OnCreate(Bundle bundle)
    {
      this.SetTextWriterForAndroidTestRunner();

      this.AddTest(TestsAssembly);

      base.OnCreate(bundle);
    }

    protected override void OnResume()
    {
      base.OnResume();

      if (IsAutomated)
      {
        this.RunTestsAndPublish();
      }
    }

    public void RunTestsAndPublish()
    {
      var testResults = this.RunTests();

      this.PublishResults(testResults);
    }

    protected virtual void PublishResults(TestResult testResults)
    {
      TestResultsConfig.PrintConfig();

      Log.Info(this.tag, "Publishing results : " + DateTime.Now +
                    "\nTotal count : {0}, Failed : {1}",
        testResults.AssertCount,
        testResults.FailCount);

      if (TestResultsConfig.IsRemote)
      {
        switch (TestResultsConfig.TestsResultsFormat)
        {
          case "plain_text":
            //          We already published test results because in this case we publish each test results separately. See SetTextWriterForAndroidTestRunner() method.
            return;
          case "nunit2":
            var nunit2Writer = new NUnit2XmlOutputWriter(this.testsStartTime);
            var tcpwriter = this.NetworkWriter;

            nunit2Writer.WriteResultFile(testResults, tcpwriter);
            tcpwriter.Close();

            Log.Info(this.tag, "Published tests results in nunit2 format");
            return;
          case "nunit3":
            var nunit3Writer = new NUnit3XmlOutputWriter(this.testsStartTime);
            var newtworkWriter = this.NetworkWriter;

            nunit3Writer.WriteResultFile(testResults, newtworkWriter);
            newtworkWriter.Close();

            Log.Info(this.tag, "Published tests results in nunit3 format");
            return;
        }
      }
      else
      {
        //      If we don't need to send test results to remote server, just return.
        return;
      }
    }

    protected TestResult RunTests()
    {
      var runMethod = this.GetAndroidRunner().GetType().GetMethod("Run", BindingFlags.Public | BindingFlags.Instance);
      this.testsStartTime = DateTime.Now;

      Log.Info(this.tag, "Running tests: " + this.testsStartTime);
      var result = runMethod.Invoke(this.GetAndroidRunner(), new object[]
      {
        this.CurrentTest
      });

      return (TestResult)result;
    }

    private void SetTextWriterForAndroidTestRunner()
    {
      var value = this.GetAndroidRunner();

      var method = this.GetSetWriterMethod();
      method.Invoke(value, new object[] { this.TextWriter });
    }

    protected TextWriter NetworkWriter
    {
      get
      {
        var message = string.Format("[{0}] Sending results to {1}:{2}", DateTime.Now, TestResultsConfig.RemoteServerIp, TestResultsConfig.RemoteServerPort);

        Log.Debug(this.GetType().Name, message);
        try
        {
          return new TcpTextWriter(hostName: TestResultsConfig.RemoteServerIp, port: TestResultsConfig.RemoteServerPort);
        }
        catch (System.Exception exception)
        {
          var debugMessage = string.Format("Failed to connect to {0}:{1}.\n{2}", TestResultsConfig.RemoteServerIp, TestResultsConfig.RemoteServerPort, exception);

          Log.Debug(GetType().Name, debugMessage);
          throw;
        }
      }
    }

    private Test CurrentTest
    {
      get
      {
        var currentTestProperty = typeof(TestSuiteActivity).GetField("current_test", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        return (Test)currentTestProperty.GetValue(this);
      }
    }

    protected object GetAndroidRunner()
    {
      Assembly ass = Assembly.GetAssembly(typeof(TestSuiteActivity));
      Type type = ass.GetType("Xamarin.Android.NUnitLite.AndroidRunner");

      FieldInfo info = type.GetField("runner", BindingFlags.NonPublic | BindingFlags.Static);
      dynamic value = info.GetValue(null);

      return value;
    }

    private MethodInfo GetSetWriterMethod()
    {
      Assembly ass = Assembly.GetAssembly(typeof(TestSuiteActivity));
      Type type = ass.GetType("Xamarin.Android.NUnitLite.AndroidRunner");

      MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

      return methodInfos.FirstOrDefault(methodInfo => methodInfo.Name.Equals("set_Writer"));
    }
  }
}