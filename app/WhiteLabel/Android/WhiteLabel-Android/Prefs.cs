namespace WhiteLabelAndroid
{
  using System.Diagnostics.CodeAnalysis;
  using Android.Content;
  using Android.Preferences;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  public class Prefs
  {
    private readonly ISharedPreferences prefs;
    private readonly Context context;

    private Prefs(Context context, ISharedPreferences sharedPreferences)
    {
      this.context = context;
      this.prefs = sharedPreferences;
    }

    #region Instance URL

    public string InstanceUrl
    {
      get
      {
        string instanceUrlKey = this.context.GetString(Resource.String.key_instance_url);
        string defaultInstanceUrl = this.context.GetString(Resource.String.text_default_instance_url);

        return this.GetString(instanceUrlKey, defaultInstanceUrl);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_instance_url), value);
      }
    }

    #endregion Instance URL

    #region Login
    public string Login
    {
      get
      {
        string loginKey = this.context.GetString(Resource.String.key_login);
        string defaultLogin = this.context.GetString(Resource.String.text_default_login);

        return this.GetString(loginKey, defaultLogin);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_login), value);
      }
    }

    #endregion Login

    #region Password
    public string Password
    {
      get
      {
        string passwordKey = this.context.GetString(Resource.String.key_password);
        string defaultPassword = this.context.GetString(Resource.String.text_default_password);

        return this.GetString(passwordKey, defaultPassword);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_password), value);
      }
    }

    #endregion Password

    #region Site
    public string Site
    {
      get
      {
        string siteKey = this.context.GetString(Resource.String.key_site);
        string defaultSite = this.context.GetString(Resource.String.text_default_site);

        return this.GetString(siteKey, defaultSite);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_site), value);
      }
    }

    #endregion Site

    #region Database
    public string Database
    {
      get
      {
        string databaseKey = this.context.GetString(Resource.String.key_database);
        string defaultDatabase = this.context.GetString(Resource.String.text_default_database);

        return this.GetString(databaseKey, defaultDatabase);
      }

      set
      {
        this.PutString(this.context.GetString(Resource.String.key_database), value);
      }
    }

    #endregion Database

    public SessionConfig SessionConfig
    {
      get
      {
        return new SessionConfig(this.InstanceUrl, this.Login, this.Password, this.Site);
      }
    }

    public ItemSource ItemSource
    {
      get
      {
        return new ItemSource(this.Database, "en");
      }
    }

    public static Prefs From(Context context)
    {
      return new Prefs(context, PreferenceManager.GetDefaultSharedPreferences(context));
    }


    private string GetString(string key, string defaultValue)
    {
      return this.prefs.GetString(key, defaultValue);
    }

    private void PutString(string key, string value)
    {
      ISharedPreferencesEditor editor = this.prefs.Edit();
      editor.PutString(key, value);
      editor.Apply();
    }
  }
}