using System;
using MonoTouch.Foundation;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.SessionSettings;

namespace WhiteLabeliOS
{
  public class InstanceSettings
  {
    private string instanceUrl;
    private string instanceLogin;
    private string instancePassword;
    private string instanceSite;
    private string instanceDataBase;
    private string instanceLanguage;

    public InstanceSettings ()
    {
      this.ReadValuesFromStorage ();
    }

    private void ReadValuesFromStorage ()
    {
      NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
      this.instanceUrl    = userDefaults.StringForKey ("instanceUrl");
      this.instanceLogin    = userDefaults.StringForKey ("instanceLogin");
      this.instancePassword   = userDefaults.StringForKey ("instancePassword");
      this.instanceSite     = userDefaults.StringForKey ("instanceSite");
      this.instanceDataBase   = userDefaults.StringForKey ("instanceDataBase");
      this.instanceLanguage   = userDefaults.StringForKey ("instanceLanguage");
    }

    public ScApiSession GetSession()
    {
      SessionConfig config = new SessionConfig (this.instanceUrl, this.instanceLogin, this.instancePassword, this.instanceSite);

      string db;
      if (!string.IsNullOrEmpty(this.instanceDataBase))
      {
        db = this.instanceDataBase;
      }
      else
      {
        db = ItemSource.DefaultDatabase;
      }

      string language;
      if (!string.IsNullOrEmpty(this.instanceLanguage))
      {
        language = this.instanceLanguage;
      }
      else
      {
        language = ItemSource.DefaultLanguage;
      }

      ItemSource defaultSource = new ItemSource( db, language, null);

      return new ScApiSession (config, defaultSource);
    }

    private void SaveValueToStorage(string value, string key)
    {
      NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
      userDefaults.SetString (value, key);
      userDefaults.Synchronize ();
    }

    public string InstanceUrl   
    { 
      get
      { 
        #if DEBUG
        if (instanceUrl == null) 
        {
          instanceUrl = "http://mobiledev1ua1.dk.sitecore.net:722/";
        }
        #endif
        return instanceUrl;
      }
      set
      { 
        this.instanceUrl = value;
        this.SaveValueToStorage (value, "instanceUrl");
      }
    }

    public string InstanceLogin   
    { 
      get
      { 
        #if DEBUG
        if (instanceLogin == null) 
        {
          instanceLogin = "admin";
        }
        #endif
        return instanceLogin;
      }
      set
      { 
        this.instanceLogin = value;
        this.SaveValueToStorage (value, "instanceLogin");
      } 
    }

    public string InstancePassword  
    { 
      get
      { 
        #if DEBUG
        if (instancePassword == null) 
        {
          instancePassword = "b";
        }
        #endif
        return instancePassword;
      } 
      set
      { 
        //TODO: @igk keychain?
        this.instancePassword = value;
        this.SaveValueToStorage (value, "instancePassword");
      } 
    }

    public string InstanceSite    
    { 
      get
      { 
        #if DEBUG
        if (instanceSite == null) 
        {
          instanceSite = "/sitecore/shell";
        }
        #endif
        return instanceSite;
      }
      set
      { 
        this.instanceSite = value;
        this.SaveValueToStorage (value, "instanceSite");
      } 
    }

    public string InstanceDataBase  
    { 
      get
      { 
        #if DEBUG
        if (instanceDataBase == null) 
        {
          instanceDataBase = "web";
        }
        #endif
        return instanceDataBase;
      }
      set
      { 
        this.instanceDataBase = value;
        this.SaveValueToStorage (value, "instanceDataBase");
      } 
    }

    public string InstanceLanguage  
    { 
      get
      { 
        #if DEBUG
        if (instanceLanguage == null) 
        {
          instanceLanguage = "en";
        }
        #endif
        return instanceLanguage;
      }
      set
      { 
        this.instanceLanguage = value;
        this.SaveValueToStorage (value, "instanceLanguage");
      } 
    }
  }
}
