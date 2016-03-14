namespace Sitecore.MobileSDK.SessionSettings
{
  using System.Diagnostics;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.Validators;

  // TODO : remove this class
  public class SessionConfig : ISessionConfig
  {
    public SessionConfig(
      string instanceUrl,
      string site = null)
    {
      this.InstanceUrl = instanceUrl;
      this.Site = site;

      this.Validate();
    }

    #region ICloneable
    public virtual SessionConfig ShallowCopy()
    {
      SessionConfig result = new SessionConfig(
        this.InstanceUrl,
        this.Site
      );

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
    #endregion Properties

    #region Validation
    private void Validate()
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(this.InstanceUrl, "SessionConfig.InstanceUrl is required");

      if (!SessionConfigValidator.IsValidSchemeOfInstanceUrl(this.InstanceUrl))
      {
        Debug.WriteLine("[WARNING] : SessionConfig - instance URL does not have a scheme");
      }
    }
    #endregion Validation
  }
}

