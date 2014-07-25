

namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using System.Diagnostics;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.Validators;

  // TODO : remove this class
  public class SessionConfig : ISessionConfig
  {
    public SessionConfig(
      string instanceUrl, 
      string site = null, 
      string itemWebApiVersion = "v1")
    {
      this.InstanceUrl = instanceUrl;
      this.Site        = site;
      this.ItemWebApiVersion = itemWebApiVersion;

      this.Validate();
    }

    #region ICloneable
    public virtual SessionConfig ShallowCopy()
    {
      SessionConfig result = new SessionConfig(
        this.InstanceUrl, 
        this.Site, 
        this.ItemWebApiVersion);

      return result;
    }

    public virtual ISessionConfig SessionConfigShallowCopy()
    {
      return this.ShallowCopy();
    }
    #endregion ICloneable

    #region Properties
    public string InstanceUrl
    {
      get;
      protected set;
    }

    public string Site      
    { 
      get; 
      protected set; 
    }
    public string ItemWebApiVersion
    {
      get; 
      protected set;
    }
    #endregion Properties

    #region Validation
    private void Validate()
    {
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage
      (
        this.InstanceUrl,
        "SessionConfig.InstanceUrl is required"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage
      (
        this.ItemWebApiVersion,
        "SessionConfig.ItemWebApiVersion is required"
      );


      if (!SessionConfigValidator.IsValidSchemeOfInstanceUrl(this.InstanceUrl))
      {
        Debug.WriteLine("[WARNING] : SessionConfig - instance URL does not have a scheme");
      }
    }
    #endregion Validation
  }
}

