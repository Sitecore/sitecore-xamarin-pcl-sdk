
namespace WhiteLabeliOS
{
  using System;
  using Foundation;

  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.PasswordProvider.iOS;

  public class InstanceSettings
  {
    private string instanceUrl;
    private string instanceLogin;
    private string instancePassword;
    private string instanceSite;
    private string instanceDataBase;
    private string instanceLanguage;

    public void Dispose()
    {
      // IDLE
    }

    public InstanceSettings()
    {
      this.ReadValuesFromStorage();
    }

    private void ReadValuesFromStorage()
    {
      NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
      this.instanceUrl            = userDefaults.StringForKey("instanceUrl"     );
      this.instanceLogin          = userDefaults.StringForKey("instanceLogin"   );
      this.instancePassword       = userDefaults.StringForKey("instancePassword");
      this.instanceSite           = userDefaults.StringForKey("instanceSite"    );
      this.instanceDataBase       = userDefaults.StringForKey("instanceDataBase");
      this.instanceLanguage       = userDefaults.StringForKey("instanceLanguage");
    }

    public ISitecoreSSCSession GetSession()
    {
      using 
      (
        var credentials = 
          new SecureStringPasswordProvider(
            this.instanceLogin, 
            this.instancePassword)
      )
      {
        var result = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.instanceUrl)
                                              .Credentials(credentials)
                                              .DefaultDatabase(this.instanceDataBase)
                                              .DefaultLanguage(this.instanceLanguage)
                                              .Site("sitecore")
                                              .BuildSession();

        return result;
      }
    }

    public ISitecoreSSCSession GetAnonymousSession()
    {
      var result = SitecoreSSCSessionBuilder.AnonymousSessionWithHost(this.instanceUrl)
                                            .DefaultDatabase(this.instanceDataBase)
                                            .DefaultLanguage(this.instanceLanguage)
                                            .Site("sitecore")
                                            .BuildSession();

        return result;
    }

    private void SaveValueToStorage(string value, string key)
    {
      NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
      userDefaults.SetString(value, key);
      userDefaults.Synchronize();
    }

    public string InstanceUrl   
    { 
      get
      { 
        #if DEBUG
        if (this.instanceUrl == null) 
        {
          this.instanceUrl = "http://cms80u2.test24dk1.dk.sitecore.net/";
        }
        #endif
        return this.instanceUrl;
      }
      set
      { 
        this.instanceUrl = value;
        this.SaveValueToStorage(value, "instanceUrl");
      }
    }

    public string InstanceLogin   
    { 
      get
      { 
        #if DEBUG
        if (this.instanceLogin == null) 
        {
          this.instanceLogin = "admin";
        }
        #endif
        return this.instanceLogin;
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
        if (this.instancePassword == null) 
        {
          this.instancePassword = "b";
        }
        #endif
        return this.instancePassword;
      } 
      set
      { 
        this.instancePassword = value;
        this.SaveValueToStorage (value, "instancePassword");
      } 
    }

    public string InstanceSite    
    { 
      get
      { 
        #if DEBUG
        if (this.instanceSite == null) 
        {
          this.instanceSite = "sitecore";
        }
        #endif
        return this.instanceSite;
      }
      set
      { 
        this.instanceSite = value;
        this.SaveValueToStorage(value, "instanceSite");
      } 
    }

    public string InstanceDataBase  
    { 
      get
      { 
        #if DEBUG
        if (this.instanceDataBase == null) 
        {
          this.instanceDataBase = "web";
        }
        #endif
        return this.instanceDataBase;
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
        if (this.instanceLanguage == null) 
        {
          this.instanceLanguage = "en";
        }
        #endif
        return this.instanceLanguage;
      }
      set
      { 
        this.instanceLanguage = value;
        this.SaveValueToStorage (value, "instanceLanguage");
      } 
    }
  }
}
