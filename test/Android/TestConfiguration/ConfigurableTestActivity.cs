namespace MobileSDKAndroidTests
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using Android.OS;
  using Xamarin.Android.NUnitLite;

  public abstract class ConfigurableTestActivity : TestSuiteActivity
  {
    protected virtual bool IsAutomated
    {
      get
      {
        return false;
      }
    }

    protected virtual TextWriter TargetTextWriter 
    {
      get
      {
        return Console.Out;
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

      Intent.PutExtra("automated", this.IsAutomated);

      base.OnCreate(bundle);
    }

    private void SetTextWriterForAndroidTestRunner()
    {
      var value = this.getAndroidRunner();

      var method = this.GetSetWriterMethod();
      method.Invoke(value, new object[] { this.TargetTextWriter });
    }

    private object getAndroidRunner()
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