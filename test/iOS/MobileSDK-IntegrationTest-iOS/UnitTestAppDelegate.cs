﻿namespace MobileSDKIntegrationTestiOS
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;
  using MonoTouch.NUnit.UI;
  using MonoTouch.NUnit;


  // The UIApplicationDelegate for the application. This class is responsible for launching the
  // User Interface of the application, as well as listening (and optionally responding) to
  // application events from iOS.
  [Register ("UnitTestAppDelegate")]
  public partial class UnitTestAppDelegate : UIApplicationDelegate
  {
    // class-level declarations
    UIWindow window;
    TouchRunner runner;

    //
    // This method is invoked when the application has loaded and is ready to run. In this
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
      // create a new window instance based on the screen size
      this.window = new UIWindow(UIScreen.MainScreen.Bounds);
      this.runner = new TouchRunner(this.window);

      #if !DEBUG
      this.ConfigureRunnerForCI();
      #endif


      // register every tests included in the main application/assembly
      var thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
      this.runner.Add(thisAssembly);


      var viewControllerForTestRunner = this.runner.GetViewController();
      this.window.RootViewController = new UINavigationController(viewControllerForTestRunner);

      // make the window visible
      this.window.MakeKeyAndVisible();

      return true;
    }

    private void ConfigureRunnerForCI()
    {
      this.runner.AutoStart = true;
      this.runner.TerminateAfterExecution = true;

      var reportStream = new NUnitLite.Runner.NUnit2XmlOutputWriter(DateTime.Now);

      //      string host = "localhost";
      //      string host = "10.38.11.5"; // @adk mac-mini
      string host = "10.38.10.205"; // @darthvader.local

      var targetStreamOnBuildServer = new TcpTextWriter(host, 16391);

      runner.Writer = new NUnitOutputTextWriter(runner, targetStreamOnBuildServer, reportStream);
    }
  }
}

